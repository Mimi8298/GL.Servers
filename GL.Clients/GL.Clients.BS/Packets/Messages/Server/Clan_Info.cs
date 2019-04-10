namespace GL.Clients.BS.Packets.Messages.Server
{
    using GL.Clients.BS.Logic;

    using GL.Servers.Extensions.Binary;

    internal class Clan_Info : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Clan_Info"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Clan_Info(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Clan_Info.
        }
    }
}