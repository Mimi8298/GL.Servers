namespace GL.Servers.CR.Packets.Messages.Client.Socials
{
    using GL.Servers.CR.Logic;

    using GL.Servers.DataStream;
    using GL.Servers.Extensions.Binary;

    internal class Bind_Facebook_Account : Message
    {
        private int Unknown;

        private string FBIdentifier;
        private string FBToken;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bind_Facebook_Account"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Bind_Facebook_Account(Device Device, ByteStream ByteStream) : base(Device, ByteStream)
        {
            // Bind_Facebook_Account.
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.Unknown        = this.Data.ReadVInt();
            this.FBIdentifier   = this.Data.ReadString();
            this.FBToken        = this.Data.ReadString();
        }

        /// <summary>
        /// Processes this message.
        /// </summary>
        internal override void Process()
        {
            /*
            if (!string.IsNullOrEmpty(this.FBIdentifier))
            {
                Player Friend           = Resources.Friends.Get(this.Device.GameMode.Player.Facebook.Identifier, this.Device.GameMode.Player.Google.Identifier, this.Device.GameMode.Player.Gamecenter.Identifier);

                if (Friend != null)
                {
                    if (Friend.PlayerID == this.Device.GameMode.Player.PlayerID)
                    {
                        // Logging.Info(this.GetType(), "Warning, Friend ID == Player ID.");

                        this.Device.GameMode.Player.Facebook.Identifier  = this.FBIdentifier;
                        this.Device.GameMode.Player.Facebook.Token       = this.FBToken;
                    }
                    else
                    {
                        // Logging.Error(this.GetType(), "Warning, Friend ID != Player ID.");
                    }
                }
                else
                {
                    // Logging.Info(this.GetType(), "Warning, this is the first time the player bind his account.");

                    this.Device.GameMode.Player.Facebook.Identifier  = this.FBIdentifier;
                    this.Device.GameMode.Player.Facebook.Token       = this.FBToken;
                }
            }
            else
            {
                Logging.Error(this.GetType(), "Warning, the facebook identifier was null or empty.");
            }
            */
        }
    }
}