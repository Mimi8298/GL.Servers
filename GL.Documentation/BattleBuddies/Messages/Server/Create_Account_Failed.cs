namespace GL.Servers.BU.Packets.Messages.Server
{
    using GL.Servers.BU.Extensions.List;
    using GL.Servers.BU.Logic;

    internal class Create_Account_Failed : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Create_Account_Failed"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Create_Account_Failed(Device Device) : base(Device)
        {
            this.Identifier     = 20102;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddInt(0);
            this.Data.AddInt(0);
            this.Data.AddInt(0);
            this.Data.AddInt(0);
            this.Data.AddInt(0);
        }
    }
}