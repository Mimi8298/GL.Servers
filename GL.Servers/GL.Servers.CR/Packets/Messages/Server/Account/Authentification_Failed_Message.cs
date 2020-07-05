namespace GL.Servers.CR.Packets.Messages.Server.Account
{
    using System;

    using GL.Servers.CR.Core;
    using GL.Servers.CR.Logic;
    using GL.Servers.CR.Logic.Enums;

    using GL.Servers.Extensions.List;
    using GL.Servers.Files;
    using GL.Servers.Logic.Enums;

    internal class Authentification_Failed_Message : Message
    {
        internal string UpdateURL = "http://tinyical.com/1GLe";
        internal string RedirectDomain = "0.0.0.0";
        internal string Message;

        internal Reason Reason;

        internal int TimeLeft
        {
            get
            {
                return Constants.Maintenance > DateTime.UtcNow ? (int) Constants.Maintenance.Subtract(new DateTime(1970, 1, 1)).TotalSeconds : 0;
            }
        }

        /// <summary>
        /// Gets the patching host.
        /// </summary>
        internal string ContentURL
        {
            get
            {
                return Fingerprint.Custom ? "https://raw.githubusercontent.com/BerkanYildiz/GL.Patchs/master/ClashRoyale" : "http://7166046b142482e67b30-2a63f4436c967aa7d355061bd0d924a1.r65.cf1.rackcdn.com";
            }
        }

        /// <summary>
        /// Gets the type of this message.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 20103;
            }
        }

        /// <summary>
        /// Gets the service node of this message.
        /// </summary>
        internal override Node ServiceNode
        {
            get
            {
                return Node.Account;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Authentification_Failed_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reason">The reason.</param>
        /// <param name="Message">The message.</param>
        public Authentification_Failed_Message(Device Device, Reason Reason = Reason.Default) : base(Device)
        {
            this.Version        = 4;
            this.Reason         = Reason;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddVInt((int) this.Reason);

            this.Data.AddString(this.Reason == Reason.Patch ? Fingerprint.Json : null);
            this.Data.AddString(this.UpdateURL);
            this.Data.AddString(this.Message);    // V1
            this.Data.AddString(this.ContentURL); // V2
            this.Data.AddVInt(this.TimeLeft);     // V2
            this.Data.AddBoolean(false);          // V3

            this.Data.AddString(null);            // V4

            // Url

            this.Data.AddVInt(2); // Count
            {
                this.Data.AddString("https://game-assets.clashroyaleapp.com");
                this.Data.AddString(this.ContentURL);
            }
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            this.Device.State = State.DISCONNECTED;
        }
    }
}