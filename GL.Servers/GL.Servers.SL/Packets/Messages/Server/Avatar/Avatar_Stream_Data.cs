namespace GL.Servers.SL.Packets.Messages.Server
{
    using GL.Servers.SL.Logic;

    using GL.Servers.Extensions.List;

    internal class Avatar_Stream_Data : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Avatar_Stream_Data"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Avatar_Stream_Data(Device Device) : base (Device)
        {
            this.Identifier = 24411;
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
