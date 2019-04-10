namespace GL.Servers.CoC.Packets.Messages.Server.GameCenter
{
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Packets.Enums;
    using GL.Servers.Extensions.List;

    internal class GameCenter_Account_Already_Bound_Message : Message
    {
        private Player Player;

        /// <summary>
        /// Gets a value indicating the message type.
        /// </summary>
        internal override short Type
        {
            get
            {
                return -1; // 24212
            }
        }

        /// <summary>
        /// Gets a value indicating the service node.
        /// </summary>
        internal override ServiceNode Node
        {
            get
            {
                return ServiceNode.Account;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameCenter_Account_Already_Bound_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Command">The command.</param>
        public GameCenter_Account_Already_Bound_Message(Device Device, Player Player) : base(Device)
        {
            this.Player     = Player;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {

        }
    }
}