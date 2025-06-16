using System.Collections.Generic;
using Script.Game;
using Script.Game.Player;
using Newtonsoft.Json;
using Script.Game.Entity;

namespace Script.Network.response
{
    /**
     * Response from the server.
     * To respond use a Message.
     */
    public class Response
    {
        [JsonProperty("uuid")]
        public string Uuid { get; set; }

        [JsonProperty("reponse")] public ResponseEnum Response_ { get; set; }

        [JsonProperty("gameName")] public GameNameEnum GameName { get; set; }
        
        [JsonProperty("text")] public string Text { get; set; }

        [JsonProperty("notify")] public string Notify { get; set; }
        
        [JsonProperty("livingEntities")] public List<LivingEntity> LivingEntities { get; set; }
        
        [JsonProperty("timestamp")] public long Timestamp { get; set; }

        public ResponseEnum GetResponse()
        {
            return Response_;
        }
    }
}