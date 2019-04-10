namespace GL.Servers.BS.Packets.Messages.Server
{
    using System.Collections.Generic;
    using System.Linq;

    using GL.Servers.BS.Files.CSV_Helpers;
    using GL.Servers.BS.Logic;
    using GL.Servers.Extensions.List;

    internal class Top_Local_Clans_Data : Message
    {
        private IEnumerable<Clan> Clans;

        /// <summary>
        /// Initializes a new instance of the <see cref="Top_Local_Clans_Data"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Top_Local_Clans_Data(Device Device, IEnumerable<Clan> Clans) : base(Device)
        {
            this.Identifier = 24403;
            this.Clans      = Clans;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddVInt(2);
            this.Data.AddVInt(0);
            this.Data.AddString(this.Device.Player.Region);

            this.Data.AddVInt(this.Clans.Count());

            foreach (Clan Clan in this.Clans)
            {
                this.Data.AddVInt(Clan.HighID);
                this.Data.AddVInt(Clan.LowID);

                this.Data.AddVInt(1);
                this.Data.AddVInt(Clan.Trophies);
                this.Data.AddVInt(2);

                this.Data.AddString(Clan.Name);
                this.Data.AddVInt(Clan.Members.Entries.Count);

                this.Data.AddVInt(GlobalID.GetType(Clan.Badge));
                this.Data.AddVInt(GlobalID.GetID(Clan.Badge));
            }

            this.Data.AddHexa("05-02-01-00");
            this.Data.AddString(this.Device.Player.Region);

            // 02-00  FF-FF-FF-FF        02  01  01  01  29  02  00-00-00-05  74-65-73-74-69  02  08  0E  00  03  02  05  02  00-00-00-0C  54-72-69-62-65  20-47-61-6D-69-6E-67-03-08-05  05-02-01-00  00-00-00-02-46-52
            // 02-00  00-00-00-02-46-49  02  01  01  01  29  02  00-00-00-05  74-65-73-74-69  02  08  0E  00  03  02  05  02  00-00-00-0C  54-72-69-62-65  20-47-61-6D-69-6E-67-03-08-05  05-02-01-00  00-00-00-02-46-52

        }
    }
}