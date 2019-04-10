namespace GL.Servers.BS.Packets.Commands.Client
{
    using GL.Servers.BS.Core;
    using GL.Servers.BS.Files;
    using GL.Servers.BS.Files.CSV_Logic;
    using GL.Servers.BS.Logic;
    using GL.Servers.BS.Logic.Enums;
    using GL.Servers.BS.Logic.Slots.Items;
    using GL.Servers.Extensions.Binary;

    internal class Upgrade_Brawler : Command
    {
        private int CardType;
        private int CardID;

        /// <summary>
        /// Gets the global identifier.
        /// </summary>
        internal int GlobalID
        {
            get
            {
                return (this.CardType * 1000000) + this.CardID;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Upgrade_Brawler"/> class.
        /// </summary>
        /// <param name="Reader">The reader.</param>
        /// <param name="Device">The device.</param>
        /// <param name="CommandID">The command identifier.</param>
        public Upgrade_Brawler(Reader Reader, Device Device, int CommandID) : base(Reader, Device, CommandID)
        {
            // Upgrade_Brawler.
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode()
        {
            this.ReadHeader();

            this.CardType   = this.Reader.ReadVInt();
            this.CardID     = this.Reader.ReadVInt();
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            Cards Card = CSV.Tables.Get(Gamefile.Cards).GetDataWithID(this.GlobalID) as Cards;

            if (Card != null)
            {
                Competence Competence   = new Competence(this.GlobalID);
                Characters Character    = CSV.Tables.Get(Gamefile.Characters).GetData(Card.Target) as Characters;

                if (Character != null)
                {
                    this.CardType   = Character.GetDataType();
                    this.CardID     = Character.GetID();

                    if (this.Device.Player.Deck.ContainsKey(this.GlobalID))
                    {
                        Card CardData = this.Device.Player.Deck[this.GlobalID];

                        if (CardData.Competences.ContainsKey(Competence.GlobalID))
                        {
                            CardData.Competences[Competence.GlobalID].Upgrade();
                        }
                        else
                        {
                            CardData.Competences.Add(Competence);
                        }
                    }
                    else
                    {
                        Card CardData = new Card(this.GlobalID);
                        this.Device.Player.Deck.Add(CardData);
                        CardData.Competences.Add(Competence);
                    }
                }
                else
                {
                    Logging.Error(this.GetType(), "Error when processing a card upgrade, the Character instance was null for the card n°" + this.GlobalID + ".");
                }
            }
            else
            {
                Logging.Error(this.GetType(), "Error when upgrading a card, with id " + this.GlobalID + ".");
            }
        }
    }
}