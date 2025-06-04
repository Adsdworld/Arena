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
        [JsonProperty("_uuid")]
        public string Uuid { get; set; }

        [JsonProperty("_reponse")] public ResponseEnum Response_ { get; set; }

        [JsonProperty("_gameName")] public GameNameEnum GameName { get; set; }

        [JsonProperty("_ability")] public string Ability { get; set; }
        
        [JsonProperty("_text")] public string Text { get; set; }

        [JsonProperty("_notify")] public string Notify { get; set; }
        
        [JsonProperty("_livingEntities")] public List<LivingEntity> LivingEntities { get; set; }

        public ResponseEnum GetResponse()
        {
            return Response_;
        }
    }
}