namespace GL.Clients.BS.Packets.Messages.Client
{
    using GL.Clients.BS.Logic;

    using GL.Servers.Extensions.List;
    using GL.Servers.Logic.Enums;

    internal class Authentification : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Authentification"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Authentification(Device Device) : base(Device)
        {
            this.Identifier     = 10101;
            this.Version        = 8;
            this.Device.State   = State.LOGIN;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddInt(this.Device.Player.HighID);
            this.Data.AddInt(this.Device.Player.LowID);

            this.Data.AddString(this.Device.Player.Token);

            this.Data.AddInt(this.Device.Player.Major);
            this.Data.AddInt(this.Device.Player.Revision);
            this.Data.AddInt(this.Device.Player.Minor);

            this.Data.AddString(this.Device.Player.MasterHash);

            this.Data.AddInt(0);

            this.Data.AddString(this.Device.Player.OpenUDID);
            this.Data.AddString(this.Device.Player.MACAddress);
            this.Data.AddString(this.Device.Player.Model);

            this.Data.AddBool(this.Device.Player.Android);

            this.Data.AddString(this.Device.Player.Region);
            this.Data.AddString(this.Device.Player.AdvertiseID);
            this.Data.AddString(this.Device.Player.OSVersion);

            this.Data.AddHexa("00-00-00-00 00-00-00-00 00-00-00-00 00-00 00-00-00-24  32-34-36-45-42-34-36-33-2D-45-37-31-43-2D-34-33-33-39-2D-42-32-34-35-2D-32-39-46-45-45-35-46-31-35-41-44-45 FF-FF-FF-FF 00-00-00-00 00-00-00-00 00-00-00-00 00-00-00-00 00-00-00-00 00-00");
        }
    }
}