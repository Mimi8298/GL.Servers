namespace GL.Servers.CoC.Packets.Messages.Server.Replay
{
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Packets.Enums;

    internal class Home_Battle_Live_Replay_Failed_Message : Message
    {
        internal HomeBattleLiveReplayFailedReason Reason;

        /// <summary>
        /// Gets a value indicating the message type.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 24117;
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
        /// Initializes a new instance of the <see cref="HomeBattleLiveReplayFailedReason"/> class.
        /// </summary>
        public Home_Battle_Live_Replay_Failed_Message(Device Device, HomeBattleLiveReplayFailedReason Reason) : base(Device)
        {
            this.Reason                     = Reason;
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddInt((int) this.Reason);
        }
    }
}