namespace GL.Servers.CoC.Packets.Messages.Client.Account
{
    using GL.Servers.CoC.Core.Network;

    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Packets.Enums;
    using GL.Servers.CoC.Packets.Messages.Server.Account;
    using GL.Servers.Extensions.Binary;

    internal class Keep_Alive_Message : Message
    {
        /// <summary>
        /// Gets a value indicating the message type.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 10108;
            }
        }

        /// <summary>
        /// Gets a value indicating the service node of message.
        /// </summary>
        internal override ServiceNode Node
        {
            get
            {
                return ServiceNode.Account;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Keep_Alive_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Keep_Alive_Message(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Keep_Alive_Message.
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Process()
        {
            new Keep_Alive_Server_Message(this.Device).Send();
        }
    }
}