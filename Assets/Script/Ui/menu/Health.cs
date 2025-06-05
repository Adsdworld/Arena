using System.Collections ; 
using System.Collections.Generic;
using Script.Game.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Ui.menu
{
    public class Health : MonoBehaviour
    {
        public float health; 
        public float maxHealth; 
    
        public Image healthBar; 
    
    
        // Update is called once per frame 
        void Update()
        {

            var entity = LocalPlayer.Instance.GetControlledEntityComponent();
            if (entity != null)
            {
                health = entity.Health;
                maxHealth = entity.MaxHealth;
            }
            healthBar.fillAmount = health / maxHealth;
        }
    }
}
