namespace GL.Servers.GS.Packets.Messages.Server
{
    using GL.Servers.Extensions.List;

    using GL.Servers.GS.Logic.Enums;
    using GL.Servers.GS.Logic;

    internal class Create_Player_Failed : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Create_Player_Failed"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Create_Player_Failed(Device Device) : base(Device)
        {
            this.Identifier = 20202;
            this.Node       = ServiceNode.SERVICE_NODE_TYPE_ACCOUNT_DIRECTORY;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddInt(0);
        }
    }
}
