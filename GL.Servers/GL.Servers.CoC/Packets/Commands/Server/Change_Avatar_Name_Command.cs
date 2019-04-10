namespace GL.Servers.CoC.Packets.Commands.Server
{
    using GL.Servers.CoC.Logic;
    using GL.Servers.DataStream;
    using GL.Servers.Extensions.Binary;
    using GL.Servers.Extensions.List;

    internal class Change_Avatar_Name_Command : ServerCommand
    {
        internal string AvatarName;
        internal int ChangeNameCount;

        /// <summary>
        /// Gets a value indicating the command type.
        /// </summary>
        internal override int Type
        {
            get
            {
                return 3;
            }
        }

        internal override ChecksumEncoder Checksum
        {
            get
            {
                ChecksumEncoder Encoder = new ChecksumEncoder(true);

                Encoder.AddString(this.AvatarName);
                // Encoder.AddInt(this.ChangeNameCount);

                return Encoder;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Change_Avatar_Name_Command"/> class.
        /// </summary>
        public Change_Avatar_Name_Command() : base()
        {
           
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Change_Avatar_Name_Command"/> class.
        /// </summary>
        public Change_Avatar_Name_Command(string AvatarName, int ChangeNameCount) : base()
        {
            this.AvatarName      = AvatarName;
            this.ChangeNameCount = ChangeNameCount;
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode(Reader Reader)
        {
            this.AvatarName      = Reader.ReadString();
            this.ChangeNameCount = Reader.ReadInt32();

            base.Decode(Reader);
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal override void Encode(ByteWriter Packet)
        {
            Packet.AddString(this.AvatarName);
            Packet.AddInt(this.ChangeNameCount);

            base.Encode(Packet);
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        internal override void Execute(Level Level)
        {
            if (this.AvatarName != null)
            {
                Level.Player.Name            = this.AvatarName;
                Level.Player.ChangeNameCount = this.ChangeNameCount;

                if (Level.Player.NameSetByUser)
                {
                    Level.Player.ChangeNameCount++;
                }
                else
                    Level.Player.NameSetByUser = true;
            }

            Level.GameMode.CommandManager.ChangeNameOnGoing = false;
        }
    }
}