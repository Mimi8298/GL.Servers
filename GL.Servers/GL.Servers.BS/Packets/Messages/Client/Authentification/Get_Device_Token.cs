namespace GL.Servers.BS.Packets.Messages.Client
{
    using GL.Servers.BS.Logic;
    using GL.Servers.Extensions.Binary;

    internal class Get_Device_Token : Message
    {
        private string Password;

        /// <summary>
        /// Initializes a new instance of the <see cref="Get_Device_Token"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Get_Device_Token(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Get_Device_Token.
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.Password = this.Reader.ReadString();
            this.Reader.ReadInt32();
        }
    }
}