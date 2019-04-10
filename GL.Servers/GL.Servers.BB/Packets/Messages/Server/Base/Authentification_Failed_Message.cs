namespace GL.Servers.BB.Packets.Messages.Server.Base
{
    using System;

    using GL.Servers.BB.Core;
    using GL.Servers.BB.Logic;

    using GL.Servers.Extensions.List;
    using GL.Servers.Files;
    using GL.Servers.Logic.Enums;

    internal class Authentification_Failed_Message : Message
    {
        internal string RedirectHost = "stage.reef.supercelltest.com";
        internal string UpdateHost = "market://details?id=com.supercell.boombeach";

        internal string Message;

        internal int AccountHighId;
        internal int AccountLowId;

        internal ErrorReason Reason;

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
        internal string ContentHost
        {
            get
            {
                return Fingerprint.Custom ? "https://raw.githubusercontent.com/BerkanYildiz/GL.Patchs/master/BoomBeach/" : "http://df70a89d32075567ba62-1e50fe9ed7ef652688e6e5fff773074c.r40.cf1.rackcdn.com/";
            }
        }

        internal override short Type
        {
            get
            {
                return 20103;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Authentification_Failed_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reason">The reason.</param>
        /// <param name="Message">The message.</param>
        public Authentification_Failed_Message(Device Device, ErrorReason Reason = ErrorReason.Default) : base(Device)
        {
            this.Version    = 2;
            this.Reason     = Reason;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddInt((int) this.Reason);
            this.Data.AddString(this.Reason == ErrorReason.Default ? Fingerprint.Json : null);
            this.Data.AddString(this.RedirectHost);
            this.Data.AddString(this.ContentHost);
            this.Data.AddString(this.UpdateHost);
            this.Data.AddString(this.Message);

            this.Data.AddInt(this.Reason == ErrorReason.Maintenance ? 3600 : 0);

            this.Data.AddBoolean(this.Reason == ErrorReason.Banned);
            this.Data.AddBoolean(this.AccountLowId > 0);

            if (this.AccountLowId > 0)
            {
                this.Data.AddInt(this.AccountHighId);
                this.Data.AddInt(this.AccountLowId);
            }
        }

        internal enum ErrorReason
        {
            Default             = 0,
            Reset               = 1,
            ContentUpdate       = 7,
            Update              = 8,
            Redirection         = 9,
            Maintenance         = 10,
            Banned              = 11,
            DebugModeMismatch   = 12,
            Locked              = 13
        }
    }
}