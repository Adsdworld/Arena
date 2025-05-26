using System;
using Script.Game;
using Script.Game.Player;
using UnityEngine;
using Newtonsoft.Json;



namespace Script.Network.Message
{
    /*
     * Serializable tag indicate that the class can be serialized into JSON format.
     */
    [Serializable]
    public class Message
    {
        [SerializeField] private string _uuid;
        [SerializeField] [NonSerialized] private ActionEnum _action;
        [SerializeField] [NonSerialized] private GameNameEnum _gameName;
        
        [JsonProperty("_action")]
        public string Action => _action.GetAction();  // Converti enum → string au moment de la sérialisation

        [JsonProperty("_gameName")]
        public string GameNameEnum => _gameName.GetGameName();  // Pareil
        
        [SerializeField] private string _ability;
        [SerializeField] private float? _x;
        [SerializeField] private float? _z;
        
        public void Send()
        {
            MessageService.MessageSender?.SendMessage(this);
        }
        
        /*
         * Getters and Setters
         */
        public string GetUuid()
        {
            return _uuid;
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
