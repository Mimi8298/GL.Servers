namespace GL.Servers.SL.Packets.Messages.Client
{
    using GL.Servers.SL.Logic;
    using GL.Servers.Extensions.Binary;
    using GL.Servers.SL.Core.Network;
    using GL.Servers.SL.Packets.Messages.Server;

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
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Process()
        {
            new Keep_Alive_Server(this.Device).Send();
        }
    }
}