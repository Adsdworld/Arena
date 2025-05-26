namespace Script.Game
{
    public enum GameNameEnum
    {
        Game1,
        Game2,
        Game3,
        Game4,
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
