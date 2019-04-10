namespace GL.Servers.SP.Packets.Messages.Client.Account
{
    using GL.Servers.Extensions.Binary;
    using GL.Servers.SP.Logic;
    using GL.Servers.SP.Packets.Enums;

    internal class Set_Device_Token_Message : Message
    {
        private string Password;

        internal override ServiceNode Node
        {
            get
            {
                return ServiceNode.Base;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Set_Device_Token_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Set_Device_Token_Message(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Set_Device_Token_Message.
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