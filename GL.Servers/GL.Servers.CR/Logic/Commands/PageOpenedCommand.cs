namespace GL.Servers.CR.Logic.Commands
{
    using GL.Servers.CR.Logic.Mode;
    using GL.Servers.DataStream;

    internal class PageOpenedCommand : Command
    {
        internal int Page;

        /// <summary>
        /// Gets the type of this command.
        /// </summary>
        internal override int Type
        {
            get
            {
                return 527;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PageOpenedCommand"/> class.
        /// </summary>
        public PageOpenedCommand()
        {
            // PageOpenedCommand.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PageOpenedCommand"/> class.
        /// </summary>
        public PageOpenedCommand(int Page)
        {
            this.Page = Page;
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode(ByteStream Packet)
        {
            base.Decode(Packet);

            this.Page = Packet.ReadVInt();
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal override void Encode(ChecksumEncoder Packet)
        {
            base.Encode(Packet);

            Packet.AddVInt(this.Page);
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        internal override void Execute(GameMode GameMode)
        {
            Home Home = GameMode.Home;

            if (Home != null)
            {
                if (this.Page > 0 && this.Page <= 4)
                {
                    Home.SetPageOpened(this.Page);
                }
            }
        }
    }
}