using Newtonsoft.Json;

namespace Script.Game.Entity
{
    [System.Serializable]
    public class LivingEntity
    {
        [JsonProperty("id")]
        public string Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("health")]
        public int Health;

        [JsonProperty("maxHealth")]
        public int MaxHealth;

        [JsonProperty("armor")]
        public int Armor;

        [JsonProperty("magicResist")]
        public int MagicResist;

        [JsonProperty("attackDamage")]
        public int AttackDamage;

        [JsonProperty("abilityPower")]
        public int AbilityPower;

        [JsonProperty("moveSpeed")]
        public float MoveSpeed;

        [JsonProperty("moving")]
        public bool Moving;

        [JsonProperty("posX")]
        public float PosX;

        [JsonProperty("posZ")]
        public float PosZ;

        [JsonProperty("posXDesired")]
        public float PosXDesired;

        [JsonProperty("posZDesired")]
        public float PosZDesired;

        [JsonProperty("team")]
        public int Team;
    }
}