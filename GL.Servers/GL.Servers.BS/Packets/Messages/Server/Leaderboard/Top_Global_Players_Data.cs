namespace GL.Servers.BS.Packets.Messages.Server
{
    using System.Collections.Generic;
    using System.Linq;

    using GL.Servers.BS.Logic;

    using GL.Servers.Extensions.List;

    internal class Top_Global_Players_Data : Message
    {
        private IEnumerable<Player> Players;

        /// <summary>
        /// Initializes a new instance of the <see cref="Top_Global_Players_Data"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Top_Global_Players_Data(Device Device, IEnumerable<Player> Players) : base(Device)
        {
            this.Identifier = 24403;
            this.Players    = Players;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddBool(true);
            this.Data.AddVInt(0);
            this.Data.AddString(null);
            
            this.Data.AddVInt(this.Players.Count());

            foreach (Player Player in this.Players)
            {
                this.Data.AddVInt(Player.HighID);
                this.Data.AddVInt(Player.LowID);

                this.Data.AddVInt(1);
                this.Data.AddVInt(Player.Info.Trophies);

                this.Data.AddVInt(1);

                this.Data.AddString(Player.Name);
                this.Data.AddString(null); // Clan Name

                this.Data.AddVInt(Player.Level);
                this.Data.AddVInt(28); // Thumbnail Type
                this.Data.AddVInt(Player.Info.Thumbnail);
                this.Data.AddVInt(0);
            }

            this.Data.AddHexa("7F-7F-01-00");
            this.Data.AddString(this.Device.Player.Region);
        }
    }
}