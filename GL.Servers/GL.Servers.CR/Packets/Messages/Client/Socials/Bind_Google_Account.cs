namespace GL.Servers.CR.Packets.Messages.Client.Socials
{
    using GL.Servers.CR.Logic;
    using GL.Servers.DataStream;
    using GL.Servers.Extensions.Binary;

    internal class Bind_Google_Account : Message
    {
        private int Unknown;

        private string GoogleID;
        private string Token;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bind_Google_Account"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Bind_Google_Account(Device Device, ByteStream ByteStream) : base(Device, ByteStream)
        {
            // Bind_Google_Account.
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.Unknown    = this.Data.ReadVInt();

            this.GoogleID   = this.Data.ReadString();
            this.Token      = this.Data.ReadString();
        }

        /// <summary>
        /// Processes this message.
        /// </summary>
        internal override void Process()
        {
            /*
            if (!string.IsNullOrEmpty(this.GoogleID))
            {
                Player Friend = Resources.Friends.Get(this.Device.GameMode.Player.Facebook.Identifier, this.GoogleID, this.Device.GameMode.Player.Gamecenter.Identifier);

                if (Friend != null)
                {
                    if (Friend.PlayerID == this.Device.GameMode.Player.PlayerID)
                    {
                        // Logging.Info(this.GetType(), "Warning, Friend ID == Player ID.");

                        this.Device.GameMode.Player.Google.Identifier    = this.GoogleID;
                        this.Device.GameMode.Player.Google.Token         = this.Token;
                    }
                    else
                    {
                        // new Device_Already_Bound(this.Device, Friend).Send();
                    }
                }
                else
                {
                    // Logging.Info(this.GetType(), "Warning, this is the first time the player bind his account.");

                    this.Device.GameMode.Player.Google.Identifier    = this.GoogleID;
                    this.Device.GameMode.Player.Google.Token         = this.Token;
                }
            }
            else
            {
                Logging.Error(this.GetType(), "Warning, the google identifier was null or empty.");
            }
            */
        }
    }
}