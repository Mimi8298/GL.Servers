namespace GL.Servers.CR.Packets.Messages.Client.Avatar
{
    using GL.Servers.CR.Logic;
    using GL.Servers.CR.Core.Network;
    using GL.Servers.CR.Packets.Messages.Server.Avatar;

    using GL.Servers.DataStream;

    internal class Avatar_Name_Check_Request_Message : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Avatar_Name_Check_Request"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Avatar_Name_Check_Request_Message(Device Device, ByteStream ByteStream) : base(Device, ByteStream)
        {
            // Avatar_Name_Check_Request_Message.
        }

        /// <summary>
        /// Processes this message.
        /// </summary>
        internal override void Process()
        {
            new Avatar_Name_Check_Response_Message(this.Device).Send();
        }
    }
}