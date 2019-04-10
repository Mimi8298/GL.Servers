namespace GL.Servers.CR.Logic.Commands
{
    using GL.Servers.CR.Logic.Mode;
    using GL.Servers.CR.Logic.Spells;
    using GL.Servers.DataStream;

    internal class SwapSpellsCommand : Command
    {
        internal int DeckOffset = -1;
        internal int Deck2Offset = -1;
        internal int SpellOffset = -1;

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
        /// Initializes a new instance of the <see cref="SwapSpellsCommand"/> class.
        /// </summary>
        public SwapSpellsCommand()
        {
            // SwapSpellsCommand.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SwapSpellsCommand"/> class.
        /// </summary>
        public SwapSpellsCommand(int SpellOffset, int DeckOffset, int Deck2Offset)
        {
            this.SpellOffset = SpellOffset;
            this.DeckOffset = DeckOffset;
            this.Deck2Offset = Deck2Offset;
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode(ByteStream Packet)
        {
            base.Decode(Packet);

            this.SpellOffset = Packet.ReadVInt();
            this.DeckOffset = Packet.ReadVInt();
            this.Deck2Offset = Packet.ReadVInt();
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal override void Encode(ChecksumEncoder Packet)
        {
            base.Encode(Packet);

            Packet.AddVInt(this.SpellOffset);
            Packet.AddVInt(this.DeckOffset);
            Packet.AddVInt(this.Deck2Offset);
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        internal override void Execute(GameMode GameMode)
        {
            Home Home = GameMode.Home;

            if (this.SpellOffset == -1)
            {
                if (this.DeckOffset > -1 && this.DeckOffset < 8)
                {
                    if (this.Deck2Offset > -1 && this.Deck2Offset < 8)
                    {
                        if (Home.SpellDeck[this.DeckOffset] != null)
                        {
                            if (Home.SpellDeck[this.Deck2Offset] != null)
                            {
                                if (this.DeckOffset != this.Deck2Offset)
                                {
                                    Home.SpellDeck.SwapSpells(this.DeckOffset, this.Deck2Offset);
                                    Home.SaveCurrentDeckTo(Home.SelectedDeck);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (this.SpellOffset > -1 && this.SpellOffset < Home.SpellCollection.Count)
                {
                    if (this.DeckOffset > -1 && this.DeckOffset < 8)
                    {
                        Spell Insert = Home.SpellCollection[this.SpellOffset];
                        Spell SpellInDeck = Home.SpellDeck[this.DeckOffset];

                        if (Insert != null)
                        {
                            if (SpellInDeck != null)
                            {
                                if (Home.SpellDeck.CanBeInserted(this.DeckOffset, Insert))
                                {
                                    Home.SpellDeck.SetSpell(this.DeckOffset, Insert);
                                    Home.SpellCollection.SetSpell(this.SpellOffset, SpellInDeck);

                                    Home.SaveCurrentDeckTo(Home.SelectedDeck);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}