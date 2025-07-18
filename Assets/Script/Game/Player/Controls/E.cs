﻿using System;
using Script.Game.Entity;
using Script.Game.Player.Listeners;
using Script.Network.Message;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.Game.Player.Controls
{
    public class E : MonoBehaviour
    {
        private InputSystem_Actions controls;
        [SerializeField] private EntityComponent entityComponent;

        private void Awake()
        {
            controls = new InputSystem_Actions();
            controls.Player.E.performed += OnE;
        }

        private void OnEnable()
        {
            controls.Player.Enable();
        }

        private void OnDisable()
        {
            controls.Player.Disable();
        }

        private void OnE(InputAction.CallbackContext context)
        {
            var now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            if (!entityComponent.IsUnityNull())
            {
                if (entityComponent.CooldownEEnd <= now)
                {
                    entityComponent.CooldownEEnd = now + entityComponent.CooldownEMs;
                    entityComponent.CooldownEStart = now;
                    ListenerScheduler.Instance.SendLocalPlayerUpdate(ActionEnum.CastE);
                }
            }
        }
        
        public void UpdateEEntityController(GameObject agameObject)
        {
            entityComponent = agameObject.GetComponent<EntityComponent>();
        }
    }
}