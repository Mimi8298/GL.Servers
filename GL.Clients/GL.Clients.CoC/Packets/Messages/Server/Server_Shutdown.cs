namespace GL.Clients.CoC.Packets.Messages.Server
{
    using GL.Clients.CoC.Logic;
    using GL.Servers.Extensions.Binary;

    internal class Server_Shutdown : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Server_Shutdown"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Server_Shutdown(Device Device, Reader Reader) : base(Device, Reader)
        {
            this.Identifier = 20161;
        }
    }
}