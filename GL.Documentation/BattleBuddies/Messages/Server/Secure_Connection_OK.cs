namespace GL.Servers.BU.Packets.Messages.Server
{
    using GL.Servers.BU.Extensions.List;
    using GL.Servers.BU.Logic;
    using GL.Servers.BU.Logic.Enums;

    internal class Secure_Connection_OK : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Secure_Connection_OK"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Secure_Connection_OK(Device Device) : base(Device)
        {
            this.Identifier     = 20112;
            this.Device.State   = State.SESSION_OK;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddInt(0); // Header

            this.Data.AddInt(0); // Session Key
        }
    }
}