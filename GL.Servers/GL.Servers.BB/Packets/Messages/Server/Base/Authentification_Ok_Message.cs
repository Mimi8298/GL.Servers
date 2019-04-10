namespace GL.Servers.BB.Packets.Messages.Server.Base
{
    using GL.Servers.BB.Extensions;
    using GL.Servers.BB.Extensions.Helper;
    using GL.Servers.BB.Extensions.Utils;
    using GL.Servers.BB.Files;
    using GL.Servers.BB.Logic;
    using GL.Servers.BB.Logic.Enums;
    using GL.Servers.BB.Packets.Enums;

    using GL.Servers.Extensions.List;

    internal class Authentification_Ok_Message : Message
    {
        internal const string FacebookAppID = "";
        internal const string GoogleServiceID = "";

        internal override short Type
        {
            get
            {
                return 20104;
            }
        }

        internal override ServiceNode ServiceNode
        {
            get
            {
                return ServiceNode.Base;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Authentification_Ok_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Authentification_Ok_Message(Device Device) : base (Device)
        {
            this.Device.State   = DeviceState.Game;        
            this.Version        = 1;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddInt(this.Device.GameMode.Level.Player.HighID);
            this.Data.AddInt(this.Device.GameMode.Level.Player.LowID);

            this.Data.AddInt(this.Device.GameMode.Level.Player.HighID);
            this.Data.AddInt(this.Device.GameMode.Level.Player.LowID);

            this.Data.AddString(this.Device.GameMode.Level.Player.PassToken);

            this.Data.AddString(null); // FacebookID
            this.Data.AddString(null); // GameCenterID

            this.Data.AddInt(GameSettings.MajorVersion);
            this.Data.AddInt(GameSettings.BuildVersion);
            this.Data.AddInt(0); // Content Version

            this.Data.AddString("stage");
            this.Data.AddString(FacebookAppID);
            this.Data.AddString("fr");

            this.Data.AddString(TileUtil.Timestamp.ToString());
            this.Data.AddString(GoogleServiceID); // GoogleServiceID

            this.Data.AddBoolean(true);
            this.Data.AddCompressableString(Update.Json);

            {
                this.Data.AddRange("01-00-00-00-03-00-00-00-12-41-42-5F-54-45-53-54-49-4E-47-5F-45-4E-41-42-4C-45-44-01-00-00-00-14-52-45-44-5F-43-41-4D-50-41-49-47-4E-5F-45-4E-41-42-4C-45-44-00-00-00-00-15-52-45-44-5F-43-41-4D-50-41-49-47-4E-5F-53-4F-4C-44-5F-4F-55-54".HexaToBytes());
            }

            this.Data.AddString("0"); // AccountCreationDate (in ms)
            this.Data.AddInt(1); // Tier
            this.Data.AddString("http://df70a89d32075567ba62-1e50fe9ed7ef652688e6e5fff773074c.r40.cf1.rackcdn.com/"); // ContentURL
            this.Data.AddString(null); // ChronosContentURL
        }
    }
}
