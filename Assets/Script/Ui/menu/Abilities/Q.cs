using System;
using Script.Game.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Ui.menu.Abilities
{
    public class Q : MonoBehaviour
    {
        public long CooldownQStart;
        public long CooldownQEnd;

        public Image ABAR;

        void Update()
        {
            // --- Affichage de la barre ---
            var controlledEntity = LocalPlayer.Instance.GetControlledEntityComponent();
            if (controlledEntity != null)
            {
                CooldownQStart = controlledEntity.CooldownQStart;
                CooldownQEnd = controlledEntity.CooldownQEnd;
            }

            long nowMs = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            if (nowMs < CooldownQStart)
            {
                ABAR.fillAmount = 0f;
            }
            else if (nowMs >= CooldownQEnd)
            {
                ABAR.fillAmount = 1f;
            }
            else
            {
                long duration = CooldownQEnd - CooldownQStart;
                long elapsed = nowMs - CooldownQStart;
                float fill = (float)elapsed / duration;
                ABAR.fillAmount = fill;
            }
        }
    }
}