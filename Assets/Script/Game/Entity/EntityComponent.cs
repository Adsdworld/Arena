using Script.Utils;
using UnityEngine;

namespace Script.Game.Entity
{
    public class EntityComponent : MonoBehaviour, ILivingEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Armor { get; set; }
        public int MagicResist { get; set; }
        public int AttackDamage { get; set; }
        public int AbilityPower { get; set; }
        public float MoveSpeed { get; set; }
        public bool Moving { get; set; }
        public float PosX { get; set; }
        public float PosZ { get; set; }
        public float PosXDesired { get; set; }
        public float PosZDesired { get; set; }
        public int Team { get; set; }

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
            Log.Info("Updating entity from data: " + livingEntity.Id);
        }
    }
}