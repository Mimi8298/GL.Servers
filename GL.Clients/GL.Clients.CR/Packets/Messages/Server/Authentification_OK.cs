namespace GL.Clients.CR.Packets.Messages.Server
{
    using GL.Clients.CR.Logic;
    using GL.Servers.Extensions.Binary;

    internal class Authentification_OK : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Authentification_OK"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Authentification_OK(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Authentification_OK.
        }
    }
}