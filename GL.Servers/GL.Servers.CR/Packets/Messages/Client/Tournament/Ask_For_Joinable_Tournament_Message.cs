namespace GL.Servers.CR.Packets.Messages.Client.Tournament
{
    using GL.Servers.CR.Logic;
    using GL.Servers.CR.Logic.Enums;
    using GL.Servers.DataStream;

    internal class Ask_For_Joinable_Tournament_Message : Message
    {
        /// <summary>
        /// Gets the type of this message.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 16103;
            }
        }

        /// <summary>
        /// Gets the service node of this message.
        /// </summary>
        internal override Node ServiceNode
        {
            get
            {
                return Node.Avatar;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ask_For_Joinable_Tournament_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Ask_For_Joinable_Tournament_Message(Device Device, ByteStream ByteStream) : base(Device, ByteStream)
        {
            // Ask_For_Joinable_Tournament_Message.
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode()
        {
            this.Data.ReadVInt();
            this.Data.ReadVInt();

            this.Data.ReadBoolean();
        }
    }
}