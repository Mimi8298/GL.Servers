namespace GL.Servers.SL.Packets.Messages.Client
{
    using GL.Servers.SL.Logic;
    using GL.Servers.Extensions.Binary;

    internal class Set_Device_Token : Message
    {
        private string Password;

        /// <summary>
        /// Initializes a new instance of the <see cref="Set_Device_Token"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Set_Device_Token(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Set_Device_Token.
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