using System;
using Script.Game.Entity;
using Script.Game.Player.Listeners;
using Script.Network.Message;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.Game.Player.Controls
{
    public class Q : MonoBehaviour
    {
        private InputSystem_Actions controls;
        [SerializeField] private EntityComponent entityComponent;


        private void Awake()
        {
            controls = new InputSystem_Actions();
            controls.Player.Q.performed += OnQ;
        }

        private void OnEnable()
        {
            controls.Player.Enable();
        }

        private void OnDisable()
        {
            controls.Player.Disable();
        }

        private void OnQ(InputAction.CallbackContext context)
        {
            var now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            if (!entityComponent.IsUnityNull())
            {
                if (entityComponent.CooldownQEnd <= now)
                {
                    entityComponent.CooldownQEnd = now + entityComponent.CooldownQMs;
                    entityComponent.CooldownQStart = now;
                    ListenerScheduler.Instance.SendLocalPlayerUpdate();
                }
            }
        }
        
        public void UpdateQEntityController(GameObject agameObject)
        {
            entityComponent = agameObject.GetComponent<EntityComponent>();
        }
    }
}