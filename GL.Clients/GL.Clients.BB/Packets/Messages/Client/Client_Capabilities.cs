namespace GL.Clients.BB.Packets.Messages.Client
{
    using GL.Clients.BB.Logic;
    using GL.Servers.Extensions.List;

    internal class Client_Capabilities : Message
    {
        private int Ping;

        /// <summary>
        /// Initializes a new instance of the <see cref="Client_Capabilities"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Client_Capabilities(Device Device) : base(Device)
        {
            this.Identifier = 10107;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddVInt(this.Ping);
        }
    }
}