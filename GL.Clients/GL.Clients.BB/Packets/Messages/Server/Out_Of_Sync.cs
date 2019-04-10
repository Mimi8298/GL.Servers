namespace GL.Clients.BB.Packets.Messages.Server
{
    using GL.Clients.BB.Logic;
    using GL.Servers.Extensions.Binary;
    using GL.Servers.Logic.Enums;

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

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            this.Device.State = State.DISCONNECTED;
        }
    }
}