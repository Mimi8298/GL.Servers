namespace GL.Servers.BU.Packets.Messages.Server
{
    using GL.Servers.BU.Extensions.List;
    using GL.Servers.BU.Logic;

    internal class Avatar_Data : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Avatar_Data"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Avatar_Data(Device Device) : base(Device)
        {
            this.Identifier     = 20201;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddInt(0); // Header

            this.Data.AddInt(0); // High ID
            this.Data.AddInt(1); // Low  ID

            this.Data.AddString("GobelinLand"); // Token ?
        }
    }
}