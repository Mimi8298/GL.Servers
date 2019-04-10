namespace GL.Servers.CR.Packets.Messages.Client.Socials
{
    using GL.Servers.CR.Logic;
    using GL.Servers.DataStream;
    using GL.Servers.Extensions.Binary;

    internal class Bind_Gamecenter_Account : Message
    {
        private int Unknown;

        private string GamecenterID;
        private string Certificate;
        private string AppBundle;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bind_Gamecenter_Account"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Bind_Gamecenter_Account(Device Device, ByteStream ByteStream) : base(Device, ByteStream)
        {
            // Bind_Gamecenter_Account.
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.Unknown        = this.Data.ReadVInt();

            this.GamecenterID   = this.Data.ReadString();
            this.Certificate    = this.Data.ReadString();
            this.AppBundle      = this.Data.ReadString();
        }

        /// <summary>
        /// Processes this message.
        /// </summary>
        internal override void Process()
        {
            /*
            if (!string.IsNullOrEmpty(this.GamecenterID))
            {
                Player Friend = Resources.Friends.Get(this.Device.GameMode.Player.Facebook.Identifier, this.Device.GameMode.Player.Google.Identifier, this.GamecenterID);

                if (Friend != null)
                {
                    if (Friend.PlayerID == this.Device.GameMode.Player.PlayerID)
                    {
                        // Logging.Info(this.GetType(), "Warning, Friend ID == Player ID.");

                        this.Device.GameMode.Player.Gamecenter.Identifier    = this.GamecenterID;
                        this.Device.GameMode.Player.Gamecenter.Certificate   = this.Certificate;
                        this.Device.GameMode.Player.Gamecenter.AppBundle     = this.AppBundle;
                    }
                    else
                    {
                        // new Device_Already_Bound(this.Device, Friend).Send();
                    }
                }
                else
                {
                    // Logging.Info(this.GetType(), "Warning, this is the first time the player bind his account.");

                    this.Device.GameMode.Player.Gamecenter.Identifier    = this.GamecenterID;
                    this.Device.GameMode.Player.Gamecenter.Certificate   = this.Certificate;
                    this.Device.GameMode.Player.Gamecenter.AppBundle     = this.AppBundle;
                }
            }
            else
            {
                Logging.Error(this.GetType(), "Warning, the gamecenter identifier was null or empty.");
            }
            */
        }
    }
}