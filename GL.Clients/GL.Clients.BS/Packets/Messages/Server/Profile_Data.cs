namespace GL.Clients.BS.Packets.Messages.Server
{
    using GL.Clients.BS.Core;
    using GL.Clients.BS.Logic;
    using GL.Clients.BS.Logic.Slots;
    using GL.Clients.BS.Logic.Slots.Items;

    using GL.Servers.Extensions.Binary;

    internal class Profile_Data : Message
    {
        internal Profile Player;

        /// <summary>
        /// Initializes a new instance of the <see cref="Profile_Data"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Profile_Data(Device Device, Reader Reader) : base(Device, Reader)
        {
            this.Player = new Profile();
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.Player.HighID      = this.Reader.ReadVInt();
            this.Player.LowID       = this.Reader.ReadVInt();

            this.Player.Username    = this.Reader.ReadString();

            this.Reader.ReadBoolean();

            int CardCount = this.Reader.ReadVInt();

            // Logging.Info(this.GetType(), "There is " + CardCount + " cards to read.");
            
            for (int i = 0; i < CardCount; i++)
            {
                Brawler Brawler     = new Brawler();

                Brawler.CardType    = this.Reader.ReadVInt(); // Card Type
                Brawler.CardID      = this.Reader.ReadVInt(); // Card ID

                if (this.Reader.ReadVInt() > 0)
                {
                    this.Reader.ReadVInt();
                }

                Brawler.Trophies    = this.Reader.ReadVInt(); // Trophies
                Brawler.Trophies2   = this.Reader.ReadVInt(); // Trophies
                Brawler.Level       = this.Reader.ReadVInt(); // Level

                this.Player.Cards.Add(Brawler);
            }

            int StatCount = this.Reader.ReadVInt();

            // Logging.Info(this.GetType(), "There is " + StatCount + " stats to read.");

            for (int i = 0; i < StatCount; i++)
            {
                this.ParseStat();
            }

            bool hasClan = this.Reader.ReadBoolean();

            if (hasClan)
            {
                this.Player.Clan        = new Clan();

                this.Player.Clan.HighID = this.Reader.ReadInt32(); // High ID
                this.Player.Clan.LowID  = this.Reader.ReadInt32(); // Low  ID

                this.Player.Clan.Name   = this.Reader.ReadString(); // Name

                this.Player.Clan.Badge  = this.Reader.ReadVInt() * 1000000; // Badge Type
                this.Player.Clan.Badge += this.Reader.ReadVInt();           // Badge ID

                this.Player.Clan.Type   = this.Reader.ReadVInt(); // Clan Type
                this.Player.Clan.MemberCount = this.Reader.ReadVInt(); // Members Count

                this.Player.Clan.Trophies = this.Reader.ReadVInt(); // Trophies
                this.Player.Clan.RequiredTrophies = this.Reader.ReadVInt(); // Required Trophies

                this.Reader.ReadVInt();

                this.Player.Clan.MemberRole = this.Reader.ReadVInt() * 1000000; // Role Type
                this.Player.Clan.MemberRole += this.Reader.ReadVInt(); // Role ID
            }

            this.Reader.ReadBoolean();
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            Logging.Info(this.GetType(), "#" + this.Device.BotId + " Username : " + this.Player.Username);

            if (Players.Get(this.Player.HighID, this.Player.LowID) != null)
            {
                Players.Save(this.Player);
            }
            else
            {
                Players.New(this.Player);
            }
        }

        /// <summary>
        /// Parses the statistics.
        /// </summary>
        internal void ParseStat()
        {
            int Type    = this.Reader.ReadVInt();
            int Value   = this.Reader.ReadVInt();

            switch (Type)
            {
                case 1:
                    this.Player.Wins            = Value;
                    break;
                case 2:
                    this.Player.Experience      = Value;
                    break;
                case 3:
                    this.Player.Trophies        = Value;
                    break;
                case 4:
                    this.Player.HighTrophies    = Value;
                    break;
                case 5:
                    // Card Count
                    break;
                case 6:
                    // Unknown
                    break;
                case 7:
                    this.Player.Thumbnail       = Value;
                    break;
                case 8:
                    this.Player.SurvTrophies    = Value;
                    break;
                case 9:
                    // Unknown
                    break;
                default:
                    Logging.Info(this.GetType(), "WARNING, UNKNOWN STAT TYPE : " + Type + ".");
                    break;
            }
        }
    }
}