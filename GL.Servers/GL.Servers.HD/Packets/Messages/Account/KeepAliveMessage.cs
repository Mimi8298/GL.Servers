namespace GL.Servers.HD.Packets.Messages.Account
{
    using GL.Servers.Extensions.Binary;
    using GL.Servers.HD.Core.Network;
    using GL.Servers.HD.Logic;

    internal class KeepAliveMessage : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeepAliveMessage"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public KeepAliveMessage(Device Device, Reader Reader) : base(Device, Reader)
        {
            // KeepAliveMessage.
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Process()
        {
            new KeepAliveServerMessage(this.Device).Send();
        }
    }
}