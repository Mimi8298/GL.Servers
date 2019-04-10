namespace GL.Servers.CR.Packets.Messages.Client.Home
{
    using GL.Servers.CR.Core;
    using GL.Servers.CR.Logic;
    using GL.Servers.CR.Core.Network;
    using GL.Servers.CR.Logic.Enums;
    using GL.Servers.CR.Packets.Messages.Server.Home;
    using GL.Servers.DataStream;
    using GL.Servers.Extensions.Binary;

    internal class Visit_Home_Message : Message
    {
        private int HighID;
        private int LowID;

        /// <summary>
        /// Gets the type of this message.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 14101;
            }
        }

        /// <summary>
        /// Gets the service node of this message.
        /// </summary>
        internal override Node ServiceNode
        {
            get
            {
                return Node.Home;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Visit_Home_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Visit_Home_Message(Device Device, ByteStream ByteStream) : base(Device, ByteStream)
        {
            // Visit_Home_Message.
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.HighID = this.Data.ReadInt();
            this.LowID  = this.Data.ReadInt();
        }

        /// <summary>
        /// Processes this message.
        /// </summary>
        internal override async void Process()
        {
            Player Player = await Resources.Players.GetAsync(this.HighID, this.LowID, Store: false);

            if (Player != null)
            {
                new Visited_Home_Data_Message(this.Device, Player).Send();
            }
            else
            {
                Logging.Error(this.GetType(), this.Device, "Error when asking a player profile [" + this.HighID + "-" + this.LowID + "], player was not found.");
            }
        }
    }
}