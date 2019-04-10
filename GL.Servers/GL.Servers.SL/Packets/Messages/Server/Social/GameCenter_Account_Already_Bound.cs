namespace GL.Servers.SL.Packets.Messages.Server
{
    using GL.Servers.Extensions.List;
    using GL.Servers.SL.Logic;
    using GL.Servers.SL.Logic.Avatar;

    internal class GameCenter_Account_Already_Bound : Message
    {
        private Player Player;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameCenter_Account_Already_Bound"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Command">The command.</param>
        public GameCenter_Account_Already_Bound(Device Device, Player Player) : base(Device)
        {
            this.Identifier = 24212;
            this.Player     = Player;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddString(null);

            if (this.Player.PlayerID != 0)
            {
                this.Data.Add(1);
                this.Data.AddLong(this.Player.PlayerID);
            }
            else this.Data.Add(0);

            this.Data.AddString(this.Player.Token);
            this.Data.AddRange(this.Player.ToBytes);
        }
    }
}