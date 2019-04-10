namespace GL.Servers.BS.Packets.Messages.Server
{
    using System;

    using GL.Servers.BS.Core;
    using GL.Servers.BS.Logic;

    using GL.Servers.Extensions.List;
    using GL.Servers.Files;
    using GL.Servers.Logic.Enums;

    internal class Authentification_Failed : Message
    {
        internal string ServerHost = "stage.brawlstarsgame.com";
        internal string UpdateHost = "https://www.gobelinland.fr/resources/gobelinland-cr.3/";

        internal string Message;

        internal Reason Reason;

        /// <summary>
        /// Gets the time left before maintenance ends in seconds.
        /// </summary>
        internal int TimeLeft
        {
            get
            {
                return Constants.MaintenanceTime > DateTime.UtcNow ? (int) Constants.MaintenanceTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds : 0;
            }
        }

        /// <summary>
        /// Gets the patching host.
        /// </summary>
        internal string PatchingHost
        {
            get
            {
                return Fingerprint.Custom ? "https://raw.githubusercontent.com/BerkanYildiz/GL.Patchs/master/BrawlStars/" : "http://a678dbc1c015a893c9fd-4e8cc3b1ad3a3c940c504815caefa967.r87.cf2.rackcdn.com/";
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Authentification_Failed"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reason">The reason.</param>
        /// <param name="Message">The message.</param>
        public Authentification_Failed(Device Device, Reason Reason = Reason.Default, string Message = null) : base(Device)
        {
            this.Identifier = 20103;
            this.Version    = 9;
            this.Reason     = Reason;
            this.Message    = Message;

            Logging.Error(this.GetType(), Reason.ToString() + ".");
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddInt((int) this.Reason);

            this.Data.AddString(this.Reason == Reason.Patch ? Fingerprint.Json : null);
            this.Data.AddString(this.ServerHost);
            this.Data.AddString(this.PatchingHost);
            this.Data.AddString(this.UpdateHost);
            this.Data.AddString(this.Message);

            this.Data.Add(0);
            this.Data.AddInt(this.TimeLeft);
            this.Data.AddString(null);
            this.Data.AddString(null);
            this.Data.AddInt(-43); // FF-FF-FF-D5 ???????????????????????????????????????????????????????? WTF ????????????????????????????????????????????????????????
            this.Data.AddInt(0);
            this.Data.AddString(null);
            this.Data.AddString(null);
            this.Data.Add(1);
        }
    }
}