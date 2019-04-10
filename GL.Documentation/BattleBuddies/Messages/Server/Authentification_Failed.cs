namespace GL.Servers.BU.Packets.Messages.Server
{
    using GL.Servers.BU.Extensions.List;
    using GL.Servers.BU.Logic;
    using GL.Servers.BU.Logic.Enums;

    internal class Authentification_Failed : Message
    {
        internal Reason Reason;

        /// <summary>
        /// Initializes a new instance of the <see cref="Authentification_Failed"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reason">The reason.</param>
        public Authentification_Failed(Device Device, Reason Reason = Reason.Default) : base(Device)
        {
            this.Identifier = 20103;
            this.Reason     = Reason;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddInt(0); // Header

            this.Data.AddInt(1);
        }
    }
}