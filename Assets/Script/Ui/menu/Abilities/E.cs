using System;
using Script.Game.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Ui.menu.Abilities
{
    public class E : MonoBehaviour
    {
        public long CooldownEStart;
        public long CooldownEEnd;

        public Image EBAR;

        void Update()
        {
            // --- Affichage de la barre ---
            var controlledEntity = LocalPlayer.Instance.GetControlledEntityComponent();
            if (controlledEntity != null)
            {
                CooldownEStart = controlledEntity.CooldownEStart;
                CooldownEEnd = controlledEntity.CooldownEEnd;
            }

            long nowMs = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            if (nowMs < CooldownEStart)
            {
                EBAR.fillAmount = 0f;
            }
            else if (nowMs >= CooldownEEnd)
            {
                EBAR.fillAmount = 1f;
            }
            else
            {
                long duration = CooldownEEnd - CooldownEStart;
                long elapsed = nowMs - CooldownEStart;
                float fill = (float)elapsed / duration;
                EBAR.fillAmount = fill;
            }
        }
    }
}