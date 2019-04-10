namespace GL.Servers.SP.Packets.Messages.Server.Account
{
    using GL.Servers.SP.Logic;

    internal class Keep_Alive_Server_Message : Message
    {
        /// <summary>
        /// Gets a value indicating the message type.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 20112;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Keep_Alive_Server_Message"/> class.
        /// </summary>
        internal Keep_Alive_Server_Message(Device Device) : base(Device)
        {
            // Keep_Alive_Server_Message.
        }
    }
}