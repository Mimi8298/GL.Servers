namespace GL.Servers.CoC.Packets.Messages.Server.Account
{
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Packets.Enums;

    using GL.Servers.Logic.Enums;

    internal class Disconnected_Message : Message
    {
        private int Reason;

        /// <summary>
        /// Gets a value indicating the message type.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 25892;
            }
        }

        /// <summary>
        /// Gets a value indicating the service node of message.
        /// </summary>
        internal override ServiceNode Node
        {
            get
            {
                return ServiceNode.Account;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Disconnected_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Disconnected_Message(Device Device, int Reason) : base(Device)
        {
            this.Reason     = Reason;
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