using System.Collections ; 
using System.Collections.Generic;
using Script.Game.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Ui.menu.Abilities
{ 
    public class A : MonoBehaviour
    {
        public float health;
        public float maxHealth;

        public Image ABAR;


        // Update is called once per frame 
        void Update()
        {
            var entity = LocalPlayer.Instance.GetControlledEntityComponent();
            if (entity != null)
            {
                health = entity.Health;
                maxHealth = entity.MaxHealth;
            }

            ABAR.fillAmount = health / maxHealth;
        }
    }
}
