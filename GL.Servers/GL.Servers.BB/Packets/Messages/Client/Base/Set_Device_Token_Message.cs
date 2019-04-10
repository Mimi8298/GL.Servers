namespace GL.Servers.BB.Packets.Messages.Client.Base
{
    using GL.Servers.BB.Logic;
    using GL.Servers.Extensions.Binary;

    internal class Set_Device_Token_Message : Message
    {
        private string Password;

        /// <summary>
        /// Initializes a new instance of the <see cref="Set_Device_Token_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Set_Device_Token_Message(Device Device, Reader Reader) : base(Device, Reader)
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