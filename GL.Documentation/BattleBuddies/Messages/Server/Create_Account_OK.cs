namespace GL.Servers.BU.Packets.Messages.Server
{
    using GL.Servers.BU.Extensions.List;
    using GL.Servers.BU.Logic;

    internal class Create_Account_OK : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Create_Account_OK"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Create_Account_OK(Device Device) : base(Device)
        {
            this.Identifier     = 20101;
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

            this.Data.AddRange(new byte[128]);
        }
    }
}