namespace GL.Servers.CR.Packets.Messages.Client.Home
{
    using GL.Servers.CR.Core;
    using GL.Servers.CR.Logic;
    using GL.Servers.CR.Core.Network;
    using GL.Servers.CR.Logic.Enums;
    using GL.Servers.CR.Packets.Messages.Server.Home;
    using GL.Servers.DataStream;
    using GL.Servers.Extensions.Binary;
    using State = GL.Servers.CR.Logic.Mode.Enums.State;

    internal class Go_Home_Message : Message
    {
        /// <summary>
        /// Gets the type of this message.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 14101;
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
        /// Initializes a new instance of the <see cref="Go_Home_Message"/> class.
        /// </summary>
        public Go_Home_Message(Device Device, ByteStream ByteStream) : base(Device, ByteStream)
        {
            // Go_Home_Message.
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.Data.ReadInt();
            this.Data.ReadVInt();
        }

        /// <summary>
        /// Processes this message.
        /// </summary>
        internal override void Process()
        {
            if (this.Device.GameMode.State <= State.Home)
            {
                new Own_Home_Data_Message(this.Device, Resources.Players.Get(this.Device.MessageManager.AccountHighId, this.Device.MessageManager.AccountLowId)).Send();
            }
        }
    }
}