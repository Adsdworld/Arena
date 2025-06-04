using System.Collections ; 
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Ui.menu
{
    public class Health : MonoBehaviour
    {
        public float health = 75f; 
        public float maxHealth = 100f; 
    
        public Image healthBar; 
    
    
        // Update is called once per frame 
        void Update()
        {
            healthBar.fillAmount = health / maxHealth;
        }
    }
}
