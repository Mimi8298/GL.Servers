namespace GL.Servers.CoC.Packets.Debugs
{
    using GL.Servers.CoC.Core;
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Core.Network;
    using GL.Servers.CoC.Packets.Messages.Server.Account;
    using GL.Servers.Extensions;
    using GL.Servers.Logic.Enums;

    internal class Fast_Forward_Debug : Debug
    {
        internal int Time;

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
        /// Initializes a new instance of the <see cref="Fast_Forward_Debug"/> class.
        /// </summary>
        public Fast_Forward_Debug(params string[] Parameters) : base(Parameters)
        {
            
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode()
        {
            base.Decode();
            int.TryParse(this.NextParameter, out this.Time);
        }

        internal override async void Process()
        {
            if (!string.IsNullOrEmpty(this.PlayerTag))
            {
                if (this.Time > 0)
                {
                    Player Player = await Resources.Accounts.LoadPlayerAsync(this.PlayerHighID, this.PlayerLowID);

                    if (Player != null)
                    {
                        Player.Level.FastForwardTime(this.Time);

                        if (Player.Connected)
                        {
                            new Disconnected_Message(Player.Level.GameMode.Device, 0).Send();
                        }
                    }
                }
            }
        }
    }
}