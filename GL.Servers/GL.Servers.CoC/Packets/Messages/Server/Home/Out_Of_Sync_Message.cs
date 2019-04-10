namespace GL.Servers.CoC.Packets.Messages.Server.Home
{
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Packets.Enums;
    using GL.Servers.Logic.Enums;

    internal class Out_Of_Sync_Message : Message
    {
        /// <summary>
        /// Gets a value indicating the message type.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 24104;
            }
        }

        /// <summary>
        /// Gets a value indicating the service node of message.
        /// </summary>
        internal override ServiceNode Node
        {
            get
            {
                return ServiceNode.Home;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Out_Of_Sync_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Out_Of_Sync_Message(Device Device) : base(Device)
        {
            // Out_Of_Sync_Message.
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