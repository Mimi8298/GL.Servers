namespace GL.Servers.BS.Packets.Messages.Server
{
    using GL.Servers.BS.Logic;

    using GL.Servers.Extensions.List;
    using GL.Servers.Logic.Enums;

    internal class Matchmaking_Data : Message
    {
        private Player Player;

        /// <summary>
        /// Initializes a new instance of the <see cref="Matchmaking_Data"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Player">The player.</param>
        public Matchmaking_Data(Device Device, Player Player) : base(Device)
        {
            this.Identifier = 20405;
            this.Player     = Player;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddInt(10);

            this.Data.AddInt(1);
            {
                this.Data.AddString(this.Player.Name);
                this.Data.AddBool(this.Player.Device.State == State.MATCHMAKING);

                this.Data.AddInt(this.Player.HighID);
                this.Data.AddInt(this.Player.LowID);
            }

            this.Data.AddInt(6);
        }
    }
}