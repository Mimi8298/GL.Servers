namespace GL.Servers.CR.Packets.Messages.Server.Home
{
    using System;
    using GL.Servers.CR.Extensions.Utils;
    using GL.Servers.CR.Logic;
    using GL.Servers.CR.Logic.Enums;
    using GL.Servers.Extensions.List;

    internal class Own_Home_Data_Message : Message
    {
        internal Home Home;
        internal Player Player;

        /// <summary>
        /// Gets the type of this message.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 24101;
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
        /// Initializes a new instance of the <see cref="Own_Home_Data_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Own_Home_Data_Message(Device Device, Player Player) : base(Device)
        {
            this.Home = Player.Home;
            this.Player = Player;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Home.Encode(this.Data);
            this.Player.Encode(this.Data);

            this.Data.AddVInt(TimeUtil.Timestamp);
            this.Data.AddVInt(TimeUtil.Timestamp);
            this.Data.AddVInt(TimeUtil.Timestamp);
        }

        internal override void Process()
        {
            this.Device.GameMode.LoadHomeState(this.Player, this.Home.SecondsSinceLastSave, 113);
            this.Device.GameMode.Home.LastTick = DateTime.UtcNow;
        }
    }
}