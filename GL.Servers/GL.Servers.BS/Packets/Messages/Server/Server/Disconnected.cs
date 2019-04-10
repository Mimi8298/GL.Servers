namespace GL.Servers.BS.Packets.Messages.Server
{
    using GL.Servers.BS.Logic;

    internal class Disconnected : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Disconnected"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Disconnected(Device Device) : base(Device)
        {
            this.Identifier = 25892;
        }
    }
}