using System;
using Script.Game.Entity;
using Script.Game.Player.Listeners;
using Script.Network.Message;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.Game.Player.Controls
{
    public class R : MonoBehaviour
    {
        private InputSystem_Actions controls;
        [SerializeField] private EntityComponent entityComponent;


        private void Awake()
        {
            controls = new InputSystem_Actions();
            controls.Player.R.performed += OnR;
        }

        private void OnEnable()
        {
            controls.Player.Enable();
        }

        private void OnDisable()
        {
            controls.Player.Disable();
        }

        private void OnR(InputAction.CallbackContext context)
        {
            var now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            if (!entityComponent.IsUnityNull())
            {
                if (entityComponent.CooldownREnd <= now)
                {
                    entityComponent.CooldownREnd = now + entityComponent.CooldownRMs;
                    entityComponent.CooldownRStart = now;
                    ListenerScheduler.Instance.SendLocalPlayerUpdate();
                }
            }
        }
        
        public void UpdateREntityController(GameObject agameObject)
        {
            entityComponent = agameObject.GetComponent<EntityComponent>();
        }
    }
}