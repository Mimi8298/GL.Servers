namespace GL.Servers.CR.Packets.Messages.Server.Account
{
    using GL.Servers.CR.Logic;
    using GL.Servers.CR.Logic.Enums;

    internal class Server_Shutdown_Message : Message
    {
        /// <summary>
        /// Gets the type of this message.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 20161;
            }
        }

        /// <summary>
        /// Gets the service node of this message.
        /// </summary>
        internal override Node ServiceNode
        {
            get
            {
                return Node.Account;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Server_Shutdown_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Server_Shutdown_Message(Device Device) : base(Device)
        {
            // Server_Shutdown_Message.
        }
    }
}