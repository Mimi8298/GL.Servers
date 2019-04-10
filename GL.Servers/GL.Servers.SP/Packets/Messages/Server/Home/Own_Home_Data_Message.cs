namespace GL.Servers.SP.Packets.Messages.Server.Home
{
    using GL.Servers.SP.Logic;
    using GL.Servers.SP.Packets.Enums;

    internal class Own_Home_Data_Message : Message
    {
        private int RandomSeed;
        private int SecondsSinceLastSave;

        /// <summary>
        /// Gets a value indicating the message type.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 24101;
            }
        }

        /// <summary>
        /// Gets a value indicating the service node of message.
        /// </summary>
        internal override ServiceNode Node
        {
            get
            {
                return ServiceNode.Home;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Own_Home_Data_Message"/> class.
        /// </summary>
        public Own_Home_Data_Message(Device Device) : base(Device)
        {
            this.RandomSeed = 112;
            this.SecondsSinceLastSave = 0;
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddInt(this.SecondsSinceLastSave);
            this.Data.AddInt(this.RandomSeed);

            this.Device.GameMode.Player.Home.Encode(this.Data);
            this.Device.GameMode.Player.Encode(this.Data);
        }

        internal override void Process()
        {
            this.Device.GameMode.LoadGame(this.RandomSeed, this.SecondsSinceLastSave);
        }
    }
}