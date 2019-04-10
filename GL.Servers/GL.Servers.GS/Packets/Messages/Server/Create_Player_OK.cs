namespace GL.Servers.GS.Packets.Messages.Server
{
    using GL.Servers.GS.Logic;
    using GL.Servers.GS.Logic.Enums;

    internal class Create_Player_OK : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Create_Player_OK"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Create_Player_OK(Device Device) : base(Device)
        {
            this.Identifier = 20201;
            this.Node       = ServiceNode.SERVICE_NODE_TYPE_ACCOUNT_DIRECTORY;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddRange(this.Device.Player.ToBytes);
        }
    }
}
