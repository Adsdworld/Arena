using System;
using Script.Game.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Ui.menu.Abilities
{
    public class W : MonoBehaviour
    {
        public long CooldownWStart;
        public long CooldownWEnd;

        public Image WBAR;

        void Update()
        {
            // --- Affichage de la barre ---
            var controlledEntity = LocalPlayer.Instance.GetControlledEntityComponent();
            if (controlledEntity != null)
            {
                CooldownWStart = controlledEntity.CooldownWStart;
                CooldownWEnd = controlledEntity.CooldownWEnd;
            }

            long nowMs = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            if (nowMs < CooldownWStart)
            {
                WBAR.fillAmount = 0f;
            }
            else if (nowMs >= CooldownWEnd)
            {
                WBAR.fillAmount = 1f;
            }
            else
            {
                long duration = CooldownWEnd - CooldownWStart;
                long elapsed = nowMs - CooldownWStart;
                float fill = (float)elapsed / duration;
                WBAR.fillAmount = fill;
            }
        }
    }
}