namespace GL.Servers.CR.Logic.Commands.Server
{
    using GL.Servers.CR.Logic.Mode;
    using GL.Servers.DataStream;

    internal class TransactionsRevokedCommand : ServerCommand
    {
        internal int Diamonds;

        /// <summary>
        /// Gets the type of this command.
        /// </summary>
        internal override int Type
        {
            get
            {
                return 215;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionsRevokedCommand"/> class.
        /// </summary>
        public TransactionsRevokedCommand()
        {
            // TransactionsRevokedCommand.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionsRevokedCommand"/> class.
        /// </summary>
        public TransactionsRevokedCommand(int Diamonds)
        {
            this.Diamonds = Diamonds;
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode(ByteStream Packet)
        {
            base.Decode(Packet);
            this.Diamonds = Packet.ReadInt();
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal override void Encode(ChecksumEncoder Packet)
        {
            base.Encode(Packet);
            Packet.AddInt(this.Diamonds);
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        internal override void Execute(GameMode GameMode)
        {
            Player Player = GameMode.Player;

            if (Player != null)
            {
                Player.UseDiamonds(this.Diamonds);
            }
        }
    }
}