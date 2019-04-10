namespace GL.Servers.BU.Packets.Messages.Client
{
    using GL.Servers.BU.Core.Network;
    using GL.Servers.BU.Extensions.Binary;
    using GL.Servers.BU.Logic;
    using GL.Servers.BU.Logic.Enums;
    using GL.Servers.BU.Packets.Messages.Server;

    internal class Secure_Connection : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Secure_Connection"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Secure_Connection(Device Device, Reader Reader) : base(Device, Reader)
        {
            this.Device.State = State.SESSION;
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.Reader.ReadInt32();
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            new Secure_Connection_OK(this.Device).Send();
        }
    }
}