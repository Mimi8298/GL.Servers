namespace GL.Servers.BS.Packets.Messages.Server
{
    using GL.Servers.BS.Logic;
    using GL.Servers.BS.Logic.Slots.Items;
    using GL.Servers.Extensions.List;

    internal class Udp_Connection_Info : Message
    {
        private const int Port = 9339;
        private const string SIP = "192.168.1.13";

        private Battle Battle;

        /// <summary>
        /// Initializes a new instance of the <see cref="Sector_PC"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Udp_Connection_Info(Device Device, Battle Battle) : base(Device)
        {
            this.Identifier = 24112;
            this.Battle = Battle;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddVInt(Udp_Connection_Info.Port);
            this.Data.AddString(Udp_Connection_Info.SIP);

            this.Data.AddInt(10);
            {
                this.Data.Add(0); // HighID
                this.Data.AddInt(this.Battle.LowID);

                this.Data.Add((byte) this.Device.Player.HighID);
                this.Data.AddInt(this.Device.Player.LowID);
            }

            this.Data.AddString("hi-kfP1ub1HxoZFsLLVNmxD0zYxWWMmtpZY_EwE88uw");
        }
    }
}