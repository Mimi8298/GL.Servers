namespace GL.Servers.CoC.Packets.Messages.Server.Alliance
{
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Packets.Enums;
    using GL.Servers.CoC.Logic.Clan.StreamEntry;

    internal class Alliance_Stream_Entry_Removed_Message : Message
    {
        private StreamEntry StreamEntry;

        /// <summary>
        /// Gets a value indicating the message type.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 24318;
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
        /// Initializes a new instance of the <see cref="Alliance_Stream_Entry_Removed_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Alliance_Stream_Entry_Removed_Message(Device Device, StreamEntry StreamEntry) : base(Device)
        {
            this.StreamEntry = StreamEntry;
        }
        
        internal override void Encode()
        {
            this.Data.AddLong(this.StreamEntry.HighId, this.StreamEntry.LowId);
        }
    }
}