namespace GL.Servers.CoC.Packets.Messages.Server.Account
{
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Packets.Enums;

    internal class Maintenance_Inbound_Message : Message
    {
        /// <summary>
        /// Gets a value indicating the message type.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 20161;
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
        /// Initializes a new instance of the <see cref="Maintenance_Inbound_Message."/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Maintenance_Inbound_Message(Device Device) : base(Device)
        {
            // Maintenance_Inbound_Message.
        }
    }
}