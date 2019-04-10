namespace GL.Servers.BS.Packets.Messages.Client
{
    using GL.Servers.BS.Core.Network;
    using GL.Servers.BS.Logic;
    using GL.Servers.BS.Packets.Messages.Server;
    using GL.Servers.Extensions.Binary;

    internal class Keep_Alive : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Keep_Alive"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
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