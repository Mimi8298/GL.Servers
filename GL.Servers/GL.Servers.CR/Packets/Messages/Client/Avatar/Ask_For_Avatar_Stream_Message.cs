namespace GL.Servers.CR.Packets.Messages.Client.Avatar
{
    using GL.Servers.CR.Logic;
    using GL.Servers.CR.Core.Network;
    using GL.Servers.CR.Logic.Enums;
    using GL.Servers.CR.Packets.Messages.Server.Avatar;
    using GL.Servers.DataStream;

    internal class Ask_For_Avatar_Stream_Message : Message
    {
        /// <summary>
        /// Gets the type of this message.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 14405;
            }
        }

        /// <summary>
        /// Gets the service node of this message.
        /// </summary>
        internal override Node ServiceNode
        {
            get
            {
                return Node.Avatar;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ask_For_Avatar_Stream_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Ask_For_Avatar_Stream_Message(Device Device, ByteStream ByteStream) : base(Device, ByteStream)
        {
            // Ask_For_Avatar_Stream_Message.
        }

        /// <summary>
        /// Processes this message.
        /// </summary>
        internal override void Process()
        {
            new Avatar_Stream_Message(this.Device).Send();
        }
    }
}