namespace GL.Clients.CR.Packets.Messages.Server
{
    using GL.Clients.CR.Logic;
    using GL.Servers.Extensions.Binary;

    internal class Disconnected : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Disconnected"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Disconnected(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Disconnected.
        }
    }
}