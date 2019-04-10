namespace GL.Servers.BS.Packets.Messages.Client
{
    using GL.Servers.BS.Core;
    using GL.Servers.BS.Core.Network;
    using GL.Servers.BS.Files;
    using GL.Servers.BS.Files.CSV_Helpers;
    using GL.Servers.BS.Files.CSV_Logic;
    using GL.Servers.BS.Logic;
    using GL.Servers.BS.Logic.Enums;
    using GL.Servers.BS.Packets.Messages.Server;

    using GL.Servers.Extensions.Binary;
    using GL.Servers.Logic.Enums;

    using Resources = GL.Servers.BS.Core.Resources;

    internal class Clan_Create : Message
    {
        private string Name;
        private string Description;

        private int BadgeType;
        private int BadgeID;
        private int RequiredTrophies;

        private Hiring Hiring;

        /// <summary>
        /// Gets the gold cost for the alliance creation.
        /// </summary>
        private int GoldCost
        {
            get
            {
                return (CSV.Tables.Get(Gamefile.Globals).GetData("ALLIANCE_CREATE_COST") as Globals).NumberValue;
            }
        }

        /// <summary>
        /// Gets the badge global identifier.
        /// </summary>
        private int Badge
        {
            get
            {
                return GlobalID.Create(this.BadgeType, this.BadgeID);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Clan_Create"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Clan_Create(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Clan_Create.
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.Name               = this.Reader.ReadString();
            this.Description        = this.Reader.ReadString();

            this.BadgeType          = this.Reader.ReadVInt(); // Badge Type
            this.BadgeID            = this.Reader.ReadVInt(); // Badge ID

            this.Hiring             = (Hiring) this.Reader.ReadVInt();
            this.RequiredTrophies   = this.Reader.ReadVInt();
        }

        /// <summary>
        /// Processes this message.
        /// </summary>
        internal override void Process()
        {
            if (!this.ValuesAreValid)
            {
                return;
            }

            this.Device.Player.Resources.Minus(Resource.Gold, this.GoldCost);

            Clan Clan               = new Clan
            {
                HighID              = Constants.ServerID,
                LowID               = 0,

                Name                = this.Name,
                Description         = this.Description,
                Badge               = this.Badge,
                Type                = this.Hiring,
                Required_Trophies   = this.RequiredTrophies
            };

            if (Clan.Members.TryAdd(this.Device.Player))
            {

                Clan                = Resources.Clans.New(Clan);

                this.Device.Player.ClanHighID   = Clan.HighID;
                this.Device.Player.ClanLowID    = Clan.LowID;

                if (Clan != null)
                {
                    new Clan_Create_OK(this.Device).Send();
                    new Clan_Data(this.Device, Clan).Send();
                    new Clan_Stream(this.Device, Clan).Send();
                    new Clan_Info(this.Device, Clan).Send();
                }
                else
                {
                    Logging.Error(this.GetType(), this.Device, "Error when creating a clan, the clan failed to save on the database.");
                }
            }
            else
            {
                Logging.Error(this.GetType(), "Failed to create a clan, TryAdd(Player) returned false.");
            }
        }

        /// <summary>
        /// Gets a value indicating whether the values are valid.
        /// </summary>
        internal bool ValuesAreValid
        {
            get
            {
                bool Valid = true;

                if (string.IsNullOrEmpty(this.Name) || this.Name.Length > 15)
                {
                    Logging.Error(this.GetType(), this.Device, "Error when creating a clan, the name is either empty or too long.");
                    Valid = false;
                }

                if (this.RequiredTrophies < 0)
                {
                    Logging.Error(this.GetType(), this.Device, "Error when creating a clan, the required trophies value is not correct.");
                    Valid = false;
                }

                if (this.Device.Player.Resources.Get(Resource.Gold) < this.GoldCost)
                {
                    Logging.Error(this.GetType(), this.Device, "Error when creating a clan, insufficent resources.");
                    Valid = false;
                }

                return Valid;
            }
        }
    }
}