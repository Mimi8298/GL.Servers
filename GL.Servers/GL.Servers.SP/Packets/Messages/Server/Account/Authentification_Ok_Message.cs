namespace GL.Servers.SP.Packets.Messages.Server.Account
{
    using GL.Servers.SP.Logic;

    internal class Authentification_Ok_Message : Message
    {
        internal const string FacebookID = "";
        internal const string GamecenterID = "";
        
        internal const string ServerEnvironment = "apple-stage";

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
        /// Initializes a new instance of the <see cref="Authentification_Ok_Message"/> class.
        /// </summary>
        internal Authentification_Ok_Message(Device Device) : base(Device)
        {
            // Authentification_Ok_Message.
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddLong(this.Device.GameMode.Player.HighID, this.Device.GameMode.Player.LowID);
            this.Data.AddString(this.Device.GameMode.Player.PassToken);

            this.Data.AddString(Authentification_Ok_Message.FacebookID);
            this.Data.AddString(Authentification_Ok_Message.GamecenterID);

            this.Data.AddInt(Logic.Version.ServerMajorVersion);
            this.Data.AddInt(Logic.Version.ServerMinorVersion);
            this.Data.AddInt(Logic.Version.ServerBuildVersion);

            this.Data.AddInt(0); // ContentVersion

            this.Data.AddString(Authentification_Ok_Message.ServerEnvironment);

            this.Data.AddInt(0); // SessionCount
            this.Data.AddInt(0); // PlayTimeSeconds
            this.Data.AddInt(0); // DaysSinceStartedPlaying

            this.Data.AddString(string.Empty); // FacebookAppID
            this.Data.AddString(string.Empty); // ServerTime
            this.Data.AddString(string.Empty); // AccountCreatedDate

            this.Data.AddInt(0); // StartupCooldownSeconds
        }
    }
}