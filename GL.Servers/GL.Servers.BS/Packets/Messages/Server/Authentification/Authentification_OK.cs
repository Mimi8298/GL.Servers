namespace GL.Servers.BS.Packets.Messages.Server
{
    using GL.Servers.BS.Logic;
    using GL.Servers.BS.Logic.Enums;
    using GL.Servers.BS.Logic.Slots.Items;
    using GL.Servers.Extensions.List;
    using GL.Servers.Logic.Enums;

    internal class Authentification_OK : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Authentification_OK"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Authentification_OK(Device Device) : base (Device)
        {
            this.Device.State   = State.LOGGED;

            this.Identifier     = 20104;
            this.Version        = 1;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddInt(this.Device.Player.HighID);
            this.Data.AddInt(this.Device.Player.LowID);

            this.Data.AddInt(this.Device.Player.HighID);
            this.Data.AddInt(this.Device.Player.LowID);

            this.Data.AddString(this.Device.Player.Token);

            this.Data.AddString(this.Device.Player.Facebook.Identifier);
            this.Data.AddString(this.Device.Player.Gamecenter.Identifier);

            this.Data.AddInt((int) CVersion.Major);
            this.Data.AddInt((int) CVersion.Minor);
            this.Data.AddInt((int) CVersion.Revision);

            this.Data.AddString("prod");

            this.Data.AddInt(1); // Total Session
            this.Data.AddInt(0); // Played Time
            this.Data.AddInt(0); // Played Time in day

            this.Data.AddString(Facebook.ApplicationID); // 103121310241222
            this.Data.AddString(string.Empty);
            this.Data.AddString(string.Empty);
            // this.Data.AddString(DateTime.UtcNow.ToString("#"));
            // this.Data.AddString(this.Device.Player.Created.Subtract(new DateTime(1970, 1, 1)).TotalSeconds.ToString("#"));

            this.Data.AddInt(0); // Remaining time before all functionality

            this.Data.AddString(this.Device.Player.Google.Identifier);
            this.Data.AddString(this.Device.Player.Region);
            this.Data.AddString(null);

            this.Data.AddInt(1);

            this.Data.AddString(null);
            this.Data.AddString(null);
            this.Data.AddString(null);
        }
    }
}
