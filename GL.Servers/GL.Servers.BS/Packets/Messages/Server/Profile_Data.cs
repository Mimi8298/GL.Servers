namespace GL.Servers.BS.Packets.Messages.Server
{
    using GL.Servers.BS.Core;
    using GL.Servers.BS.Files.CSV_Helpers;
    using GL.Servers.BS.Logic;
    using GL.Servers.BS.Logic.Slots.Items;

    using GL.Servers.Extensions.List;

    internal class Profile_Data : Message
    {
        private Player Player;

        /// <summary>
        /// Initializes a new instance of the <see cref="Profile_Data"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Player">The player.</param>
        public Profile_Data(Device Device, Player Player) : base(Device)
        {
            this.Identifier = 24113;
            this.Player     = Player;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddVInt(this.Player.HighID);
            this.Data.AddVInt(this.Player.LowID);

            this.Data.AddString(this.Player.Name);
            this.Data.AddBool(false);

            this.Data.AddVInt(this.Player.Deck.Count);
            {
                this.Data.AddRange(this.Player.Deck.ToBytes);
            }

            this.Data.AddVInt(8);
            {
                this.Data.AddVInt(1);
                this.Data.AddVInt(this.Player.Wins);

                this.Data.AddVInt(2);
                this.Data.AddVInt(this.Player.Info.Experience);

                this.Data.AddVInt(3);
                this.Data.AddVInt(this.Player.Info.Trophies);

                this.Data.AddVInt(4);
                this.Data.AddVInt(this.Player.Info.HighTrophies);

                this.Data.AddVInt(5);
                this.Data.AddVInt(this.Player.Deck.Count);

                this.Data.AddVInt(6);
                this.Data.AddVInt(0);

                this.Data.AddVInt(7);
                this.Data.AddVInt(this.Player.Info.Thumbnail);

                this.Data.AddVInt(8);
                this.Data.AddVInt(this.Player.Info.SurvTrophies);
            }

            if (this.Player.ClanLowID > 0)
            {
                Clan Clan = this.Player.Clan;

                if (Clan != null)
                {
                    Member Member;

                    if (Clan.Members.Entries.TryGetValue(this.Player.PlayerID, out Member))
                    {
                        this.Data.AddBool(true);

                        this.Data.AddInt(Clan.HighID);
                        this.Data.AddInt(Clan.LowID);

                        this.Data.AddString(Clan.Name);

                        this.Data.AddVInt(GlobalID.GetType(Clan.Badge));
                        this.Data.AddVInt(GlobalID.GetID(Clan.Badge));

                        this.Data.AddVInt((int) Clan.Type);

                        this.Data.AddVInt(Clan.Members.Entries.Count);
                        this.Data.AddVInt(Clan.Trophies);
                        this.Data.AddVInt(Clan.Required_Trophies);
                        this.Data.AddVInt(0);

                        this.Data.AddVInt(0x19); // Role Type
                        this.Data.AddVInt((int) Member.Role); // Role ID
                    }
                    else
                    {
                        Logging.Error(this.GetType(), "Member was null.");

                        this.Data.AddBool(false);
                        this.Data.AddBool(false);
                    }
                }
                else
                {
                    Logging.Error(this.GetType(), "Clan was null.");

                    this.Data.AddBool(false);
                    this.Data.AddBool(false);
                }
            }
            else
            {
                this.Data.AddBool(false);
                this.Data.AddBool(false);
            }
        }
    }
}