using Script.Game.Core;
using Script.Utils;
using UnityEngine;

namespace Script.Game.Entity
{
    public class EntityComponent : MonoBehaviour, ILivingEntity
    {
        [SerializeField] private string _id;
        [SerializeField] private string _name;
        [SerializeField] private int _health;
        [SerializeField] private int _maxHealth;
        [SerializeField] private int _armor;
        [SerializeField] private int _magicResist;
        [SerializeField] private int _attackDamage;
        [SerializeField] private int _abilityPower;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private bool _moving;
        [SerializeField] private float _posX;
        [SerializeField] private float _posZ;
        [SerializeField] private float _posY;
        [SerializeField] private float _posXDesired;
        [SerializeField] private float _posZDesired;
        [SerializeField] private float _rotationY;
        [SerializeField] private int _team;
        [SerializeField] private long _cooldownQStart;
        [SerializeField] private long _cooldownWStart;
        [SerializeField] private long _cooldownEStart;
        [SerializeField] private long _cooldownRStart;
        [SerializeField] private long _cooldownQEnd;
        [SerializeField] private long _cooldownWEnd;
        [SerializeField] private long _cooldownEEnd;
        [SerializeField] private long _cooldownREnd;

        public string Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public int Health { get => _health; set => _health = value; }
        public int MaxHealth { get => _maxHealth; set => _maxHealth = value; }
        public int Armor { get => _armor; set => _armor = value; }
        public int MagicResist { get => _magicResist; set => _magicResist = value; }
        public int AttackDamage { get => _attackDamage; set => _attackDamage = value; }
        public int AbilityPower { get => _abilityPower; set => _abilityPower = value; }
        public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
        public bool Moving { get => _moving; set => _moving = value; }
        public float PosX { get => _posX; set => _posX = value; }
        public float PosZ { get => _posZ; set => _posZ = value; }
        public float PosY { get => _posY; set => _posY = value; }
        public float PosXDesired { get => _posXDesired; set => _posXDesired = value; }
        public float PosZDesired { get => _posZDesired; set => _posZDesired = value; }
        public float RotationY { get => _rotationY; set => _rotationY = value; }
        public int Team { get => _team; set => _team = value; }
        
        public long CooldownQStart { get => _cooldownQStart; set => _cooldownQStart = value; }
        public long CooldownWStart { get => _cooldownWStart; set => _cooldownWStart = value; }
        public long CooldownEStart { get => _cooldownEStart; set => _cooldownEStart = value; }
        public long CooldownRStart { get => _cooldownRStart; set => _cooldownRStart = value; }
        public long CooldownQEnd { get => _cooldownQEnd; set => _cooldownQEnd = value; }
        public long CooldownWEnd { get => _cooldownWEnd; set => _cooldownWEnd = value; }
        public long CooldownEEnd { get => _cooldownEEnd; set => _cooldownEEnd = value; }
        public long CooldownREnd { get => _cooldownREnd; set => _cooldownREnd = value; }
        

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
            RotationY = data.RotationY;
            Team = data.Team;
            
            CooldownQStart = data.CooldownQStart;
            CooldownWStart = data.CooldownWStart;
            CooldownEStart = data.CooldownEStart;
            CooldownRStart = data.CooldownRStart;
            CooldownQEnd = data.CooldownQEnd;
            CooldownWEnd = data.CooldownWEnd;
            CooldownEEnd = data.CooldownEEnd;
            CooldownREnd = data.CooldownREnd;

            // Positionner le transform du GameObject à la position initiale
            transform.position = new Vector3(PosX, transform.position.y, PosZ);
        }

        public void UpdateFromData(LivingEntity livingEntity)
        {
            Log.Info("@@@ Updating entity from data: " + livingEntity.Id);

            if (livingEntity.Id == UuidManager.GetUuid())
            {
                Id = livingEntity.Id;
                Name = livingEntity.Name;
                Health = livingEntity.Health;
                MaxHealth = livingEntity.MaxHealth;
                Armor = livingEntity.Armor;
                MagicResist = livingEntity.MagicResist;
                AttackDamage = livingEntity.AttackDamage;
                AbilityPower = livingEntity.AbilityPower;
                MoveSpeed = livingEntity.MoveSpeed;
                Moving = livingEntity.Moving;
                //PosX = livingEntity.PosX;
                //PosZ = livingEntity.PosZ;
                //PosXDesired = livingEntity.PosXDesired;
                //PosZDesired = livingEntity.PosZDesired;
                //Rotation = livingEntity.Rotation;
                Team = livingEntity.Team;
                //CooldownQStart = livingEntity.CooldownQStart;
                //CooldownWStart = livingEntity.CooldownWStart;
                //CooldownEStart = livingEntity.CooldownEStart;
                //CooldownRStart = livingEntity.CooldownRStart;
                CooldownQEnd = livingEntity.CooldownQEnd;
                CooldownWEnd = livingEntity.CooldownWEnd;
                CooldownEEnd = livingEntity.CooldownEEnd;
                CooldownREnd = livingEntity.CooldownREnd;
            }
            else
            {
                Id = livingEntity.Id;
                Name = livingEntity.Name;
                Health = livingEntity.Health;
                MaxHealth = livingEntity.MaxHealth;
                Armor = livingEntity.Armor;
                MagicResist = livingEntity.MagicResist;
                AttackDamage = livingEntity.AttackDamage;
                AbilityPower = livingEntity.AbilityPower;
                MoveSpeed = livingEntity.MoveSpeed;
                Moving = livingEntity.Moving;
                PosX = livingEntity.PosX;
                PosZ = livingEntity.PosZ;
                PosXDesired = livingEntity.PosXDesired;
                PosZDesired = livingEntity.PosZDesired;
                RotationY = livingEntity.RotationY;
                Team = livingEntity.Team;
                CooldownQStart = livingEntity.CooldownQStart;
                CooldownWStart = livingEntity.CooldownWStart;
                CooldownEStart = livingEntity.CooldownEStart;
                CooldownRStart = livingEntity.CooldownRStart;
                CooldownQEnd = livingEntity.CooldownQEnd;
                CooldownWEnd = livingEntity.CooldownWEnd;
                CooldownEEnd = livingEntity.CooldownEEnd;
                CooldownREnd = livingEntity.CooldownREnd;
            }
        }
    }
}