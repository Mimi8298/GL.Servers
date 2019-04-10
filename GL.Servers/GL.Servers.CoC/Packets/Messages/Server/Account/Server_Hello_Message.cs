namespace GL.Servers.CoC.Packets.Messages.Server.Account
{
    using GL.Servers.CoC.Core;
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Packets.Cryptography;
    using GL.Servers.CoC.Packets.Enums;

    internal class Server_Hello_Message : Message
    {
        internal byte[] Key;

        /// <summary>
        /// Gets a value indicating the message type.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 20100;
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
        /// Initializes a new instance of the <see cref="Device"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Server_Hello_Message(Device Device) : base(Device)
        {
            this.Key = new byte[24];

            Resources.Random.NextBytes(this.Key);
        }
        
        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddBytes(this.Key);
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            this.Device.PepperState.SessionKey = this.Key;
        }
    }
}