namespace GL.Servers.BS.Packets.Messages.Server
{
    using GL.Servers.BS.Core;
    using GL.Servers.BS.Logic;

    using GL.Servers.Extensions.List;

    internal class Clan_Stream : Message
    {
        private Clan Clan;

        /// <summary>
        /// Initializes a new instance of the <see cref="Clan_Stream"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Clan_Stream(Device Device, Clan Clan) : base(Device)
        {
            this.Identifier = 24311;
            this.Clan       = Clan;

            if (this.Clan == null)
            {
                this.Clan   = Resources.Clans.Get(this.Device.Player.ClanHighID, this.Device.Player.ClanLowID);
            }
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            if (this.Clan != null)
            {
                this.Data.AddRange(this.Clan.Messages.ToBytes);
            }
            else
            {
                this.Data.AddVInt(0);
            }
        }
    }
}
 