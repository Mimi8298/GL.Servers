namespace GL.Servers.CR.Packets.Messages.Server.Home
{
    using GL.Servers.CR.Logic;
    using GL.Servers.CR.Logic.Enums;
    using GL.Servers.Extensions.List;

    internal class Visited_Home_Data_Message : Message
    {
        private Player Player;
        
        /// <summary>
        /// Gets the type of this message.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 24113;
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
        /// Initializes a new instance of the <see cref="Visited_Home_Data_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Player">The player.</param>
        public Visited_Home_Data_Message(Device Device, Player Player) : base(Device)
        {
            this.Player     = Player;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            // TODO Implement VisitHomeDataMessage::encode().
        }
    }
}