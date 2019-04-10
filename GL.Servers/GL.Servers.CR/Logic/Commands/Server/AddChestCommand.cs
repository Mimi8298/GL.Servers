namespace GL.Servers.CR.Logic.Commands.Server
{
    using GL.Servers.CR.Extensions.Helper;
    using GL.Servers.CR.Files.Logic;
    using GL.Servers.CR.Logic.Mode;
    using GL.Servers.DataStream;

    internal class AddChestCommand : ServerCommand
    {
        internal bool Add;
        internal ArenaData ArenaData;
        internal string Name;

        /// <summary>
        /// Gets the type of this command.
        /// </summary>
        internal override int Type
        {
            get
            {
                return 211;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddChestCommand"/> class.
        /// </summary>
        public AddChestCommand(bool Add, ArenaData ArenaData, string Name)
        {
            this.Add = Add;
            this.ArenaData = ArenaData;
            this.Name = Name;
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode(ByteStream Packet)
        {
            this.Add = Packet.ReadBoolean();
            Packet.ReadBoolean();
            Packet.ReadBoolean();
            this.ArenaData = Packet.DecodeData<ArenaData>();
            this.Name = Packet.ReadString();

            base.Decode(Packet);
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal override void Encode(ChecksumEncoder Packet)
        {
            Packet.AddBoolean(this.Add);
            Packet.AddBoolean(false);
            Packet.AddBoolean(false);
            Packet.EncodeData(this.ArenaData);
            Packet.AddString(this.Name);

            base.Encode(Packet);
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        internal override void Execute(GameMode GameMode)
        {
            // TODO Implement AddChestCommand::execute();
        }
    }
}