namespace GL.Servers.BB.Packets.Messages.Server.Player
{
    using System;
    using GL.Servers.BB.Logic;
    using GL.Servers.BB.Packets.Enums;

    using GL.Servers.Extensions.List;

    internal class Own_Home_Data_Message : Message
    {
        private Player Player;

        private int SecondsSinceLastSave;

        private string LastOriginCountry;
        private string LastOriginRegion;

        internal override short Type
        {
            get
            {
                return 24101;
            }
        }

        internal override ServiceNode ServiceNode
        {
            get
            {
                return ServiceNode.Player;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Own_Home_Data_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Own_Home_Data_Message(Device Device) : base (Device)
        {
            this.Player = Device.GameMode.Level.Player;

            this.LastOriginCountry    = "fr";
            this.SecondsSinceLastSave = (int) DateTime.UtcNow.Subtract(Device.GameMode.Level.Player.Update).TotalSeconds;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddInt(this.SecondsSinceLastSave);

            this.Player.Home.Encode(this.Data);
            this.Player.Encode(this.Data);

            this.Data.AddInt(-1);
            this.Data.AddInt(0);
            this.Data.AddInt(0);
            this.Data.AddInt(0);
            this.Data.AddInt(0);

            this.Data.AddString(this.LastOriginCountry);
            this.Data.AddString(this.LastOriginRegion);
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            this.Device.GameMode.LoadHomeState(this.SecondsSinceLastSave);
        }
    }
}
