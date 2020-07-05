namespace GL.Servers.CR.Packets.Messages.Server.Account
{
    using GL.Servers.CR.Logic;
    using GL.Servers.CR.Logic.Enums;

    internal class Keep_Alive_Server_Message : Message
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
        /// Initializes a new instance of the <see cref="Keep_Alive_Server_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Keep_Alive_Server_Message(Device Device) : base(Device)
        {
            // Keep_Alive_Server_Message.
        }
    }
}