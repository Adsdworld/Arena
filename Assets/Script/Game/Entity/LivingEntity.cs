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
        [JsonProperty("posXDesired")]
        public float PosXDesired { get; set; }
        [JsonProperty("posZDesired")]
        public float PosZDesired { get; set; }
        
        [JsonProperty("rotation")]
        public float Rotation { get; set; }

        [JsonProperty("team")]
        public int Team { get; set; }
        
        [JsonProperty("cooldownQ")]
        public float CooldownQ { get; set; }
        [JsonProperty("cooldownW")]
        public float CooldownW { get; set; }
        [JsonProperty("cooldownE")]
        public float CooldownE { get; set; }
        [JsonProperty("cooldownR")]
        public float CooldownR { get; set; }
    }
}