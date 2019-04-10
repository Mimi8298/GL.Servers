namespace GL.Servers.BS.Packets.Messages.Server
{
    using GL.Servers.BS.Logic;

    using GL.Servers.Extensions.List;

    internal class Clan_Edit_Failed : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Clan_Edit_Failed"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Clan">The clan.</param>
        public Clan_Edit_Failed(Device Device, Clan Clan) : base(Device)
        {
            this.Identifier = 24333;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddVInt(93);
        }
    }
}