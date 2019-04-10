namespace GL.Servers.CoC.Packets.Messages.Server.Home
{
    using GL.Servers.CoC.Extensions.Utils;
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Packets.Enums;

    internal class Enemy_Home_Data_Message : Message
    {
        internal Home Home;
        internal Player Player;
        internal Player Attacker;
        internal int Timestamp;
        internal int SecondsSinceLastSave;

        /// <summary>
        /// Gets a value indicating the message type.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 24107;
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

        public Enemy_Home_Data_Message(Device Device, Home Home, Player Player, Player Attacker) : base(Device)
        {
            this.Home = Home;
            this.Player = Player;
            this.Attacker = Attacker;
            this.Timestamp = TimeUtil.Timestamp;
            this.SecondsSinceLastSave = 0;
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddInt(this.SecondsSinceLastSave);
            this.Data.AddInt(-1);
            this.Data.AddInt(this.Timestamp);

            this.Home.Encode(this.Data);
            this.Player.Encode(this.Data);

            this.Data.AddInt(0);
            this.Data.AddInt(0);

            this.Attacker.Encode(this.Data);

            this.Data.AddInt(0);
            this.Data.AddInt(0);

            this.Data.AddInt(3);
            this.Data.AddInt(0);

            if (false)
            {
                this.Data.AddBoolean(true);
                this.Data.AddLong(0);
            }
            else
                this.Data.AddBoolean(false);
        }
    }
}