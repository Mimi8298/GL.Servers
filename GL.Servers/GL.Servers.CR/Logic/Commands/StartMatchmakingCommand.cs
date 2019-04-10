namespace GL.Servers.CR.Logic.Commands
{
    using GL.Servers.CR.Core;
    using GL.Servers.CR.Logic.Mode;

    internal class StartMatchmakingCommand : Command
    {
        /// <summary>
        /// Gets the type of this command.
        /// </summary>
        internal override int Type
        {
            get
            {
                return 525;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StartMatchmakingCommand"/> class.
        /// </summary>
        public StartMatchmakingCommand()
        {
            // StartMatchmakingCommand.
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        internal override void Execute(GameMode GameMode)
        {
            Home Home = GameMode.Home;
            Player Player = GameMode.Player;

            if (Home != null)
            {
                if (Player != null)
                {
                    if (Player.Arena.TrainingCamp)
                    {
                        return;
                    }

                    Resources.BattleManager.AddPlayer(GameMode);
                }
            }
        }
    }
}