namespace GL.Servers.CoC.Packets.Commands.Server
{
    using GL.Servers.CoC.Logic;
    using GL.Servers.DataStream;
    using GL.Servers.Extensions.Binary;
    using GL.Servers.Extensions.List;

    internal class Alliance_Settings_Changed_Command : ServerCommand
    {
        internal long AllianceID;
        internal int AllianceBadge;

        /// <summary>
        /// Gets a value indicating the command type.
        /// </summary>
        internal override int Type
        {
            get
            {
                return 6;
            }
        }

        internal override ChecksumEncoder Checksum
        {
            get
            {
                ChecksumEncoder Encoder = new ChecksumEncoder(true);
                
                Encoder.AddLong(this.AllianceID);
                Encoder.AddInt(this.AllianceBadge);

                return Encoder;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Alliance_Settings_Changed_Command"/> class.
        /// </summary>
        public Alliance_Settings_Changed_Command() : base()
        {
            // Alliance_Settings_Changed_Command.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Alliance_Settings_Changed_Command"/> class.
        /// </summary>
        public Alliance_Settings_Changed_Command(long AllianceID, int AllianceBadge) : base()
        {
            this.AllianceID    = AllianceID;
            this.AllianceBadge = AllianceBadge;
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode(Reader Reader)
        {
            this.AllianceID    = Reader.ReadInt64();
            this.AllianceBadge = Reader.ReadInt32();

            base.Decode(Reader);
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal override void Encode(ByteWriter Packet)
        {
            Packet.AddLong(this.AllianceID);
            Packet.AddInt(this.AllianceBadge);

            base.Encode(Packet);
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        internal override void Execute(Level Level)
        {
            Level.Player.AllianceHighID = (int) (this.AllianceID >> 32);
            Level.Player.AllianceLowID  = (int) this.AllianceID;
        }
    }
}