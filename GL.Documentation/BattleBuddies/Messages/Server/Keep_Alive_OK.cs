namespace GL.Servers.BU.Packets.Messages.Server
{
    using GL.Servers.BU.Extensions.List;
    using GL.Servers.BU.Logic;

    internal class Keep_Alive_OK : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Keep_Alive_OK"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Keep_Alive_OK(Device Device) : base(Device)
        {
            this.Identifier = 20108;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddInt(0); // Header
        }
    }
}