namespace GL.Servers.CoC.Packets.Messages.Client.Clan
{
    using GL.Servers.CoC.Core;
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Packets.Enums;
    using GL.Servers.CoC.Logic.Clan.StreamEntry;

    using GL.Servers.Extensions.Binary;

    internal class Chat_To_Alliance_Stream_Message : Message
    {
        private string Message;

        /// <summary>
        /// Gets a value indicating the message type.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 14315;
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
        /// Initializes a new instance of the <see cref="Chat_To_Alliance_Stream_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Chat_To_Alliance_Stream_Message(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Chat_To_Alliance_Stream_Message.
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode()
        {
            this.Message = this.Reader.ReadString();
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            if (this.Device.GameMode.Level.Player.InAlliance)
            {
                if (!string.IsNullOrWhiteSpace(this.Message))
                {
                    if (this.Message.Length <= 128)
                    {
                        this.Message = Resources.Regex.Replace(this.Message, " ");

                        if (this.Message.StartsWith(" "))
                        {
                            this.Message = this.Message.Remove(0, 1);
                        }

                        if (this.Message.Length > 0)
                        {
                            this.Device.GameMode.Level.Player.Alliance.Streams.AddEntry(new ChatStreamEntry(this.Device.GameMode.Level.Player.AllianceMember, this.Message));
                        }
                    }
                }
            }
        }
    }
}