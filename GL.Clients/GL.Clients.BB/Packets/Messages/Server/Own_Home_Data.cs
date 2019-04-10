namespace GL.Clients.BB.Packets.Messages.Server
{
    using GL.Clients.BB.Logic;
    using GL.Servers.Extensions.Binary;

    internal class Own_Home_Data : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Own_Home_Data"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Own_Home_Data(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Own_Home_Data.
        }
    }
}