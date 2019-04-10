namespace GL.Servers.BS.Packets.Messages.Server
{
    using GL.Servers.BS.Logic;
    using GL.Servers.BS.Logic.Entries;

    using GL.Servers.Extensions.List;

    internal class Clan_Stream_Entry : Message
    {
        private readonly IEntry Entry;

        /// <summary>
        /// Initializes a new instance of the <see cref="Clan_Stream_Entry"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Clan">The clan.</param>
        public Clan_Stream_Entry(Device Device, IEntry Entry) : base(Device)
        {
            this.Identifier = 24312;
            this.Entry      = Entry;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddRange(this.Entry.ToBytes);
        }
    }
}