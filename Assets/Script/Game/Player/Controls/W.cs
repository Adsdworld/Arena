using System;
using Script.Game.Entity;
using Script.Game.Player.Listeners;
using Script.Network.Message;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.Game.Player.Controls
{
    public class W : MonoBehaviour
    {
        private InputSystem_Actions controls;
        [SerializeField] private EntityComponent entityComponent;


        private void Awake()
        {
            controls = new InputSystem_Actions();
            controls.Player.W.performed += OnW;
        }

        private void OnEnable()
        {
            controls.Player.Enable();
        }

        private void OnDisable()
        {
            controls.Player.Disable();
        }

        private void OnW(InputAction.CallbackContext context)
        {
            var now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            if (!entityComponent.IsUnityNull())
            {
                if (entityComponent.CooldownWEnd <= now)
                {
                    entityComponent.CooldownWEnd = now + entityComponent.CooldownWMs;
                    entityComponent.CooldownWStart = now;
                    ListenerScheduler.Instance.SendLocalPlayerUpdate();
                }
            }
        }
        
        public void UpdateWEntityController(GameObject agameObject)
        {
            entityComponent = agameObject.GetComponent<EntityComponent>();
        }
    }
}