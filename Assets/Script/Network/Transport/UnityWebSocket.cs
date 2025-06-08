using UnityEngine;
using Script.Utils;
using Script.Game.Player;
using Script.Game.Core;
using Script.Network.Message;
using WebSocketSharp;
using System;
using System.Collections;
using Newtonsoft.Json;
using Script.Game.handlers;
using Script.Network.response;
using Unity.VisualScripting;


namespace Script.Network.Transport
{
    public class UnityWebSocket : MonoBehaviour
    {
        public static UnityWebSocket Instance { get; private set; }

        [SerializeField] private static WebSocket _websocket;
        public bool production;
        private static bool _shouldReconnect = true;
        private static bool _logEnabled = true;

        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                if (_logEnabled) Log.Info("Destroying " + gameObject.name);
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start() // Do not add code here, add code OnOpen(), OnMessage(), OnError(), OnClose().
        {
            if (_logEnabled) Log.Info("Start Initialisation du client WebSocket...");
            
            MessageService.MessageSender = new UnityWebSocketMessageSender();

            _websocket = production? new WebSocket("ws://arenafr.servegame.com:54099") : new WebSocket("ws://localhost:54099");

            _websocket.OnOpen += OnOpen;
            _websocket.OnMessage += OnMessage;
            _websocket.OnError += OnError;
            _websocket.OnClose += OnClose;

            _websocket.ConnectAsync();
        }

        private void OnOpen(object sender, EventArgs e)
        {
            _logEnabled = true;
            if (_logEnabled) Log.Info("OnOpen Connexion WebSocket établie.");
            MainThreadDispatcher.Enqueue(() =>
            {
                Message.Message message = new Message.Message();
                message.SetAction(ActionEnum.Login);
                message.SetUuid(UuidManager.GetUuid());
                message.Send();

            });
            //Log.Info("Message de connexion envoyé.");
        }

        private void OnMessage(object sender, MessageEventArgs e)
        {
            if (!e.Data.Contains("Game State")) // hide frequent Game State messages
            {
                Log.Info("Message reçu du serveur : " + e.Data);
            }

            try
            {
                var response = JsonConvert.DeserializeObject<Response>(e.Data);
                if (response == null)
                {
                    Log.Failure("Réponse désérialisée est null.");
                    return;
                }
                
                // Appel du handler approprié
                MainThreadDispatcher.Enqueue(() =>
                {
                    ResponseHandlerManager.HandleResponse(response);
                });
            }
            catch (Exception exception)
            {
                Log.Warn("A enum can not be found or other errors");
                Log.Failure("Exception dans OnMessage: " + exception.Message + "\n" + exception.StackTrace);
                throw;
            }
        }
        
        // TODO: Create a validate Json for OnMessage
        // TODO: move it to Utils
        // TODO: improve it to
        /**
         * Enum not found error : enum value present in the message but not in the Java enum
         * field not found error : field present in the message but not in the Java class
         */

        private void OnError(object sender, ErrorEventArgs e)
        {
            Log.Failure("OnError Erreur WebSocket : " + e.Message);
        }

        private void OnClose(object sender, CloseEventArgs e)
        {
            if (_logEnabled) Log.Info("OnClose Connexion WebSocket fermée. Code: " + e.Code);

            _websocket.OnOpen -= OnOpen;
            _websocket.OnMessage -= OnMessage;
            _websocket.OnError -= OnError;
            _websocket.OnClose -= OnClose;
            _websocket = null;
            
            _logEnabled = false;

            if (_shouldReconnect)
            {
                Start();
            }
        }


        public static void SendMessage(Message.Message msg)
        {
            if (Instance == null)
            {
                Log.Warn("Tentative d'envoi alors que le client WebSocket n'est pas initialisé.");
                return;
            }

            if (msg == null)
            {
                Log.Failure("Tentative d'envoi d'un message null.");
                return;
            }

            if (_websocket == null || !_websocket.IsAlive)
            {
                Log.Warn("WebSocket non connecté.");
                return;
            }

            string json = JsonConvert.SerializeObject(msg);

            if (string.IsNullOrEmpty(json))
            {
                Log.Failure("Message JSON null ou vide.");
                return;
            }

            try
            {
                _websocket.Send(json);
                Log.Info("Message envoyé au serveur : " + json);
            }
            catch (Exception e)
            {
                Log.Info("Erreur lors de l'envoi du message : " + json);
                Log.Failure("Erreur : " + e.Message);
            }
        }

        private void OnDestroy()
        {
            Log.Info("OnDestroy >> CloseWebSocket()");
            _websocket.Close(CloseStatusCode.Normal);
        }

        private void OnApplicationQuit()
        {
            Log.Info("OnApplicationQuit >> CloseWebSocket()");
            _shouldReconnect = false;
            _websocket.Close(CloseStatusCode.Normal);
        }
    }
}
