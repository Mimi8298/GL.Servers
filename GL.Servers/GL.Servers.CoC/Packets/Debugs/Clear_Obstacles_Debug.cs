namespace GL.Servers.CoC.Packets.Debugs
{
    using GL.Servers.CoC.Core;
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Core.Network;
    using GL.Servers.CoC.Packets.Messages.Server.Account;
    using GL.Servers.Logic.Enums;

    internal class Clear_Obstacles_Debug : Debug
    {
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
        /// Initializes a new instance of the <see cref="Clear_Obstacles_Debug"/> class.
        /// </summary>
        /// <param name="Device"></param>
        /// <param name="Parameters"></param>
        public Clear_Obstacles_Debug(params string[] Parameters) : base(Parameters)
        {
            
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override async void Process()
        {
            if (!string.IsNullOrEmpty(this.PlayerTag))
            {
                Player Player = await Resources.Accounts.LoadPlayerAsync(this.PlayerHighID, this.PlayerLowID);

                if (Player != null)
                {
                    Player.Level.GameObjectManager.GameObjects[3][0].Clear();
                    Player.Level.GameObjectManager.GameObjects[3][1].Clear();

                    if (Player.Connected)
                    {
                        new Disconnected_Message(Player.Level.GameMode.Device, 0).Send();
                    }
                }
            }
        }
    }
}