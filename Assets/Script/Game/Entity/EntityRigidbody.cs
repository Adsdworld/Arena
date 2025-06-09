using Newtonsoft.Json;

namespace Script.Game.Entity
{
    public class EntityRigidbody
    {
        [JsonProperty("isKinematic")]
        public bool IsKinematic { get; set; }
    }
}