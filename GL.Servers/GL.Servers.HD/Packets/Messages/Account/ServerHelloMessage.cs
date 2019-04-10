namespace GL.Servers.HD.Packets.Messages.Account
{
    using GL.Servers.Extensions.Binary;
    using GL.Servers.Extensions.List;
    using GL.Servers.HD.Logic;

    internal class ServerHelloMessage : Message
    {
        /// <summary>
        /// Gets the type of this message.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 20100;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerHelloMessage"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public ServerHelloMessage(Device Device) : base(Device)
        {
            // ServerHelloMessage.
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddBytes(new byte[24]);
        }
    }
}