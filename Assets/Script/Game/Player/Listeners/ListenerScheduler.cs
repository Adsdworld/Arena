using UnityEngine;
using System.Collections.Generic;
using System;
using Script.Game.Entity;
using Script.Network.Message;

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
                TriggerListeners();
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

        public Message CreateMessage()
        {
            TriggerListeners();

            var localPlayer = LocalPlayer.Instance;
            
            var entity = localPlayer.GetControlledEntityComponent();
            Message message = new Message();
            
            message.SetGameNameEnum(localPlayer.GameName);
            
            message.SetX(entity.PosX);
            message.SetZ(entity.PosZ);
            message.SetY(entity.PosY);
            message.SetRotationY(entity.RotationY);
            message.SetPosXDesired(entity.PosXDesired);
            message.SetPosZDesired(entity.PosZDesired);
            message.SetPosYDesired(entity.PosYDesired);

            message.SetCooldownQStart(entity.CooldownQStart);
            message.SetCooldownWStart(entity.CooldownWStart);
            message.SetCooldownEStart(entity.CooldownEStart);
            message.SetCooldownRStart(entity.CooldownRStart);

            return message;
        }
    }
}