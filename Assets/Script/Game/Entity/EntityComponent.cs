using Script.Utils;
using UnityEngine;

namespace Script.Game.Entity
{
    public class EntityComponent : MonoBehaviour, ILivingEntity
    {
        private string id;
        private string name;
        private int health;
        private int maxHealth;
        private int armor;
        private int magicResist;
        private int attackDamage;
        private int abilityPower;
        private float moveSpeed;
        private bool moving;
        private float posX;
        private float posZ;
        private float posXDesired;
        private float posZDesired;
        private int team;

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int Health { get => health; set => health = value; }
        public int MaxHealth { get => maxHealth; set => maxHealth = value; }
        public int Armor { get => armor; set => armor = value; }
        public int MagicResist { get => magicResist; set => magicResist = value; }
        public int AttackDamage { get => attackDamage; set => attackDamage = value; }
        public int AbilityPower { get => abilityPower; set => abilityPower = value; }
        public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
        public bool Moving { get => moving; set => moving = value; }
        public float PosX { get => posX; set => posX = value; }
        public float PosZ { get => posZ; set => posZ = value; }
        public float PosXDesired { get => posXDesired; set => posXDesired = value; }
        public float PosZDesired { get => posZDesired; set => posZDesired = value; }
        public int Team { get => team; set => team = value; }

        // Méthode pour initialiser les propriétés à partir d'une donnée ILivingEntity
        public void Initialize(ILivingEntity data)
        {
            Id = data.Id;
            Name = data.Name;
            Health = data.Health;
            MaxHealth = data.MaxHealth;
            Armor = data.Armor;
            MagicResist = data.MagicResist;
            AttackDamage = data.AttackDamage;
            AbilityPower = data.AbilityPower;
            MoveSpeed = data.MoveSpeed;
            Moving = data.Moving;
            PosX = data.PosX;
            PosZ = data.PosZ;
            PosXDesired = data.PosXDesired;
            PosZDesired = data.PosZDesired;
            Team = data.Team;

            // Positionner le transform du GameObject à la position initiale
            transform.position = new Vector3(PosX, transform.position.y, PosZ);
        }

        public void UpdateFromData(LivingEntity livingEntity)
        {
            Log.Info("@@@ Updating entity from data: " + livingEntity.Id);
            
            Health = livingEntity.Health;
            
        }
    }
}