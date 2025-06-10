using Newtonsoft.Json;

namespace Script.Game.Entity
{
    [System.Serializable]
    public class LivingEntity : ILivingEntity
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("health")]
        public int Health { get; set; }

        [JsonProperty("maxHealth")]
        public int MaxHealth { get; set; }

        [JsonProperty("armor")]
        public int Armor { get; set; }

        [JsonProperty("magicResist")]
        public int MagicResist { get; set; }

        [JsonProperty("attackDamage")]
        public int AttackDamage { get; set; }

        [JsonProperty("abilityPower")]
        public int AbilityPower { get; set; }

        [JsonProperty("moveSpeed")]
        public float MoveSpeed { get; set; }

        [JsonProperty("moving")]
        public bool Moving { get; set; }

        [JsonProperty("posX")]
        public float PosX { get; set; }
        [JsonProperty("posZ")]
        public float PosZ { get; set; }
        [JsonProperty("posY")]
        public float PosY { get; set; }
        [JsonProperty("posSkinX")]
        public float PosSkinX { get; set; }
        [JsonProperty("posSkinZ")]
        public float PosSkinZ { get; set; }
        [JsonProperty("posSkinY")]
        public float PosSkinY { get; set; }
        [JsonProperty("skinScale")]
        public float SkinScale { get; set; }
        [JsonProperty("skinAnimation")]
        public string SkinAnimation { get; set; }
        [JsonProperty("skinAnimationSpeed")]
        public float SkinAnimationSpeed { get; set; }
        [JsonProperty("posXDesired")]
        public float PosXDesired { get; set; }
        [JsonProperty("posZDesired")]
        public float PosZDesired { get; set; }
        [JsonProperty("posYDesired")]
        public float PosYDesired { get; set; }
        
        [JsonProperty("rotationY")]
        public float RotationY { get; set; }

        [JsonProperty("team")]
        public int Team { get; set; }
        
        [JsonProperty("cooldownQStart")]
        public long CooldownQStart { get; set; }
        [JsonProperty("cooldownWStart")]
        public long CooldownWStart { get; set; }
        [JsonProperty("cooldownEStart")]
        public long CooldownEStart { get; set; }
        [JsonProperty("cooldownRStart")]
        public long CooldownRStart { get; set; }
        
        [JsonProperty("cooldownQEnd")]
        public long CooldownQEnd { get; set; }
        [JsonProperty("cooldownWEnd")]
        public long CooldownWEnd { get; set; }
        [JsonProperty("cooldownEEnd")]
        public long CooldownEEnd { get; set; }
        [JsonProperty("cooldownREnd")]
        public long CooldownREnd { get; set; }
        
        [JsonProperty("cooldownQMs")]
        public long CooldownQMs { get; set; }
        [JsonProperty("cooldownWMs")]
        public long CooldownWMs { get; set; }
        [JsonProperty("cooldownEMs")]
        public long CooldownEMs { get; set; }
        [JsonProperty("cooldownRMs")]
        public long CooldownRMs { get; set; }
        
        /**
         * Components
         */
        
        [JsonProperty("collider")]
        public EntityCollider Collider_ { get; set; }
        [JsonProperty("rigidbody")]
        public EntityRigidbody Rigidbody_ { get; set; }
        [JsonProperty("navMeshAgent")]
        public EntityNavMeshAgent NavMeshAgent_ { get; set; }
        [JsonProperty("transform")]
        public EntityTransform Transform_ { get; set; }
        
        [JsonProperty("RuntimeTypeAdapterFactoryClazz")]
        public string RuntimeTypeAdapterFactoryClazz { get; set; }
    }
}