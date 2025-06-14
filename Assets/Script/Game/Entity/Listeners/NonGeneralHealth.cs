using Script.Game.Player;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Game.Entity.Listeners
{
    public class Health : MonoBehaviour
    {
        public float health; 
        public float maxHealth; 
    
        public Image healthBar; 
        [SerializeField] private EntityComponent _entityComponent;
        [SerializeField] private GameObject canvasRoot;

        
        void Awake()
        {
            _entityComponent = GetComponentInParent<EntityComponent>();
            if (healthBar == null)
            {
                healthBar = GetComponent<Image>(); // ce script est sur l’image rouge
            }
            canvasRoot = GetComponentInParent<Canvas>().gameObject;
        }
        
        void Update()
        {
            if (_entityComponent.IsUnityNull()) return;

            health = _entityComponent.Health;
            maxHealth = _entityComponent.MaxHealth;

            bool isLocal = "Entity_" + _entityComponent.Id == LocalPlayer.Instance.GetControlledEntityId();
            bool isDead = health <= 0;
            canvasRoot.SetActive(!(isLocal || isDead));

            if (isLocal) return;

            if (maxHealth > 0)
            {
                healthBar.fillAmount = Mathf.Clamp01(health / maxHealth);
            }
            else
            {
                healthBar.fillAmount = 0;
            }
        }
    }
}
