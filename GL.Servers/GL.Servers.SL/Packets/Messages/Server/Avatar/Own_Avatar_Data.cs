namespace GL.Servers.SL.Packets.Messages.Server
{
    using GL.Servers.SL.Logic;

    using GL.Servers.Extensions.List;
    using GL.Servers.SL.Core;

    internal class Own_Avatar_Data : Message
    {
        private int TimeSinceLastSave;

        /// <summary>
        /// Initializes a new instance of the <see cref="Own_Avatar_Data"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Own_Avatar_Data(Device Device) : base (Device)
        {
            this.Identifier        = 24101;
            this.TimeSinceLastSave = (int) System.DateTime.UtcNow.Subtract(this.Device.Player.Update).TotalSeconds;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddInt(this.TimeSinceLastSave);
            this.Data.AddInt(0);
            this.Data.AddInt(0);
            this.Data.AddInt(0);

            this.Data.AddRange(this.Device.Player.ToBytes);
        }

        internal override void Process()
        {
            this.Device.Player.AdjustSubTick();
            this.Device.Player.FastForward(this.TimeSinceLastSave);
            this.Device.Player.Tick();
        }
    }
}
