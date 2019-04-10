namespace GL.Servers.BU.Packets.Messages.Server
{
    using GL.Servers.BU.Extensions.List;
    using GL.Servers.BU.Logic;

    internal class Authentification_OK : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Authentification_OK"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Authentification_OK(Device Device) : base(Device)
        {
            this.Identifier = 20104;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddInt(0x5006);

            this.Data.AddInt(0);
            this.Data.AddInt(1);

            // this.Data.AddString("GobelinLand");

            this.Data.AddRange(new byte[256]);
        }
    }
}