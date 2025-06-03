using UnityEngine;

namespace Script.Game.Entity
{
    public class Entity : MonoBehaviour
    {
        [Header("Base Stats")]
        [SerializeField] protected float maxHealth;
        [SerializeField] protected float currentHealth;

        [SerializeField] protected float armor;
        [SerializeField] protected float magicResist;

        [SerializeField] protected float attackDamage;
        [SerializeField] protected float abilityPower;

        [SerializeField] protected float moveSpeed;

        [Header("Identifier")]
        [SerializeField] protected string uuid;

        // Propriétés publiques pour lecture/écriture contrôlée
        public float MaxHealth => maxHealth;
        public float CurrentHealth
        {
            get => currentHealth;
            set => currentHealth = Mathf.Clamp(value, 0, maxHealth);
        }

        public float Armor => armor;
        public float MagicResist => magicResist;

        public float AttackDamage => attackDamage;
        public float AbilityPower => abilityPower;

        public float MoveSpeed => moveSpeed;

        public string UUID
        {
            get => uuid;
            set => uuid = value;
        }
    }
}