namespace GL.Servers.CoC.Packets.Messages.Client.Avatar
{

    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Core.Network;
    using GL.Servers.CoC.Packets.Enums;
    using GL.Servers.CoC.Packets.Messages.Server.Avatar;

    using GL.Servers.Extensions.Binary;

    internal class Ask_For_Avatar_Stream_Message : Message
    {
        /// <summary>
        /// Gets a value indicating the message type.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 14405;
            }
        }

        /// <summary>
        /// Gets a value indicating the service node.
        /// </summary>
        internal override ServiceNode Node
        {
            get
            {
                return ServiceNode.Avatar;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ask_For_Avatar_Stream_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Ask_For_Avatar_Stream_Message(Device Device, Reader Reader) : base(Device, Reader)
        {
            // AskForAvatarStreamMessage.
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Process()
        {
            new Avatar_Stream_Message(this.Device, this.Device.GameMode.Level.Player.Logs).Send();
        }
    }
}