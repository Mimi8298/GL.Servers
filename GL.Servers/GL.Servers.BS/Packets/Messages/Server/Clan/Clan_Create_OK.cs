namespace GL.Servers.BS.Packets.Messages.Server
{
    using GL.Servers.BS.Logic;

    using GL.Servers.Extensions.List;

    internal class Clan_Create_OK : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Clan_Create_OK"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Clan_Create_OK(Device Device) : base(Device)
        {
            this.Identifier = 24333;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddVInt(20);
        }
    }
}