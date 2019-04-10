namespace GL.Servers.CoC.Packets.Commands.Server
{
    using GL.Servers.CoC.Logic;
    using GL.Servers.DataStream;
    using GL.Servers.Extensions.Binary;
    using GL.Servers.Extensions.List;

    internal class Leave_Alliance_Command : ServerCommand
    {
        internal long AllianceID;

        /// <summary>
        /// Gets a value indicating the command type.
        /// </summary>
        internal override int Type
        {
            get
            {
                return 2;
            }
        }

        internal override ChecksumEncoder Checksum
        {
            get
            {
                ChecksumEncoder Encoder = new ChecksumEncoder(true);

                Encoder.AddLong(this.AllianceID);

                return Encoder;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Leave_Alliance_Command"/> class.
        /// </summary>
        public Leave_Alliance_Command() : base()
        {
            // Leave_Alliance_Command.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Leave_Alliance_Command"/> class.
        /// </summary>
        public Leave_Alliance_Command(long AllianceID) : base()
        {
            this.AllianceID = AllianceID;
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode(Reader Reader)
        {
            this.AllianceID = Reader.ReadInt64();

            base.Decode(Reader);
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal override void Encode(ByteWriter Packet)
        {
            Packet.AddLong(this.AllianceID);

            base.Encode(Packet);
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        internal override void Execute(Level Level)
        {
            Level.Player.AllianceHighID = 0;
            Level.Player.AllianceLowID = 0;
            Level.Player.AllianceMember = null;
            Level.Player.Alliance = null;
        }
    }
}