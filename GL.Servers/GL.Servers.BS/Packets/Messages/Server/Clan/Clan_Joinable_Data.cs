namespace GL.Servers.BS.Packets.Messages.Server
{
    using System.Collections.Generic;

    using GL.Servers.BS.Logic;
    using GL.Servers.Extensions.List;

    internal class Joinable_Clans_Data : Message
    {
        private List<Clan> Clans;

        /// <summary>
        /// Initializes a new instance of the <see cref="Joinable_Clans_Data"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Clans">The clans.</param>
        internal Joinable_Clans_Data(Device Device, List<Clan> Clans) : base(Device)
        {
            this.Identifier = 24304;
            this.Clans      = Clans;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddVInt(this.Clans.Count);

            foreach (Clan Clan in this.Clans)
            {
                this.Data.AddInt(Clan.HighID);
                this.Data.AddInt(Clan.LowID);

                this.Data.AddString(Clan.Name);

                this.Data.AddVInt(Files.CSV_Helpers.GlobalID.GetType(Clan.Badge));
                this.Data.AddVInt(Files.CSV_Helpers.GlobalID.GetID(Clan.Badge));
                
                this.Data.AddVInt((int) Clan.Type);
                this.Data.AddVInt(Clan.Members.Entries.Count);

                this.Data.AddVInt(Clan.Trophies);
                this.Data.AddVInt(Clan.Required_Trophies);
                this.Data.AddVInt(0);
            }

            /* 
             *  0A
             *  
             *  00-00-00-01
             *  00-00-0D-83
             *  
             *  00-00-00-0A-56-7A-6C-61-20-53-74-61-66-66
             *  
             *  08
             *  13
             *  01
             *  02
             *  88-03
             *  00-00
             *  
             *  00-00-00-00
             *  00-00-1B-C6
             *  00-00-00-04-53-45-44-53-08-04-01-01-00-00-00-00-00-00-01-00-00-1A-DB-00-00-00-09-54-75-78-2D-50-6F-77-65-72-08-03-01-02-22-00-00-00-00-00-01-00-00-19-D4-00-00-00-0A-4F-53-55-41-4E-61-74-69-6F-6E-08-06-01-03-9A-01-00-00-00-00-00-01-00-00-1B-C1-00-00-00-0A-2D-47-45-54-20-52-33-4B-54-2D-08-02-01-02-14-00-00-00-00-00-00-00-00-19-87-00-00-00-0E-34-5A-6F-64-69-61-63-45-6C-65-6D-65-6E-74-08-0D-01-01-0A-00-00-00-00-00-01-00-00-1B-3E-00-00-00-0D-48-65-61-72-73-74-20-4C-65-67-61-63-79-08-13-01-02-37-00-00-00-00-00-00-00-00-1B-4E-00-00-00-0B-36-20-53-63-68-6C-69-74-7A-65-73-08-04-01-01-0F-00-00-00-00-00-01-00-00-1B-80-00-00-00-0C-54-55-52-54-4C-45-20-53-51-55-41-44-08-0F-01-03-33-00-00-00-00-00-00-00-00-1A-13-00-00-00-0E-6D-61-72-73-65-69-6C-6C-65-20-63-69-74-79-08-04-01-02-14-00-00 
             */
        }
    }
}