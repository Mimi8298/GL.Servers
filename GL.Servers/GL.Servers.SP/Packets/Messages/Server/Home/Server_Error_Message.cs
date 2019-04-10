namespace GL.Servers.SP.Packets.Messages.Server.Home
{
    using GL.Servers.SP.Logic;
    using GL.Servers.SP.Packets.Enums;

    internal class Server_Error_Message : Message
    {
        internal string Reason;

        /// <summary>
        /// Gets a value indicating the message type.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 24115;
            }
        }

        /// <summary>
        /// Gets a value indicating the service node of message.
        /// </summary>
        internal override ServiceNode Node
        {
            get
            {
                return ServiceNode.Home;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Server_Error_Message"/> class.
        /// </summary>
        public Server_Error_Message(Device Device, string Reason) : base(Device)
        {
            this.Reason = Reason;
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddString(this.Reason);
        }
    }
}