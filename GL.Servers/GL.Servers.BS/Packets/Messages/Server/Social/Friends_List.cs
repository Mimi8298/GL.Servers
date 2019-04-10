namespace GL.Servers.BS.Packets.Messages.Server
{
    using GL.Servers.BS.Logic;
    using GL.Servers.Extensions.List;

    internal class Friends_List : Message
    {
        private string[] Friends;

        /// <summary>
        /// Initializes a new instance of the <see cref="Friends_List"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Friends">The friends identifiers.</param>
        public Friends_List(Device Device, string[] Friends) : base(Device)
        {
            this.Identifier = 20105;
            this.Friends    = Friends;
        }

        /// <summary>
        /// Encodes this message.
        /// </summary>
        internal override void Encode()
        {

            /* 00-00-00-01
             * 00-00-00-01
             * 
             * 00-00-00-01
             * 00-00-54-35
             * 
             * 00-00-00-0A-47-61-4C-61-58-79-31-30-33-36
             * 00-00-00-0F-34-37-39-38-37-37-33-31-39-30-32-31-31-32-32
             * 00-00-00-0C-47-3A-31-39-32-38-38-35-38-39-32-36
             * FF-FF-FF-FF
             * 00-00-00-03
             * 00-00-00-14
             * FF-FF-FF-FF
             * 00-00-00-00
             * 00
             * FF-FF-FF-FF
             * 1C-00 */

            this.Data.AddInt(1);
            this.Data.AddInt(1);

            // foreach (string AccountID in this.Friends)
            {
                this.Data.AddInt(this.Device.Player.HighID);
                this.Data.AddInt(this.Device.Player.LowID);

                this.Data.AddString(this.Device.Player.Name);                   // Name
                this.Data.AddString(this.Device.Player.Facebook.Identifier);    // App-Scoped FB User ID | E.g : https://graph.facebook.com/v2.2/1083844704999868/picture
                this.Data.AddString(this.Device.Player.Gamecenter.Identifier);  // Gamecenter ID

                this.Data.AddString(null);

                this.Data.AddVInt(this.Device.Player.Level);
                this.Data.AddVInt(this.Device.Player.Info.Trophies);

                this.Data.AddString(null);
                this.Data.AddInt(0);

                this.Data.AddBool(this.Device.Player.ClanLowID > 0);
                {
                    if (this.Device.Player.ClanLowID > 0)
                    {
                        // Clan Info
                    }
                }

                this.Data.AddString(null);
                this.Data.AddVInt(28);
                this.Data.AddVInt(this.Device.Player.Info.Thumbnail);
            }
        }
    }
}
 