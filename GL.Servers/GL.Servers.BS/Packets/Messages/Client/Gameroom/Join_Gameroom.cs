namespace GL.Servers.BS.Packets.Messages.Client
{
    using GL.Servers.BS.Logic;

    using GL.Servers.Extensions.Binary;

    internal class Join_Gameroom : Message
    {
        private int Code;

        /// <summary>
        /// Initializes a new instance of the <see cref="Join_Gameroom"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Join_Gameroom(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Join_Gameroom.
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            // this.Code = this.Reader.ReadVInt();

            // this.Reader.ReadVInt();
            // this.Reader.ReadVInt();
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            this.ShowBuffer();
        }
    }
}