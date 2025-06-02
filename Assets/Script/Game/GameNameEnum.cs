using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Script.Game
{
    public enum GameNameEnum
    {
        [EnumMember(Value = "Game 1")]
        Game1,
        
        [EnumMember(Value = "Game 2")]
        Game2,
        
        [EnumMember(Value = "Game 3")]
        Game3,
        
        [EnumMember(Value = "Game 4")]
        Game4,
        
        [EnumMember(Value = "Game 5")]
        Game5,
    }

    public static class GameNameEnumExtensions
    {
        public static string GetGameName(this GameNameEnum gameNameEnum)
        {
            return gameNameEnum switch
            {
                GameNameEnum.Game1 => "Game 1",
                GameNameEnum.Game2 => "Game 2",
                GameNameEnum.Game3 => "Game 3",
                GameNameEnum.Game4 => "Game 4",
                GameNameEnum.Game5 => "Game 5",
                _ => null
            };
        }
    }
}
