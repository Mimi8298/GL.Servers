namespace GL.Servers.CR.Packets.Messages.Client.Account
{
    using GL.Servers.CR.Core.Network;
    using GL.Servers.CR.Logic;
    using GL.Servers.CR.Logic.Enums;
    using GL.Servers.CR.Packets.Messages.Server.Account;
    using GL.Servers.DataStream;

    internal class Keep_Alive_Message : Message
    {
        /// <summary>
        /// Gets the type of this message.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 10108;
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
        /// Initializes a new instance of the <see cref="Keep_Alive_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Keep_Alive_Message(Device Device, ByteStream ByteStream) : base(Device, ByteStream)
        {
            // Keep_Alive_Message.
        }

        /// <summary>
        /// Processes this message.
        /// </summary>
        internal override void Process()
        {
            this.Device.MessageManager.KeepAliveMessageReceived();
            new Keep_Alive_Server_Message(this.Device).Send();
        }
    }
}