namespace GL.Servers.CR.Logic.Commands
{
    using GL.Servers.CR.Extensions.Game;
    using GL.Servers.CR.Logic.Mode;
    using GL.Servers.CR.Logic.Spells;
    using GL.Servers.DataStream;

    internal class SelectDeckCommand : Command
    {
        internal int DeckIdx;

        /// <summary>
        /// Gets the type of this command.
        /// </summary>
        internal override int Type
        {
            get
            {
                return 500;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectDeckCommand"/> class.
        /// </summary>
        public SelectDeckCommand()
        {
            // SelectDeckCommand.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectDeckCommand"/> class.
        /// </summary>
        public SelectDeckCommand(int DeckIdx)
        {
            this.DeckIdx = DeckIdx;
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode(ByteStream Packet)
        {
            base.Decode(Packet);

            this.DeckIdx = Packet.ReadVInt();
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal override void Encode(ChecksumEncoder Packet)
        {
            base.Encode(Packet);
            
            Packet.AddVInt(this.DeckIdx);
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        internal override void Execute(GameMode GameMode)
        {
            if (Globals.MultipleDecks)
            {
                if (this.DeckIdx > -1 && this.DeckIdx < 5)
                {
                    GameMode.Home.SetSelectedDeck(this.DeckIdx);
                }   
            }
        }
    }
}