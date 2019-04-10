namespace GL.Servers.BS.Packets.Messages.Client
{
    using GL.Servers.BS.Logic;
    using GL.Servers.Extensions.Binary;

    internal class Chat_Gameroom : Message
    {
        private string Message;

        /// <summary>
        /// Initializes a new instance of the <see cref="Chat_Gameroom"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Chat_Gameroom(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Chat_Gameroom.
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.Message = this.Reader.ReadString();
        }
    }
}