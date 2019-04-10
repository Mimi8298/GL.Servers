namespace GL.Servers.CoC.Packets.Debugs
{
    using GL.Servers.CoC.Core;
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Packets.Commands.Server;
    using GL.Servers.Logic.Enums;

    internal class Add_Diamonds_Debug : Debug
    {
        internal int Count;

        /// <summary>
        /// Gets a value indicating the required rank.
        /// </summary>
        internal override Rank RequiredRank
        {
            get
            {
                return Rank.Moderator;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Add_Diamonds_Debug"/> class.
        /// </summary>
        public Add_Diamonds_Debug(params string[] Parameters) : base(Parameters)
        {
            // Add_Diamonds_Debug.
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode()
        {
            base.Decode();
            int.TryParse(this.NextParameter, out this.Count);
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override async void Process()
        {
            if (this.Count > 0)
            {
                if (!string.IsNullOrEmpty(this.PlayerTag))
                {
                    Player Player = await Resources.Accounts.LoadPlayerAsync(this.PlayerHighID, this.PlayerLowID);

                    if (Player != null)
                    {
                        if (Player.Connected)
                        {
                            Player.Level.GameMode.CommandManager.AddCommand(new Diamonds_Added_Command(false, this.Count));
                        }
                        else
                        {
                            Player.AddDiamonds(this.Count);
                        }
                    }
                }
            }
        }
    }
}