namespace Script.Game.Player
{
    public enum ActionEnum
    {
        /// <summary>
        /// Require uuid
        /// </summary>
        Login,
        
        /// <summary>
        /// Require gameName
        /// </summary>
        CreateGame,
        
        /// <summary>
        /// Require gameName, uuid
        /// </summary>
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
