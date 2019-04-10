namespace GL.Clients.BS.Packets.Messages.Client
{
    using GL.Clients.BS.Logic;

    using GL.Servers.Extensions.List;

    internal class Ask_Profile : Message
    {
        private int HighID;
        private int LowID;

        /// <summary>
        /// Initializes a new instance of the <see cref="Ask_Profile"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Ask_Profile(Device Device, int HighID, int LowID) : base(Device)
        {
            this.Identifier = 14113;
            this.HighID     = HighID;
            this.LowID      = LowID;
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