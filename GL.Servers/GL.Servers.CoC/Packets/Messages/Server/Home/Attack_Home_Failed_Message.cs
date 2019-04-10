namespace GL.Servers.CoC.Packets.Messages.Server.Home
{
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Packets.Enums;

    internal class Attack_Home_Failed_Message : Message
    {
        private AttackHomeFailedReason Reason;

        /// <summary>
        /// Gets a value indicating the message type.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 24103;
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
        /// Initializes a new instance of the <see cref="Attack_Home_Failed_Message"/> class.
        /// </summary>
        public Attack_Home_Failed_Message(Device Device, AttackHomeFailedReason Reason) : base(Device)
        {
            this.Reason     = Reason;
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