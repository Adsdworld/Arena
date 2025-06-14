using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;


namespace Script.Game.Player
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ResponseEnum
    {
        [EnumMember(Value = "Info")]
        Info,
        
        [EnumMember(Value = "Logged")]
        Logged,

        [EnumMember(Value = "Game Created")]
        GameCreated,

        [EnumMember(Value = "Game Already Exists")]
        GameAlreadyExists,

        [EnumMember(Value = "Games Limit Reached")]
        GamesLimitReached,
        
        [EnumMember(Value = "Game Closed")]
        GameClosed,
        
        [EnumMember(Value = "Game Not Found")]
        GameNotFound,
        
        [EnumMember(Value = "Joined")]
        Joined,
        
        [EnumMember(Value = "Spawned")]
        Spawned,
        
        [EnumMember(Value = "Game State")]
        GameState,
        
        [EnumMember(Value = "Your Entity Is")]
        YourEntityIs,
        
        [EnumMember(Value = "Player Already In Game")]
        PlayerAlreadyInGame,
    }
    
    public static class ResponseEnumExtensions
    {
        public static string GetResponseName(this ResponseEnum responseEnum)
        {
            return responseEnum switch
            {
                ResponseEnum.Info => "Info",
                ResponseEnum.Logged => "Logged",
                ResponseEnum.GameCreated => "Game Created",
                ResponseEnum.GameAlreadyExists => "Game Already Exists",
                ResponseEnum.GamesLimitReached => "Games Limit Reached",
                ResponseEnum.GameClosed => "Game Closed",
                ResponseEnum.GameNotFound => "Game Not Found",
                ResponseEnum.Joined => "Joined",
                ResponseEnum.Spawned => "Spawned",
                ResponseEnum.GameState => "Game State",
                ResponseEnum.YourEntityIs => "Your Entity Is",
                ResponseEnum.PlayerAlreadyInGame => "Player Already In Game",
                _ => null
            };
        }
    }
}