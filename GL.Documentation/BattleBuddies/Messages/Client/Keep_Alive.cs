namespace GL.Servers.BU.Packets.Messages.Client
{
    using GL.Servers.BU.Core.Network;
    using GL.Servers.BU.Extensions.Binary;
    using GL.Servers.BU.Logic;
    using GL.Servers.BU.Packets.Messages.Server;

    internal class Keep_Alive : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Keep_Alive"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Keep_Alive(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Keep_Alive.
        }

        /// <summary>
        /// Processes this message.
        /// </summary>
        internal override void Process()
        {
            new Keep_Alive_OK(this.Device).Send();
        }
    }
}