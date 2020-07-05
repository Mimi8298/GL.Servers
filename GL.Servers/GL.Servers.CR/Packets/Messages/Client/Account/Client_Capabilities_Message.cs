namespace GL.Servers.CR.Packets.Messages.Client.Account
{
    using GL.Servers.CR.Logic;
    using GL.Servers.DataStream;
    using GL.Servers.Extensions.Binary;

    internal class Client_Capabilities_Message : Message
    {
        private int Ping;
        private string Interface;

        /// <summary>
        /// Initializes a new instance of the <see cref="Client_Capabilities_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Client_Capabilities_Message(Device Device, ByteStream ByteStream) : base(Device, ByteStream)
        {
            // Client_Capabilities_Message.
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.Ping               = this.Data.ReadVInt();
            this.Interface          = this.Data.ReadString();
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            this.Device.MessageManager.Ping = this.Ping;
            this.Device.MessageManager.ConnectionInterface = this.Interface;
        }
    }
}