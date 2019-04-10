namespace GL.Servers.BB.Packets.Messages.Client.Base
{
    using GL.Servers.BB.Logic;
    using GL.Servers.Extensions.Binary;

    internal class Client_Capabilities_Message : Message
    {
        private int Ping;

        /// <summary>
        /// Initializes a new instance of the <see cref="Client_Capabilities_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Client_Capabilities_Message(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Client_Capabilities.
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.Ping = this.Reader.ReadVInt();
        }
    }
}