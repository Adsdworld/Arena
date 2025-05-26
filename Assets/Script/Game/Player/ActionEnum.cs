namespace Script.Game.Player
{
    public enum ActionEnum
    {
        Login,
        CreateGame,
        Join,
    }

    public static class ActionEnumExtensions
    {
        public static string GetAction(this ActionEnum actionEnum)
        {
            return actionEnum switch
            {
                ActionEnum.Login => "Login",
                ActionEnum.CreateGame => "Create Game",
                ActionEnum.Join => "Join",
                _ => null
            };
        }
    }
}
