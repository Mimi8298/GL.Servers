namespace GL.Servers.CR.Logic.Commands
{
    using GL.Servers.CR.Logic.Mode;

    internal class UpdateLastShownLevelUpCommand : Command
    {
        /// <summary>
        /// Gets the type of this command.
        /// </summary>
        internal override int Type
        {
            get
            {
                return 507;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateLastShownLevelUpCommand"/> class.
        /// </summary>
        public UpdateLastShownLevelUpCommand()
        {
            // UpdateLastShownLevelUpCommand.
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        internal override void Execute(GameMode GameMode)
        {
            Home Home = GameMode.Home;

            if (Home != null)
            {
                Player Player = GameMode.Player;

                if (Player != null)
                {
                    if (Player.ExpLevel > Home.LastShownLevelUp)
                    {
                        Home.SetLastShownLevelUp(Player.ExpLevel);
                    }
                }
            }
        }
    }
}