namespace GL.Servers.BS.Packets.Messages.Server
{
    using System.Linq;

    using GL.Servers.BS.Files.CSV_Helpers;
    using GL.Servers.BS.Logic;
    using GL.Servers.BS.Logic.Slots.Items;
    using GL.Servers.Extensions.List;

    internal class Clan_Data : Message
    {
        private Clan Clan;

        /// <summary>
        /// Initializes a new instance of the <see cref="Clan_Data"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Clan">The clan.</param>
        public Clan_Data(Device Device, Clan Clan) : base(Device)
        {
            this.Identifier = 24301;
            this.Clan       = Clan;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddInt(this.Clan.HighID);
            this.Data.AddInt(this.Clan.LowID);

            this.Data.AddString(this.Clan.Name);

            this.Data.AddVInt(GlobalID.GetType(this.Clan.Badge));
            this.Data.AddVInt(GlobalID.GetID(this.Clan.Badge));

            this.Data.AddVInt((int)this.Clan.Type);

            this.Data.AddVInt(this.Clan.Members.Entries.Count);
            this.Data.AddVInt(this.Clan.Trophies);

            this.Data.AddVInt(this.Clan.Required_Trophies);
            this.Data.AddVInt(0);

            this.Data.AddString(this.Clan.Description);

            this.Data.AddVInt(this.Clan.Members.Entries.Count);

            foreach (Member Member in this.Clan.Members.Entries.Values.ToArray())
            {
                this.Data.AddInt(Member.HighID);
                this.Data.AddInt(Member.LowID);

                this.Data.AddString(Member.Username);

                this.Data.AddVInt((int) Member.Role);

                this.Data.AddVInt(Member.Level);
                this.Data.AddVInt(Member.Trophies);

                this.Data.AddVInt(28); // Thumbnail Type
                this.Data.AddVInt(Member.Thumbnail);
            }

            // 01  00-00-00-00  00-00-39-84  00-00-00-09-EC-95-84-EC-9D-B4-EC-9C-A0  02  06  A1-01  1C  00
        }
    }
}