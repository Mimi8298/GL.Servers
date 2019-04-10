namespace GL.Servers.BS.Packets.Messages.Server
{
    using GL.Servers.BS.Logic;

    internal class Server_Shutdown : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Server_Shutdown"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Server_Shutdown(Device Device) : base(Device)
        {
            this.Identifier = 20161;
        }
    }
}