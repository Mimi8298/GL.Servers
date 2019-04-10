namespace GL.Servers.CoC.Packets.Messages.Server.Replay
{
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Packets.Enums;

    internal class Home_Battle_Replay_Message : Message
    {
        internal string Replay;

        /// <summary>
        /// Gets a value indicating the message type.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 24114;
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
        /// Initializes a new instance of the <see cref="Home_Battle_Replay_Message"/> class.
        /// </summary>
        public Home_Battle_Replay_Message(Device Device, string Replay) : base(Device)
        {
            this.Replay     = Replay;
        }
    }
}