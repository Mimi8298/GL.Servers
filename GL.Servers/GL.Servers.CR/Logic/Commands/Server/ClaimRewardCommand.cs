namespace GL.Servers.CR.Logic.Commands.Server
{
    using GL.Servers.CR.Logic.Mode;
    using GL.Servers.DataStream;

    internal class ClaimRewardCommand : ServerCommand
    {
        internal string Name;
        internal bool NameSetByUser;
        internal int NameChangeState;

        /// <summary>
        /// Gets the type of this command.
        /// </summary>
        internal override int Type
        {
            get
            {
                return 210;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimRewardCommand"/> class.
        /// </summary>
        public ClaimRewardCommand()
        {
            // ChangeAvatarNameCommand.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimRewardCommand"/> class.
        /// </summary>
        public ClaimRewardCommand(string Name, bool NameSetByUser, int NameChangeState)
        {
            this.Name = Name;
            this.NameSetByUser = NameSetByUser;
            this.NameChangeState = NameChangeState;
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode(ByteStream Packet)
        {
            this.Name = Packet.ReadString();
            this.NameChangeState = Packet.ReadVInt();
            this.NameSetByUser = Packet.ReadBoolean();

            base.Decode(Packet);
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal override void Encode(ChecksumEncoder Packet)
        {
            Packet.AddString(this.Name);
            Packet.AddVInt(this.NameChangeState);
            Packet.AddBoolean(this.NameSetByUser);

            base.Encode(Packet);
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        internal override void Execute(GameMode GameMode)
        {
            // Execute.
        }
    }
}