namespace GL.Clients.BS.Packets.Messages.Server
{
    using GL.Clients.BS.Logic;
    using GL.Clients.BS.Packets.Messages.Client;

    using GL.Servers.Extensions.Binary;

    internal class Battle_Result : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Battle_Result"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Battle_Result(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Battle_Result.
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            this.Device.Gateway.Send(new Go_Home(this.Device));
        }
    }
}