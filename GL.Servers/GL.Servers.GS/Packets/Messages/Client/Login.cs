namespace GL.Servers.GS.Packets.Messages.Client
{
    using GL.Servers.Extensions.Binary;
    using GL.Servers.GS.Core;
    using GL.Servers.GS.Core.Network;

    using GL.Servers.GS.Logic.Enums;
    using GL.Servers.GS.Logic;

    using GL.Servers.GS.Extensions.List;

    using GL.Servers.GS.Packets.Messages.Server;

    internal class Login : Message
    {
        internal string Email;
        internal string PasswordHash;

        /// <summary>
        /// Initializes a new instance of the <see cref="Login"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Login(Device Device) : base(Device)
        {
            this.Identifier = 10101;
            this.Node       = ServiceNode.SERVICE_NODE_TYPE_ACCOUNT_DIRECTORY;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Login"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Login(Device Device, Reader Reader) : base(Device, Reader)
        {
            this.Node       = ServiceNode.SERVICE_NODE_TYPE_ACCOUNT_DIRECTORY;
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.Email        = this.Reader.ReadString();
            this.PasswordHash = this.Reader.ReadString();
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddString(this.Email);
            this.Data.AddString(this.PasswordHash);
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            this.Device.Player = new Player(this.Device, 0, 1);

            Resources.Players.Add(this.Device.Player);

            new Login_OK(this.Device).Send();
        }
    }
}
