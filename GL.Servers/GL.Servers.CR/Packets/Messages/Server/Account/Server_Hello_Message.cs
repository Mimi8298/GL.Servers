namespace GL.Servers.CR.Packets.Messages.Server.Account
{
    using GL.Servers.CR.Logic;
    using GL.Servers.CR.Logic.Enums;
    using GL.Servers.Extensions.List;
    using GL.Servers.Logic.Enums;

    internal class Server_Hello_Message : Message
    {
        /// <summary>
        /// Gets the type of this message.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 20100;
            }
        }

        /// <summary>
        /// Gets the service node of this message.
        /// </summary>
        internal override Node ServiceNode
        {
            get
            {
                return Node.Account;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Server_Hello_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        internal Server_Hello_Message(Device Device) : base(Device)
        {
            this.Device.State = State.SESSION_OK;
            this.Device.MessageManager.PepperInit.SessionKey = new byte[24];
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddBytes(this.Device.MessageManager.PepperInit.SessionKey);
        }
    }
}