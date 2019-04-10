namespace GL.Servers.CoC.Packets.Messages.Client.GlobalChat
{
    using System;

    using GL.Servers.CoC.Core;
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Packets.Enums;
    using GL.Servers.Extensions.Binary;

    internal class Send_Global_Chat_Line_Message : Message
    {
        private string Message;

        /// <summary>
        /// Gets a value indicating the message type.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 14715;
            }
        }

        /// <summary>
        /// Gets a value indicating the server node of message.
        /// </summary>
        internal override ServiceNode Node
        {
            get
            {
                return ServiceNode.GlobalChat;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Send_Global_Chat_Line_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Send_Global_Chat_Line_Message(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Send_Global_Chat_Line_Message.
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.Message = this.Reader.ReadString();
        }

        /// <summary>
        /// Process this instance.
        /// </summary>
        internal override void Process()
        {
            if (this.Device.Chat != null)
            {
                if (DateTime.UtcNow.AddSeconds(-2) >= this.Device.LastGlobalChatEntry)
                {
                    if (!string.IsNullOrEmpty(this.Message))
                    {
                        if (this.Message.StartsWith(Factory.Delimiter.ToString()))
                        {
                            Debug Debug = Factory.CreateDebug(this.Message, this.Device.GameMode.Level.Player);

                            if (Debug != null)
                            {
                                Debug.Decode();
                                Debug.Process();

                                return;
                            }
                        }

                        if (this.Message.Length <= 128)
                        {
                            this.Message = Resources.Regex.Replace(this.Message, " ");

                            if (this.Message.Length > 0)
                            {
                                this.Device.Chat.AddEntry(this.Device, this.Message);
                            }
                        }
                    }
                }
            }
        }
    }
}