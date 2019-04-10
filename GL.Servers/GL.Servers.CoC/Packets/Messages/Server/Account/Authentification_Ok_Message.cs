namespace GL.Servers.CoC.Packets.Messages.Server.Account
{
    using GL.Servers.CoC.Extensions.Utils;
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Logic.Apis;
    using GL.Servers.CoC.Packets.Enums;

    using GL.Servers.Logic.Enums;
    using GL.Servers.Extensions.List;

    internal class Authentification_Ok_Message : Message
    {
        /// <summary>
        /// Gets a value indicating the message type.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 20104;
            }
        }

        /// <summary>
        /// Gets a value indicating the service node of message.
        /// </summary>
        internal override ServiceNode Node
        {
            get
            {
                return ServiceNode.Account;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Authentification_Ok_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Authentification_Ok_Message(Device Device) : base (Device)
        {
            this.Device.State   = State.LOGGED;
            this.Version = 1;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddLong(this.Device.Account.HighID, this.Device.Account.LowID);
            this.Data.AddLong(this.Device.Account.HighID, this.Device.Account.LowID);

            this.Data.AddString(this.Device.Account.PassToken);

            this.Data.AddString(string.Empty); // Facebook ID
            this.Data.AddString(string.Empty); // Gamecenter ID

            this.Data.AddInt(Logic.Version.ServerMajorVersion);
            this.Data.AddInt(Logic.Version.ServerBuildVersion);
            this.Data.AddInt(0); // Content Version

            this.Data.AddString(Logic.Version.ServerEnvironment);

            this.Data.AddInt(1); // Total Session
            this.Data.AddInt(0); // Play Time Seconds
            this.Data.AddInt(0); // Days Since Started Playing

            this.Data.AddString(Facebook.ApplicationID); // 103121310241222
            this.Data.AddString(TimeUtil.TimestampMS.ToString()); // Server Time
            this.Data.AddString("0"); // Account Creation Date

            this.Data.AddInt(0); // StartupCooldownSeconds

            this.Data.AddString(null); // Google Service ID
            this.Data.AddString("fr");
            this.Data.AddString(null);

            this.Data.AddInt(1);

            this.Data.AddString(null);
            this.Data.AddString(null);
            this.Data.AddString(null);

            this.Data.AddInt(2);
            {
                this.Data.AddString("https://game-assets.clashofclans.com/");
                this.Data.AddString("http://b46f744d64acd2191eda-3720c0374d47e9a0dd52be4d281c260f.r11.cf2.rackcdn.com/");
            }

            this.Data.AddInt(1);
            {
                this.Data.AddString("https://event-assets.clashofclans.com");
            }
        }
    }
}
