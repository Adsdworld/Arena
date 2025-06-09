using Newtonsoft.Json;

namespace Script.Game.Entity
{
    public class EntityTransform
    {
        [JsonProperty("scale")]
        public float Scale { get; set; }
    }
}