namespace GL.Servers.CR.Logic.Commands.Server
{
    using GL.Servers.CR.Logic.Mode;
    using GL.Servers.DataStream;

    internal class DiamondsAddedCommand : ServerCommand
    {
        internal int Diamonds;

        /// <summary>
        /// Gets the type of this command.
        /// </summary>
        internal override int Type
        {
            get
            {
                return 202;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiamondsAddedCommand"/> class.
        /// </summary>
        public DiamondsAddedCommand()
        {
            // DiamondsAddedCommand.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiamondsAddedCommand"/> class.
        /// </summary>
        public DiamondsAddedCommand(int Diamonds)
        {
            this.Diamonds = Diamonds;
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode(ByteStream Packet)
        {
            Packet.ReadBoolean();
            this.Diamonds = Packet.ReadVInt();
            Packet.ReadString();

            base.Decode(Packet);
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal override void Encode(ChecksumEncoder Packet)
        {
            Packet.AddBoolean(false);
            Packet.AddVInt(this.Diamonds);
            Packet.AddString(null);

            base.Encode(Packet);
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        internal override void Execute(GameMode GameMode)
        {
            Player Player = GameMode.Player;

            if (Player != null)
            {
                if (this.Diamonds > 0)
                {
                    GameMode.Player.AddFreeDiamonds(this.Diamonds);
                }
            }
        }
    }
}