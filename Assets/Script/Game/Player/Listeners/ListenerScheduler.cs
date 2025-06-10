using UnityEngine;
using System.Collections.Generic;
using System;
using System.Threading;
using Newtonsoft.Json;
using Script.Game.Entity;
using Script.Network.Message;
using Script.Utils;

namespace Script.Game.Player.Listeners
{
    public class ListenerScheduler : MonoBehaviour
    {
        // Intervalle entre chaque update en secondes (50 ms = 0.05s)
        [SerializeField] private float interval = 1f;

        private float timer = 0f;

        // Liste des listeners à déclencher
        private List<Action> listeners = new();
        public static ListenerScheduler Instance { get; private set; }
        
        [SerializeField] private string entityId;
        [SerializeField] private EntityComponent entityComponent;
        [SerializeField] private LivingEntity livingEntity;


        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject); // Empêche plusieurs instances
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer >= interval)
            {
                timer = 0f;
                SendLocalPlayerUpdate();
            }
        }

        public void RegisterListener(Action listener)
        {
            if (!listeners.Contains(listener))
                listeners.Add(listener);
        }

        public void UnregisterListener(Action listener)
        {
            if (listeners.Contains(listener))
                listeners.Remove(listener);
        }

        public void TriggerListeners()
        {
            foreach (var listener in listeners)
            {
                listener.Invoke();
            }
        }
        
        public void UpdateListenerSchedulerEntityController(GameObject agameObject)
        {
            entityId = LocalPlayer.Instance.GetControlledEntityId();
            entityComponent = agameObject.GetComponent<EntityComponent>();
        }

        public void SendLocalPlayerUpdate()
        {
            TriggerListeners();
            if (entityId != LocalPlayer.defaultControlledEntityId)
            {
                livingEntity = entityComponent.ToLivingEntity();
            
                Message message = new Message();
                message.SetAction(ActionEnum.PlayerStateUpdate);
                message.SetLivingEntity(livingEntity);
            
                message.SetGameNameEnum(LocalPlayer.Instance.GameName);
                message.Send();
            }
        }
    }
}