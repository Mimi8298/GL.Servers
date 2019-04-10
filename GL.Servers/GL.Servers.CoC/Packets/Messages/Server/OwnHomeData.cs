namespace GL.Servers.BS.Packets.Messages.Server
{
    using GL.Servers.BS.Logic;
    using GL.Servers.Extensions.List;

    internal class OwnHomeData : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OwnHomeData"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public OwnHomeData(Device Device) : base (Device)
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
        }
    }
}
