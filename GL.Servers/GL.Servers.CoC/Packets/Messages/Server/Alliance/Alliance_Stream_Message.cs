namespace GL.Servers.CoC.Packets.Messages.Server.Alliance
{
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Logic.Clan.StreamEntry;
    using GL.Servers.CoC.Packets.Enums;

    internal class Alliance_Stream_Message : Message
    {
        private StreamEntry[] StreamEntries;

        /// <summary>
        /// Gets a value indicating the message type.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 24311;
            }
        }

        /// <summary>
        /// Gets a value indicating the service node of message.
        /// </summary>
        internal override ServiceNode Node
        {
            get
            {
                return ServiceNode.Alliance;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Alliance_Stream_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Alliance_Stream_Message(Device Device, StreamEntry[] StreamEntries) : base(Device)
        {
            this.StreamEntries = StreamEntries;
        }
        
        internal override void Encode()
        {
            this.Data.AddInt(0);
            this.Data.AddInt(this.StreamEntries.Length);

            for (int i = 0; i < this.StreamEntries.Length; i++)
            {
                this.Data.AddInt((int) this.StreamEntries[i].StreamType);
                this.StreamEntries[i].Encode(this.Data);
            }
        }
    }
}