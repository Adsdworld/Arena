using System;
using Script.Game;
using Script.Game.Player;
using UnityEngine;
using Newtonsoft.Json;
using Script.Game.Core;
using Script.Game.Entity;
using Script.Network.Transport;
using Script.Utils;


namespace Script.Network.Message
{
    /*
     * Serializable tag indicate that the class can be serialized into JSON format.
     */
    [Serializable]
    public class Message
    {
        [JsonProperty("uuid")]
        private string _uuid;
        
        [NonSerialized] private ActionEnum _action;
        [JsonProperty("action")]
        public string Action => _action.GetActionName();  // Converti enum → string au moment de la sérialisation

        [NonSerialized] private GameNameEnum _gameName;
        [JsonProperty("gameName")]
        public string GameNameEnum => _gameName.GetGameName();  // Pareil
        
        
        [JsonProperty("timestamp")]
        private long _timestamp;
        
        [JsonProperty("livingEntity")]
        LivingEntity _livingEntity;
        
        /// <summary>
        /// uuid is send by default, it is used to identify the player.
        /// </summary>
        public void Send()
        {
            _uuid = UuidManager.GetUuid();
            //_timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            _timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + TimeSync.TimeOffsetMs;

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
        
        public LivingEntity GetLivingEntity()
        {
            return _livingEntity;
        }
        public void SetLivingEntity(LivingEntity livingEntity)
        {
            _livingEntity = livingEntity;
        }
        
        public long GetTimestamp()
        {
            return _timestamp;
        }
        public void SetTimestamp(long timestamp)
        {
            _timestamp = timestamp;
        }
        

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }
    }
}
