namespace GL.Servers.CoC.Packets.Messages.Client.Account
{
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Packets.Enums;
    using GL.Servers.Extensions.Binary;

    internal class Set_Device_Token_Message : Message
    {
        private string Password;

        /// <summary>
        /// Gets a value indicating the message type.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 10113;
            }
        }

        /// <summary>
        /// Gets a value indicating the service node of message.
        /// </summary>
        internal override ServiceNode Node
        {
            get
            {
                return ServiceNode.Account;
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