namespace GL.Servers.CoC.Packets.Messages.Client.Alliance
{
    using GL.Servers.CoC.Core;

    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Logic.Clan;
    using GL.Servers.CoC.Core.Network;
    using GL.Servers.CoC.Packets.Enums;
    using GL.Servers.CoC.Packets.Messages.Server.Alliance;

    using GL.Servers.Extensions.Binary;

    internal class Ask_For_Alliance_Data_Message : Message
    {
        private int AllianceHighID;
        private int AllianceLowID;

        /// <summary>
        /// Gets a value indicating the message type.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 14302;
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
        /// Initializes a new instance of the <see cref="Ask_For_Alliance_Data_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Ask_For_Alliance_Data_Message(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Ask_For_Alliance_Data_Message.
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode()
        {
            this.AllianceHighID = this.Reader.ReadInt32();
            this.AllianceLowID  = this.Reader.ReadInt32();
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override async void Process()
        {
            if (this.AllianceLowID > 0)
            {
                Alliance Alliance = await Resources.Clans.GetAsync(this.AllianceHighID, this.AllianceLowID);

                if (Alliance != null)
                {
                    new Alliance_Data_Message(this.Device, Alliance).Send();
                }
            }
        }
    }
}