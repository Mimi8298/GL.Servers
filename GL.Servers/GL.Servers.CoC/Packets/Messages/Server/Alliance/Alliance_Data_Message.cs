namespace GL.Servers.CoC.Packets.Messages.Server.Alliance
{
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Logic.Clan;
    using GL.Servers.CoC.Packets.Enums;

    internal class Alliance_Data_Message : Message
    {
        private Alliance Alliance;

        /// <summary>
        /// Gets a value indicating the message type.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 24301;
            }
        }

        /// <summary>
        /// Gets a value indicating the service node of message.
        /// </summary>
        internal override ServiceNode Node
        {
            get
            {
                return ServiceNode.Alliance;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Device"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Alliance_Data_Message(Device Device, Alliance Alliance) : base(Device)
        {
            this.Alliance   = Alliance;
        }
        
        internal override void Encode()
        {
            this.Alliance.Encode(this.Data);
        }
    }
}