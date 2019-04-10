namespace GL.Servers.CoC.Packets.Commands.Server
{
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Logic.Clan;
    using GL.Servers.CoC.Logic.Clan.Items;
    using GL.Servers.DataStream;
    using GL.Servers.Extensions.Binary;
    using GL.Servers.Extensions.List;

    internal class Join_Alliance_Command : ServerCommand
    {
        internal long AllianceID;

        internal string AllianceName;

        internal int AllianceBadge;
        internal int AllianceLevel;

        internal bool CreateAlliance;

        /// <summary>
        /// Gets a value indicating the command type.
        /// </summary>
        internal override int Type
        {
            get
            {
                return 1;
            }
        }

        internal override ChecksumEncoder Checksum
        {
            get
            {
                ChecksumEncoder Encoder = new ChecksumEncoder(true);

                Encoder.AddLong(this.AllianceID);
                Encoder.AddString(this.AllianceName);
                Encoder.AddInt(this.AllianceBadge);
                Encoder.AddBoolean(this.CreateAlliance);
                Encoder.AddInt(this.AllianceLevel);

                return Encoder;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Join_Alliance_Command"/> class.
        /// </summary>
        public Join_Alliance_Command() : base()
        {
           
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Join_Alliance_Command"/> class.
        /// </summary>
        public Join_Alliance_Command(long AllianceID, string AllianceName, int AllianceBadge, int AllianceLevel, bool CreateAlliance) : base()
        {
            this.AllianceID = AllianceID;
            this.AllianceName = AllianceName;
            this.AllianceBadge = AllianceBadge;
            this.AllianceLevel = AllianceLevel;
            this.CreateAlliance = CreateAlliance;
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode(Reader Reader)
        {
            this.AllianceID = Reader.ReadInt64();
            this.AllianceName = Reader.ReadString();
            this.AllianceBadge = Reader.ReadInt32();
            this.CreateAlliance = Reader.ReadBoolean();
            this.AllianceLevel = Reader.ReadInt32();

            base.Decode(Reader);
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal override void Encode(ByteWriter Packet)
        {
            Packet.AddLong(this.AllianceID);
            Packet.AddString(this.AllianceName);
            Packet.AddInt(this.AllianceBadge);
            Packet.AddBoolean(this.CreateAlliance);
            Packet.AddInt(this.AllianceLevel);

            base.Encode(Packet);
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        internal override void Execute(Level Level)
        {
            if (Core.Resources.Clans.TryGetValue(this.AllianceID, out Alliance Alliance))
            {
                if (Alliance.Members.Slots.TryGetValue(Level.Player.PlayerID, out Member Member))
                {
                    Level.Player.SetAlliance(Alliance, Member);
                }
            }
        }
    }
}