namespace GL.Servers.BS.Packets.Messages.Server
{
    using System.Collections.Generic;

    using GL.Servers.BS.Logic;
    using GL.Servers.Extensions.List;

    internal class Search_Clans_Data : Message
    {
        private List<Clan> Clans;
        private string Name;

        /// <summary>
        /// Initializes a new instance of the <see cref="Search_Clans_Data"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Clans">The clans.</param>
        internal Search_Clans_Data(Device Device, string Name, List<Clan> Clans) : base(Device)
        {
            this.Identifier = 24310;
            this.Clans      = Clans;
            this.Name       = Name;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddString(this.Name);
            this.Data.AddVInt(this.Clans.Count);

            foreach (Clan Clan in this.Clans)
            {
                this.Data.AddInt(Clan.HighID);
                this.Data.AddInt(Clan.LowID);

                this.Data.AddString(Clan.Name);

                this.Data.AddVInt(Files.CSV_Helpers.GlobalID.GetType(Clan.Badge));
                this.Data.AddVInt(Files.CSV_Helpers.GlobalID.GetID(Clan.Badge));

                this.Data.AddVInt((int)Clan.Type);
                this.Data.AddVInt(Clan.Members.Entries.Count);

                this.Data.AddVInt(Clan.Trophies);
                this.Data.AddVInt(Clan.Required_Trophies);
                this.Data.AddVInt(0);
            }
        }
    }
}