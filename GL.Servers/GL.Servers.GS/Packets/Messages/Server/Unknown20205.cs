namespace GL.Servers.GS.Packets.Messages.Server
{
    using GL.Servers.Extensions.List;

    using GL.Servers.GS.Logic.Enums;
    using GL.Servers.GS.Logic;

    internal class Unknown20205 : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Unknown20205"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Unknown20205(Device Device) : base(Device)
        {
            this.Identifier = 20205;
            this.Node       = ServiceNode.SERVICE_NODE_TYPE_ACCOUNT_DIRECTORY;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddInt(this.Device.Player.HighID);
            this.Data.AddInt(this.Device.Player.LowID);

            this.Data.AddString(this.Device.Player.Name);
            this.Data.AddString(null);
            this.Data.AddString(null);

            this.Data.AddInt(0);
            this.Data.AddInt(0);
        }
    }
}
