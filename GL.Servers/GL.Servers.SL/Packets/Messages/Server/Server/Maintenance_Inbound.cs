namespace GL.Servers.SL.Packets.Messages.Server
{
    using GL.Servers.SL.Logic;

    internal class Maintenance_Inbound : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Maintenance_Inbound"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Maintenance_Inbound(Device Device) : base(Device)
        {
            this.Identifier = 20161;
        }
    }
}