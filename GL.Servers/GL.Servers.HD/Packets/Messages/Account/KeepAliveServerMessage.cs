namespace GL.Servers.HD.Packets.Messages.Account
{
    using GL.Servers.HD.Logic;

    internal class KeepAliveServerMessage : Message
    {
        /// <summary>
        /// Gets the type of this message.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 20108;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeepAliveServerMessage"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public KeepAliveServerMessage(Device Device) : base(Device)
        {
            // KeepAliveServerMessage.
        }
    }
}