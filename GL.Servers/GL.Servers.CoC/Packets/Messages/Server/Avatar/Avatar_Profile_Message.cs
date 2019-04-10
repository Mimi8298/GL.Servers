namespace GL.Servers.CoC.Packets.Messages.Server.Avatar
{
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Packets.Enums;
    using GL.Servers.CoC.Extensions.Helper;

    using GL.Servers.Extensions.List;

    internal class Avatar_Profile_Message : Message
    {
        private Player Player;
        private Home Home;

        /// <summary>
        /// Gets a value indicating the message type.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 24334;
            }
        }

        /// <summary>
        /// Gets a value indicating the service node of message.
        /// </summary>
        internal override ServiceNode Node
        {
            get
            {
                return ServiceNode.Avatar;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Avatar_Profile_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Avatar_Profile_Message(Device Device, Player Player, Home Home) : base (Device)
        {
            this.Player = Player;
            this.Home = Home;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Player.Encode(this.Data);

            this.Data.AddInt(0);
            this.Data.AddInt(0);

            this.Data.AddCompressedString(this.Home.Save().ToString());

            this.Data.AddInt(3); // Troop Received
            this.Data.AddInt(4); // Troop Sended
            this.Data.AddInt(5); // RemainingSecs for available in alliance war

            this.Data.Add(1);

            this.Data.AddInt(6);
        }
    }
}
