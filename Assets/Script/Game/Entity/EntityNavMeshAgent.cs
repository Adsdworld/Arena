using Newtonsoft.Json;

namespace Script.Game.Entity
{
    public class EntityNavMeshAgent
    {
        [JsonProperty("enabled")]
        public bool Enabled { get; set; }
    }
}