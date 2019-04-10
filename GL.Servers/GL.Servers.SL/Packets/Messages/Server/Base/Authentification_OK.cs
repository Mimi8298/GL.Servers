namespace GL.Servers.SL.Packets.Messages.Server
{
    using GL.Servers.SL.Logic;
    using GL.Servers.SL.Logic.Enums;
    using GL.Servers.SL.Logic.Slots.Items;

    using GL.Servers.Logic.Enums;
    using GL.Servers.Extensions.List;

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
            this.Data.AddInt((int) CVersion.Build);
            this.Data.AddInt(0);

            this.Data.AddString("content-stage");

            this.Data.AddInt(1); // Total Session
            this.Data.AddInt(0); // Played Time
            this.Data.AddInt(0); // Played Time in day

            this.Data.AddString(Facebook.ApplicationID); // 103121310241222
            this.Data.AddString(string.Empty);
            this.Data.AddString(string.Empty);

            this.Data.AddInt(00); // Remaining time before all functionality
        }
    }
}
