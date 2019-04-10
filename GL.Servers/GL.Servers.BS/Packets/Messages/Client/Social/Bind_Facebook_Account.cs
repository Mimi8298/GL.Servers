namespace GL.Servers.BS.Packets.Messages.Client
{
    using GL.Servers.BS.Core.Network;
    using GL.Servers.BS.Logic;
    using GL.Servers.BS.Packets.Messages.Server;
    using GL.Servers.Extensions.Binary;

    internal class Bind_Facebook_Account : Message
    {
        private int Unknown;

        private string FBIdentifier;
        private string FBToken;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bind_Facebook_Account"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Bind_Facebook_Account(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Bind_Facebook_Account.
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.Unknown        = this.Reader.ReadVInt();
            this.FBIdentifier   = this.Reader.ReadString();
            this.FBToken        = this.Reader.ReadString();
        }

        /// <summary>
        /// Processes this message.
        /// </summary>
        internal override void Process()
        {
            this.Device.Player.Facebook.Identifier  = this.FBIdentifier;
            this.Device.Player.Facebook.Token       = this.FBToken;

            this.Device.Player.Facebook.Connect();

            if (this.Device.Player.Facebook.Connected)
            {
                new Bind_Facebook_Account_OK(this.Device).Send();
            }
        }
    }
}