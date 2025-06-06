using System;
using Script.Game.Player.Listeners;
using Script.Network.Message;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.Game.Player.Controls
{
    public class W : MonoBehaviour
    {
        private InputSystem_Actions controls;

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
            long now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            var entity = LocalPlayer.Instance.GetControlledEntityComponent();
            if (entity != null)
            {
                entity.CooldownWEnd = now + entity.CooldownWMs;
                entity.CooldownWStart = now;
            }

            Message message = ListenerScheduler.Instance.CreateMessage();
            message.SetAction(ActionEnum.CooldownStart);
            message.SetCooldownWStart(now);
            message.Send();
        }
    }
}