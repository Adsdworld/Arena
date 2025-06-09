using Newtonsoft.Json;

namespace Script.Game.Entity
{
    public class EntityCollider
    {
        [JsonProperty("enabled")]
        public bool Enabled { get; set; }
    }
}