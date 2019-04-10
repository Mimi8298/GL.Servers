namespace GL.Servers.CR.Packets.Messages.Server.Avatar
{
    using GL.Servers.CR.Logic;
    using GL.Servers.CR.Logic.Enums;
    using GL.Servers.Extensions.List;

    internal class Avatar_Name_Change_Failed_Message : Message
    {
        /// <summary>
        /// Gets the type of this message.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 20205;
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
        /// Initializes a new instance of the <see cref="Avatar_Name_Change_Failed_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Avatar_Name_Change_Failed_Message(Device Device) : base(Device)
        {
            // Avatar_Name_Change_Failed_Message.
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddInt(0);
        }
    }
}