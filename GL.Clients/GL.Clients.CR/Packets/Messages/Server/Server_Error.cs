namespace GL.Clients.CR.Packets.Messages.Server
{
    using GL.Clients.CR.Logic;
    using GL.Servers.Extensions.Binary;

    internal class Server_Error : Message
    {
        internal string Message;

        /// <summary>
        /// Initializes a new instance of the <see cref="Server_Error"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Server_Error(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Server_Error.
        }

        /// <summary>
        /// Encodes this message.
        /// </summary>
        internal override void Decode()
        {
            this.Message = this.Reader.ReadString();
        }
    }
}