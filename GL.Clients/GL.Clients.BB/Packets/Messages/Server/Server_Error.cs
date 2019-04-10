namespace GL.Clients.BB.Packets.Messages.Server
{
    using GL.Clients.BB.Logic;
    using GL.Servers.Extensions.Binary;
    using GL.Servers.Logic.Enums;

    internal class Server_Error : Message
    {
        internal int ErrorCode;

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
            this.ErrorCode = this.Reader.ReadInt32();
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            this.Device.State = State.DISCONNECTED;
        }
    }
}