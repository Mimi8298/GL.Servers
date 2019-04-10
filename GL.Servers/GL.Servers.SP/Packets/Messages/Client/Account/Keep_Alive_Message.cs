namespace GL.Servers.SP.Packets.Messages.Client.Account
{
    using GL.Servers.Extensions.Binary;
    using GL.Servers.SP.Core.Network;
    using GL.Servers.SP.Logic;
    using GL.Servers.SP.Packets.Enums;
    using GL.Servers.SP.Packets.Messages.Server.Account;

    internal class Keep_Alive_Message : Message
    {
        internal override ServiceNode Node
        {
            get
            {
                return ServiceNode.Base;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Keep_Alive_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Keep_Alive_Message(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Keep_Alive_Message.
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Process()
        {
            new Keep_Alive_Server_Message(this.Device).Send();
        }
    }
}