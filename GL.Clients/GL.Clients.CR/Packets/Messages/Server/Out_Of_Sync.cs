namespace GL.Clients.CR.Packets.Messages.Server
{
    using GL.Clients.CR.Logic;
    using GL.Servers.Extensions.Binary;

    internal class Out_Of_Sync : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Out_Of_Sync"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Out_Of_Sync(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Out_Of_Sync.
        }
    }
}