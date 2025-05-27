using System;
using Script.Game;
using Script.Game.Player;
using UnityEngine;
using Newtonsoft.Json;
using Script.Game.Core;
using Script.Utils;


namespace Script.Network.Message
{
    /*
     * Serializable tag indicate that the class can be serialized into JSON format.
     */
    [Serializable]
    public class Message
    {
        [JsonProperty("_uuid")]
        private string _uuid;
        
        [NonSerialized] private ActionEnum _action;
        [JsonProperty("_action")]
        public string Action => _action.GetAction();  // Converti enum → string au moment de la sérialisation

        [NonSerialized] private GameNameEnum _gameName;
        [JsonProperty("_gameName")]
        public string GameNameEnum => _gameName.GetGameName();  // Pareil
        
        [JsonProperty("_ability")]
        private string _ability;
        
        [JsonProperty("_x")]
        private float? _x;
        
        [JsonProperty("_z")]
        private float? _z;
        
        [JsonProperty("_timestamp")]
        private long _timestamp;

        
        /// <summary>
        /// uuid is send by default, it is used to identify the player.
        /// </summary>
        public void Send()
        {
            _uuid = UuidManager.GetUuid();
            _timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            MessageService.MessageSender?.SendMessage(this);
        }
        
        /*
         * Getters and Setters
         */
        /// <summary>
        /// Prefer UuidManager.GetUuid() instead of this method
        /// </summary>
        public string GetUuid()
        {
            throw new Exception("Use UuidManager.GetUuid() instead of this method message.GetUuid().");
        }
        public void SetUuid(string uuid)
        {
            _uuid = uuid;
        }
        
        public ActionEnum GetAction()
        {
            return _action;
        }
        public void SetAction(ActionEnum action)
        {
            _action = action;
        }
        
        public GameNameEnum GetGameNameEnum()
        {
            return _gameName;
        }
        public void SetGameNameEnum(GameNameEnum gameNameEnum)
        {
            _gameName = gameNameEnum;
        }
        
        public string GetAbility()
        {
            return _ability;
        }
        public void SetAbility(string ability)
        {
            _ability = ability;
        }
        
        public float? GetX()
        {
            return _x;
        }
        public void SetX(float? x)
        {
            _x = x;
        }
        
        public float? GetZ()
        {
            return _z;
        }
        public void SetZ(float? z)
        {
            _z = z;
        }

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }
    }
}
