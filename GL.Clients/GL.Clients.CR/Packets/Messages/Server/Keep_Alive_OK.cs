namespace GL.Clients.CR.Packets.Messages.Server
{
    using GL.Clients.CR.Logic;
    using GL.Servers.Extensions.Binary;

    internal class Keep_Alive_OK : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Keep_Alive_OK"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Keep_Alive_OK(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Keep_Alive_OK.
        }
    }
}