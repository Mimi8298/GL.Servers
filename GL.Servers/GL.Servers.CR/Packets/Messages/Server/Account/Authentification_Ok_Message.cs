namespace GL.Servers.CR.Packets.Messages.Server.Account
{
    using System;
    using GL.Servers.CR.Logic;
    using GL.Servers.CR.Logic.Apis;
    using GL.Servers.CR.Logic.Enums;

    using GL.Servers.Extensions.List;

    internal class Authentification_Ok_Message : Message
    {
        internal string PassToken;

        /// <summary>
        /// Gets the type of this message.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 20104;
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
        /// Initializes a new instance of the <see cref="Authentification_Ok_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Authentification_Ok_Message(Device Device, string PassToken) : base(Device)
        {
            this.Version    = 1;
            this.PassToken  = PassToken;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddLong(this.Device.MessageManager.AccountHighId, this.Device.MessageManager.AccountLowId);
            this.Data.AddLong(this.Device.MessageManager.AccountHighId, this.Device.MessageManager.AccountLowId);

            this.Data.AddString(this.PassToken);

            this.Data.AddString(null);
            this.Data.AddString(null);

            this.Data.AddVInt(Logic.Version.ServerMajorVersion);
            this.Data.AddVInt(0); // ServerMinorVersion
            this.Data.AddVInt(Logic.Version.ServerBuildVersion);

            this.Data.AddVInt(1); // Content Version

            this.Data.AddString(Logic.Version.Environment);

            this.Data.AddVInt(0); // Session Count
            this.Data.AddVInt(0); // PlayTime Seconds.
            this.Data.AddVInt(0); // Days Since Started Playing.

            this.Data.AddString(Facebook.ApplicationID);
            
            this.Data.AddString("1507593815116");
            this.Data.AddString("1507593409000");

            this.Data.AddVInt(0);

            this.Data.AddString(null);
            this.Data.AddString(null);
            this.Data.AddString(null);

            this.Data.AddString("CA");
            this.Data.AddString("Port Coquitlam");
            this.Data.AddString("BC");

            this.Data.AddVInt(0);
            
            this.Data.AddString("https://game-assets.clashroyaleapp.com");//assents
            this.Data.AddString("https://99faf1e355c749a9a049-2a63f4436c967aa7d355061bd0d924a1.ssl.cf1.rackcdn.com");//assents?
            this.Data.AddString("https://event-assets.clashroyale.com");//events
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            this.Device.State = Servers.Logic.Enums.State.LOGGED;
        }
    }
}