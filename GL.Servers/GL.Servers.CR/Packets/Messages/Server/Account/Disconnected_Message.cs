namespace GL.Servers.CR.Packets.Messages.Server.Account
{
    using GL.Servers.CR.Logic;
    using GL.Servers.CR.Logic.Enums;
    using GL.Servers.Logic.Enums;

    internal class Disconnected_Message : Message
    {
        /// <summary>
        /// Gets the type of this message.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 25892;
            }
        }

        /// <summary>
        /// Gets the service node of this message.
        /// </summary>
        internal override Node ServiceNode
        {
            get
            {
                return Node.Account;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Disconnected_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Disconnected_Message(Device Device) : base(Device)
        {
            Device.State = State.DISCONNECTED;
        }
    }
}