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
        
        /// <summary>
        /// Require gameName, uuid
        /// </summary>
        WhatIsMyEntity,
        
        /// <summary>
        /// 
        /// </summary>
        CastQ,
        
        /// <summary>
        /// 
        /// </summary>
        CastW,
        
        /// <summary>
        /// 
        /// </summary>
        CastE,
        
        /// <summary>
        /// 
        /// </summary>
        CastR,
        
        /// <summary>
        ///
        /// </summary>
        PlayerStateUpdate,
        
        
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
                ActionEnum.WhatIsMyEntity => "What Is My Entity",
                ActionEnum.PlayerStateUpdate => "Player State Update",
                ActionEnum.CastQ => "Cast Q",
                ActionEnum.CastW => "Cast W",
                ActionEnum.CastE => "Cast E",
                ActionEnum.CastR => "Cast R",
                _ => null
            };
        }
    }
}
