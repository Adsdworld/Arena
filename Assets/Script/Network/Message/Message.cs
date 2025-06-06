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
        [JsonProperty("uuid")]
        private string _uuid;
        
        [NonSerialized] private ActionEnum _action;
        [JsonProperty("action")]
        public string Action => _action.GetActionName();  // Converti enum → string au moment de la sérialisation

        [NonSerialized] private GameNameEnum _gameName;
        [JsonProperty("gameName")]
        public string GameNameEnum => _gameName.GetGameName();  // Pareil
        
        [JsonProperty("posX")]
        private float? _x;
        
        [JsonProperty("posZ")]
        private float? _z;
        
        [JsonProperty("posY")]
        private float? _y;
        
        [JsonProperty("posXDesired")]
        private float? _posXDesired;
        
        [JsonProperty("posZDesired")]
        private float? _posZDesired;
        
        [JsonProperty("posYDesired")]
        private float? _posYDesired;
        
        [JsonProperty("rotationY")]
        private float? _rotationY;
        
        [JsonProperty("timestamp")]
        private long _timestamp;

        [JsonProperty("cooldownQStart")] private long _cooldownQStart;
        [JsonProperty("cooldownWStart")] private long _cooldownWStart;
        [JsonProperty("cooldownEStart")] private long _cooldownEStart;
        [JsonProperty("cooldownRStart")] private long _cooldownRStart;
        
        

        
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
        
        public void SetX(float? x)
        {
            _x = x;
        }
        public void SetZ(float? z)
        {
            _z = z;
        }
        public void SetY(float? y)
        {
            _y = y;
        }
        public void SetPosXDesired(float? posXDesired)
        {
            _posXDesired = posXDesired;
        }
        public void SetPosZDesired(float? posZDesired)
        {
            _posZDesired = posZDesired;
        }
        public void SetPosYDesired(float? posYDesired)
        {
            _posYDesired = posYDesired;
        }
        
        
        public long GetTimestamp()
        {
            return _timestamp;
        }
        public void SetTimestamp(long timestamp)
        {
            _timestamp = timestamp;
        }
        
        public void SetCooldownQStart(long cooldownQStart)
        {
            _cooldownQStart = cooldownQStart;
        }
        public void SetCooldownWStart(long cooldownWStart)
        {
            _cooldownWStart = cooldownWStart;
        }
        public void SetCooldownEStart(long cooldownEStart)
        {
            _cooldownEStart = cooldownEStart;
        }
        public void SetCooldownRStart(long cooldownRStart)
        {
            _cooldownRStart = cooldownRStart;
        }
        
        public void SetRotationY(float? rotationY)
        {
            _rotationY = rotationY;
        }

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }
    }
}
