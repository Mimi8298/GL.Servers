namespace GL.Servers.CR.Packets.Messages.Server.Home
{
    using GL.Servers.CR.Logic;
    using GL.Servers.CR.Logic.Enums;
    using GL.Servers.Logic.Enums;

    internal class Out_Of_Sync_Message : Message
    {
        internal int ClientChecksum;
        internal int ServerChecksum;

        /// <summary>
        /// Gets the type of this message.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 24104;
            }
        }

        /// <summary>
        /// Gets the service node of this message.
        /// </summary>
        internal override Node ServiceNode
        {
            get
            {
                return Node.Home;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Out_Of_Sync_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Out_Of_Sync_Message(Device Device, int ClientChecksum, int ServerChecksum) : base(Device)
        {
            this.ClientChecksum = ClientChecksum;
            this.ServerChecksum = ServerChecksum;
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddVInt(this.ServerChecksum);
            this.Data.AddVInt(this.ClientChecksum);
            this.Data.AddVInt(0);
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