using Script.Game.Core;
using Script.Game.Entity.Listeners;
using Script.Game.Player.Controls;
using Script.Game.Player.Listeners;
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
        [SerializeField] private float _skinScale;
        [SerializeField] private float _posXDesired;
        [SerializeField] private float _posZDesired;
        [SerializeField] private float _posYDesired;
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
        [SerializeField] private long _cooldownQMs;
        [SerializeField] private long _cooldownWMs;
        [SerializeField] private long _cooldownEMs;
        [SerializeField] private long _cooldownRMs;

        public string Id { get => _id; set => _id = value; }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                var SkinComponent = GetComponentInChildren<Skin>();
                if (SkinComponent != null) SkinComponent.UpdateSkin();
                else Log.Warn("Please check if enity prefab have Skin.cs: ");
            }
        }

        public int Health { get => _health; set => _health = value; }
        public int MaxHealth { get => _maxHealth; set => _maxHealth = value; }
        public int Armor { get => _armor; set => _armor = value; }
        public int MagicResist { get => _magicResist; set => _magicResist = value; }
        public int AttackDamage { get => _attackDamage; set => _attackDamage = value; }
        public int AbilityPower { get => _abilityPower; set => _abilityPower = value; }
        
        public float MoveSpeed { get => _moveSpeed; set =>_moveSpeed = value; }
        
        public bool Moving { get => _moving; set => _moving = value; }
        public float PosX { get => _posX; set => _posX = value; }
        public float PosZ { get => _posZ; set => _posZ = value; }
        public float PosY { get => _posY; set => _posY = value; }
        public float PosSkinX { get; set; }
        public float PosSkinZ { get; set; }
        public float PosSkinY { get; set; }
        public float SkinScale { get => _skinScale; set => _skinScale = value; }
        public float PosXDesired { get => _posXDesired; set => _posXDesired = value; }
        public float PosZDesired { get => _posZDesired; set => _posZDesired = value; }
        public float PosYDesired { get => _posYDesired; set => _posYDesired = value; }
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
        
        public long CooldownQMs { get => _cooldownQMs; set => _cooldownQMs = value; }
        public long CooldownWMs { get => _cooldownWMs; set => _cooldownWMs = value; }
        public long CooldownEMs { get => _cooldownEMs; set => _cooldownEMs = value; }
        public long CooldownRMs { get => _cooldownRMs; set => _cooldownRMs = value; }
        

        // Méthode pour initialiser les propriétés à partir d'une donnée ILivingEntity
        public void Initialize(ILivingEntity data)
        {
            Log.Info("Initializing EntityComponent with data: " + data.PosX + ", " + data.PosY + ", " + data.PosZ);

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
            PosY = data.PosY;
            PosSkinX = data.PosSkinX;
            PosSkinZ = data.PosSkinZ;
            PosSkinY = data.PosSkinY;
            SkinScale = data.SkinScale;
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
        }

        public void UpdateFromData(LivingEntity livingEntity)
        {
            //Log.Info(livingEntity.Id + " == " + UuidManager.GetUuid() +" @@@ Updating entity from data: ");
            
            if (livingEntity.Id == UuidManager.GetUuid())
            {
                //Log.Info("Equals to local player, updating local properties.");
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
                //PosY = livingEntity.PosY;
                PosSkinX = livingEntity.PosSkinX;
                PosSkinZ = livingEntity.PosSkinZ;
                PosSkinY = livingEntity.PosSkinY;
                SkinScale = livingEntity.SkinScale;
                //PosXDesired = livingEntity.PosXDesired;
                //PosZDesired = livingEntity.PosZDesired;
                //PosYDesired = livingEntity.PosYDesired;
                //RotationY = livingEntity.RotationY;
                Team = livingEntity.Team;
                //CooldownQStart = livingEntity.CooldownQStart;
                //CooldownWStart = livingEntity.CooldownWStart;
                //CooldownEStart = livingEntity.CooldownEStart;
                //CooldownRStart = livingEntity.CooldownRStart;
                CooldownQEnd = livingEntity.CooldownQEnd;
                CooldownWEnd = livingEntity.CooldownWEnd;
                CooldownEEnd = livingEntity.CooldownEEnd;
                CooldownREnd = livingEntity.CooldownREnd;
                CooldownQMs = livingEntity.CooldownQMs;
                CooldownWMs = livingEntity.CooldownWMs;
                CooldownEMs = livingEntity.CooldownEMs;
                CooldownRMs = livingEntity.CooldownRMs;
                
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
                PosY = livingEntity.PosY;
                PosSkinX = livingEntity.PosSkinX;
                PosSkinZ = livingEntity.PosSkinZ;
                PosSkinY = livingEntity.PosSkinY;
                SkinScale = livingEntity.SkinScale;
                PosXDesired = livingEntity.PosXDesired;
                PosZDesired = livingEntity.PosZDesired;
                PosYDesired = livingEntity.PosYDesired;
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
                CooldownQMs = livingEntity.CooldownQMs;
                CooldownWMs = livingEntity.CooldownWMs;
                CooldownEMs = livingEntity.CooldownEMs;
                CooldownRMs = livingEntity.CooldownRMs;
            }
        }
    }
}