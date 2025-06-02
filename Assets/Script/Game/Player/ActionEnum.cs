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
        
        /// <summary>
        /// Require gameName
        /// </summary>
        CloseGame,
    }

    public static class ActionEnumExtensions
    {
        public static string GetActionName(this ActionEnum actionEnum)
        {
            return actionEnum switch
            {
                ActionEnum.Login => "Login",
                ActionEnum.CreateGame => "Create Game",
                ActionEnum.Join => "Join",
                ActionEnum.CloseGame => "Close Game",
                _ => null
            };
        }
    }
}
