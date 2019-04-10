namespace GL.Servers.BS.Packets.Messages.Server
{
    using GL.Servers.BS.Logic;

    using GL.Servers.Extensions.List;

    internal class Own_Home_Data : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Own_Home_Data"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Own_Home_Data(Device Device) : base (Device)
        {
            this.Identifier = 24101;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddRange(this.Device.Player.Objects.ToBytes);
            this.Data.AddRange(this.Device.Player.ToBytes);

            this.Data.AddVInt(0); // Timestamp
        }
    }
}
