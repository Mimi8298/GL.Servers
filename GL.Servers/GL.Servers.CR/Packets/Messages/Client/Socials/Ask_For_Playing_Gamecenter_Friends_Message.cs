namespace GL.Servers.CR.Packets.Messages.Client.Socials
{
    using GL.Servers.CR.Logic;
    using GL.Servers.CR.Logic.Enums;
    using GL.Servers.DataStream;
    using GL.Servers.Extensions.Binary;

    internal class Ask_For_Playing_Gamecenter_Friends_Message : Message
    {
        private string[] FriendsID;

        /// <summary>
        /// Gets the type of this message.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 10512;
            }
        }

        /// <summary>
        /// Gets the service node of this message.
        /// </summary>
        internal override Node ServiceNode
        {
            get
            {
                return Node.Friend;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ask_For_Playing_Gamecenter_Friends_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Ask_For_Playing_Gamecenter_Friends_Message(Device Device, ByteStream ByteStream) : base(Device, ByteStream)
        {
            // Ask_For_Playing_Gamecenter_Friends_Message.
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            int Count       = this.Data.ReadVInt();

            this.FriendsID  = new string[Count];

            for (int i = 0; i < Count; i++)
            {
                this.FriendsID[i] = this.Data.ReadString();
            }
        }

        /// <summary>
        /// Processes this message.
        /// </summary>
        internal override void Process()
        {
            /*
            List<Player> Friends = new List<Player>(this.FriendsID.Length);

            foreach (string GamecenterID in this.FriendsID)
            {
                Player Player = Resources.Friends.Get(null, null, GamecenterID);

                if (Player != null)
                {
                    Friends.Add(Player);
                }
            }

            new Friends_List_Message(this.Device, Friends).Send();
            */
        }
    }
}