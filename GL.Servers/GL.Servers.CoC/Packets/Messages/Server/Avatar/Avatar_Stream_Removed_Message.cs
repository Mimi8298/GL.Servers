namespace GL.Servers.CoC.Packets.Messages.Server.Avatar
{
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Logic.Log;
    using GL.Servers.CoC.Packets.Enums;
    using GL.Servers.Extensions.List;

    internal class Avatar_Stream_Removed_Message : Message
    {
        private AvatarStreamEntry StreamEntry;

        /// <summary>
        /// Gets a value indicating the message type.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 24418;
            }
        }

        /// <summary>
        /// Gets a value indicating the service node of message.
        /// </summary>
        internal override ServiceNode Node
        {
            get
            {
                return ServiceNode.Avatar;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Avatar_Stream_Removed_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Avatar_Stream_Removed_Message(Device Device, AvatarStreamEntry StreamEntry) : base (Device)
        {
            this.StreamEntry = StreamEntry;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddLong(this.StreamEntry.HighID, this.StreamEntry.LowID);
        }
    }
}
