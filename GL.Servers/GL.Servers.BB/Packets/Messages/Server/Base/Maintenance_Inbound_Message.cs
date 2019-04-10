namespace GL.Servers.BB.Packets.Messages.Server.Base
{
    using GL.Servers.BB.Logic;

    internal class Maintenance_Inbound_Message : Message
    {
        internal override short Type
        {
            get
            {
                return 20161;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Maintenance_Inbound_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Maintenance_Inbound_Message(Device Device) : base(Device)
        {

        }
    }
}