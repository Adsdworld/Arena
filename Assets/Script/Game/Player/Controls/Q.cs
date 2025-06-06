using System;
using Script.Game.Player.Listeners;
using Script.Network.Message;

namespace Script.Game.Player.Controls
{
    public class Q
    {
        private InputSystem_Actions controls;

        private void Awake()
        {
            controls = new InputSystem_Actions();
            controls.Player.Q.performed += OnQ;
        }

        private void OnEnable()
        {
            controls.ServerSelector.Enable();
        }

        private void OnDisable()
        {
            controls.ServerSelector.Disable();
        }

        private void OnQ()
        {
            long now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            var entity = LocalPlayer.Instance.GetControlledEntityComponent();
            if (entity != null)
            {
                entity.CooldownQStart = now;
            }

            Message message = new Message();
            message.SetAction(ActionEnum.CooldownStart);
            message.SetCooldownQStart(now);
            ListenerScheduler.Instance.TriggerListeners();
            message.SetX(entity.PosX);
            message.SetZ(entity.PosZ);
            message.SetY(entity.PosY);
            message.SetRotationY(entity.RotationY);
            message.Send();
        }
    }
}