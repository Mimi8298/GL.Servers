namespace GL.Servers.SL.Packets.Messages.Server
{
    using GL.Servers.SL.Logic;

    internal class Keep_Alive_Server : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Keep_Alive_Server"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Keep_Alive_Server(Device Device) : base (Device)
        {
            this.Identifier     = 20108;
        }
    }
}
