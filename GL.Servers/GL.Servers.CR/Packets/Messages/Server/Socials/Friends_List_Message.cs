namespace GL.Servers.CR.Packets.Messages.Server.Socials
{
    using System.Collections.Generic;
    using GL.Servers.CR.Extensions.Helper;
    using GL.Servers.CR.Logic;
    using GL.Servers.CR.Logic.Enums;

    internal class Friends_List_Message : Message
    {
        private List<Player> Friends;

        /// <summary>
        /// Gets the type of this message.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 20105;
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
        /// Initializes a new instance of the <see cref="Friends_List_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Friends">The friends identifiers.</param>
        public Friends_List_Message(Device Device, List<Player> Friends) : base(Device)
        {
            this.Friends    = Friends;
        }

        /// <summary>
        /// Encodes this message.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddInt(this.Friends.Count);
            this.Data.AddInt(this.Friends.Count);

            foreach (Player Friend in this.Friends)
            {
                this.Data.AddLong(Friend.PlayerID);

                this.Data.AddBoolean(true);

                this.Data.AddLong(Friend.PlayerID);

                this.Data.AddString(Friend.Name);
                this.Data.AddString(Friend.Facebook.Identifier);    // https://graph.facebook.com/v2.2/1083844704999868/picture
                this.Data.AddString(Friend.Gamecenter.Identifier);

                this.Data.AddVInt(Friend.ExpLevel);
                this.Data.AddVInt(Friend.Score);

                this.Data.AddBoolean(false); // Clan.

                // this.Data.AddString(null);
                // this.Data.AddString(null);

                // this.Data.AddVInt(1);
                
                this.Data.EncodeData(Friend.Arena);

                this.Data.AddString(null);
                this.Data.AddString(null);
                
                this.Data.AddVInt(-1);

                this.Data.AddInt(0);
                this.Data.AddInt(0);
            }
        }
    }
}
 