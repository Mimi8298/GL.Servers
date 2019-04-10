namespace GL.Servers.BB.Packets.Messages.Server.Base
{
    using GL.Servers.BB.Logic;

    internal class Disconnected_Message : Message
    {
        internal override short Type
        {
            get
            {
                return 25892;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Disconnected_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Disconnected_Message(Device Device) : base(Device)
        {

        }
    }
}