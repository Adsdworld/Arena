﻿using System;
using Script.Game.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Ui.menu.Abilities
{
    public class R : MonoBehaviour
    {
        public long CooldownRStart;
        public long CooldownREnd;

        public Image RBAR;

        void Update()
        {
            // --- Affichage de la barre ---
            var controlledEntity = LocalPlayer.Instance.GetControlledEntityComponent();
            if (controlledEntity != null)
            {
                CooldownRStart = controlledEntity.CooldownRStart;
                CooldownREnd = controlledEntity.CooldownREnd;
            }

            long nowMs = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            if (nowMs < CooldownRStart)
            {
                RBAR.fillAmount = 0f;
            }
            else if (nowMs >= CooldownREnd)
            {
                RBAR.fillAmount = 1f;
            }
            else
            {
                long duration = CooldownREnd - CooldownRStart;
                long elapsed = nowMs - CooldownRStart;
                float fill = (float)elapsed / duration;
                RBAR.fillAmount = fill;
            }
        }
    }
}