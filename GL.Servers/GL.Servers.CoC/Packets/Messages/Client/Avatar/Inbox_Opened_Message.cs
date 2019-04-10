namespace GL.Servers.CoC.Packets.Messages.Client.Avatar
{
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Packets.Enums;
    using GL.Servers.Extensions.Binary;

    internal class Inbox_Opened_Message : Message
    {
        /// <summary>
        /// Gets a value indicating the message type.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 10905;
            }
        }

        /// <summary>
        /// Gets a value indicating the server node of message.
        /// </summary>
        internal override ServiceNode Node
        {
            get
            {
                return ServiceNode.Avatar;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Inbox_Opened_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Inbox_Opened_Message(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Inbox_Opened_Message.
        }
    }
}