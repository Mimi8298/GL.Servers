namespace GL.Servers.SP.Packets.Messages.Server.Home
{
    using GL.Servers.Logic.Enums;
    using GL.Servers.SP.Logic;
    using GL.Servers.SP.Packets.Enums;

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
        public Out_Of_Sync_Message(Device Device) : base(Device)
        {
            // Out_Of_Sync_Message.
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddInt(0);
            this.Data.AddInt(0);
            this.Data.AddInt(0);
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