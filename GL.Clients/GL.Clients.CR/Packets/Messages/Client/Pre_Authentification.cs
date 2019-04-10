namespace GL.Clients.CR.Packets.Messages.Client
{
    using GL.Clients.CR.Logic;
    using GL.Clients.CR.Logic.Enums;
    using GL.Servers.Extensions.List;

    internal class Pre_Authentification : Message
    {
        internal int Protocol       = 1;
        internal int KeyVersion     = 11;

        internal int Major          = 3;
        internal int Minor          = 377;
        internal int Revision       = 0;

        internal string Masterhash  = "622384571aafa79a8453424fb4907c5f1e4268ce";

        internal int DeviceType     = 2;
        internal int AppStore       = 2;

        /// <summary>
        /// Initializes a new instance of the <see cref="Pre_Authentification"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Pre_Authentification(Device Device) : base(Device)
        {
            this.Identifier     = 10100;
            this.Device.State   = State.SESSION;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddInt(this.Protocol);
            this.Data.AddInt(this.KeyVersion);

            this.Data.AddInt(this.Major);
            this.Data.AddInt(this.Revision);
            this.Data.AddInt(this.Minor);

            this.Data.AddString(this.Masterhash);

            this.Data.AddInt(this.DeviceType);
            this.Data.AddInt(this.AppStore);
        }
    }
}