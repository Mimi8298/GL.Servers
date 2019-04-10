namespace GL.Servers.GS.Packets.Messages.Server
{
    using GL.Servers.GS.Logic;
    using GL.Servers.GS.Logic.Enums;

    using GL.Servers.Extensions.List;
    using GL.Servers.Logic.Enums;

    internal class Login_OK : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Login_OK"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Login_OK(Device Device) : base(Device)
        {
            this.Identifier     = 20104;
            this.Node           = ServiceNode.SERVICE_NODE_TYPE_ACCOUNT_DIRECTORY;
            this.Device.State   = State.LOGGED;
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.Reader.ReadInt32();
            this.Reader.ReadInt32();

            this.Reader.ReadString();

            this.Reader.ReadInt32();
            this.Reader.ReadInt32();

            this.Reader.ReadInt32();

            this.Reader.ReadString();
            this.Reader.ReadInt32();
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddInt(this.Device.Player.HighID);
            this.Data.AddInt(this.Device.Player.LowID);

            this.Data.AddString(this.Device.Player.Token);

            this.Data.AddInt(this.Device.Player.HighID);
            this.Data.AddInt(this.Device.Player.LowID);

            this.Data.AddInt(0); // Array

            this.Data.AddString(null);
            this.Data.AddInt(0);

            {
                this.Data.AddInt(0);
                this.Data.AddBool(false);
                this.Data.AddBool(false);
                this.Data.AddBool(false);
                this.Data.AddBool(false);
                this.Data.AddBool(false);
                this.Data.AddInt(0);
            }

            this.Data.AddInt(0);
        }
    }
}
