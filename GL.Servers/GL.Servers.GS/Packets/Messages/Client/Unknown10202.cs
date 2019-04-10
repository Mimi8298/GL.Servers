namespace GL.Servers.GS.Packets.Messages.Client
{
    using GL.Servers.Extensions.Binary;
    using GL.Servers.Extensions.List;

    using GL.Servers.GS.Logic;
    using GL.Servers.GS.Logic.Enums;

    internal class Unknown10202 : Message
    {
        internal int HighID;
        internal int LowID;

        /// <summary>
        /// Initializes a new instance of the <see cref="Unknown10202"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Unknown10202(Device Device) : base(Device)
        {
            this.Identifier = 10202;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Unknown10202"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Unknown10202(Device Device, Reader Reader) : base(Device, Reader)
        {
            this.Node = ServiceNode.SERVICE_NODE_TYPE_AVATAR_CONTAINER;
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.HighID = this.Reader.ReadInt32();
            this.LowID  = this.Reader.ReadInt32();
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddInt(this.HighID);
            this.Data.AddInt(this.LowID);
        }
    }
}
