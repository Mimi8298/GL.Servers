namespace GL.Servers.BS.Packets.Messages.Client
{
    using GL.Servers.BS.Core;
    using GL.Servers.BS.Core.Network;
    using GL.Servers.BS.Files.CSV_Helpers;
    using GL.Servers.BS.Logic;
    using GL.Servers.BS.Logic.Enums;
    using GL.Servers.BS.Logic.Slots.Items;
    using GL.Servers.BS.Packets.Messages.Server;

    using GL.Servers.Extensions.Binary;
    using GL.Servers.Logic.Enums;

    internal class Clan_Edit : Message
    {
        private Clan Clan;

        private string Name;
        private string Description;

        private int RequiredScore;
        private int BadgeType;
        private int BadgeID;

        private Hiring Type;

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
        /// Initializes a new instance of the <see cref="Clan_Edit"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Clan_Edit(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Clan_Edit.
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.Description    = this.Reader.ReadString();
            this.BadgeType      = this.Reader.ReadVInt();
            this.BadgeID        = this.Reader.ReadVInt();
            this.Type           = (Hiring) this.Reader.ReadVInt();
            this.RequiredScore  = this.Reader.ReadVInt();
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            if (this.ValuesAreValid)
            {
                if (this.Device.Player.ClanLowID > 0)
                {
                    this.Clan = Resources.Clans.Get(this.Device.Player.ClanHighID, this.Device.Player.ClanLowID, Constants.Database, false);

                    if (this.Clan != null)
                    {
                        Member Member;

                        if (this.Clan.Members.Entries.TryGetValue(this.Device.Player.PlayerID, out Member))
                        {
                            if (Member.Role == Role.Co_Leader || Member.Role == Role.Leader)
                            {
                                this.Clan.Description       = this.Description;
                                this.Clan.Badge             = this.Badge;
                                this.Clan.Type              = this.Type;
                                this.Clan.Required_Trophies = this.RequiredScore;

                                new Clan_Edit_OK(this.Device).Send();
                                new Clan_Info(this.Device, this.Clan).Send();
                            }
                            else
                            {
                                Logging.Error(this.GetType(), "Failed to edit the clan, player has not rights.");
                            }
                        }
                        else
                        {
                            Logging.Error(this.GetType(), "Failed to edit the clan, TryGetValue(PlayerID, out Member) returned false.");
                        }
                    }
                    else
                    {
                        Logging.Error(this.GetType(), "Clan was null when Process() has been called.");
                    }
                }
                else
                {
                    Logging.Error(this.GetType(), "Failed to edit the clan, player is not in a clan.");
                }
            }
            else
            {
                Logging.Error(this.GetType(), "Failed to edit the clan, values were not valid.");
            }
        }

        internal bool ValuesAreValid
        {
            get
            {
                bool Valid = true;

                if (string.IsNullOrWhiteSpace(this.Description) || this.Description.Length > 255)
                {
                    Valid = false;
                }

                if (this.RequiredScore < 0 || this.RequiredScore > 5000)
                {
                    Valid = false;
                }

                if (this.BadgeType != 0x08 || this.BadgeID < 0)
                {
                    Valid = false;
                }

                return Valid;
            }
        }
    }
}