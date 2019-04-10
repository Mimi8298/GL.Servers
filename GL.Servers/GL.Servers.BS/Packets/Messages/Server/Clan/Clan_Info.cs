namespace GL.Servers.BS.Packets.Messages.Server
{
    using GL.Servers.BS.Files.CSV_Helpers;
    using GL.Servers.BS.Logic;

    using GL.Servers.Extensions.List;

    internal class Clan_Info : Message
    {
        private Clan Clan;

        /// <summary>
        /// Initializes a new instance of the <see cref="Clan_Info"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Clan_Info(Device Device, Clan Clan) : base(Device)
        {
            this.Identifier = 24399;
            this.Clan       = Clan;
        }

        // 02  01  19  01  00-00-00-00  00-00-15-DD  00-00-00-08  42-75-73-68-2D-6D-65-6E  08  05  01  02  A4-03  00-00

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            if (this.Clan != null)
            {
                this.Data.AddVInt(this.Clan.Members.Entries.Count); // Online Faggot

                this.Data.AddBool(true);
                {
                    this.Data.AddVInt(25);
                    this.Data.AddVInt((int) this.Clan.Members.Entries[this.Device.Player.LowID].Role);

                    this.Data.AddInt(this.Clan.HighID);
                    this.Data.AddInt(this.Clan.LowID);

                    this.Data.AddString(this.Clan.Name);

                    this.Data.AddVInt(GlobalID.GetType(this.Clan.Badge));
                    this.Data.AddVInt(GlobalID.GetID(this.Clan.Badge));

                    this.Data.AddVInt((int) this.Clan.Type);

                    this.Data.AddVInt(this.Clan.Members.Entries.Count);

                    this.Data.AddVInt(this.Clan.Trophies);
                    this.Data.AddVInt(this.Clan.Required_Trophies);

                    this.Data.AddVInt(0);
                }
            }
            else
            {
                this.Data.AddVInt(0);
                this.Data.AddVInt(0);
            }
        }
    }
}