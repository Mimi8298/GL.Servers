namespace GL.Servers.CoC.Packets.Messages.Server.Avatar
{
    using System.Collections.Generic;

    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Logic.Log;
    using GL.Servers.CoC.Packets.Enums;

    using GL.Servers.Extensions.List;

    internal class Avatar_Stream_Message : Message
    {
        internal List<AvatarStreamEntry> AvatarStreamEntries;

        /// <summary>
        /// Gets a value indicating the message type.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 24411;
            }
        }

        /// <summary>
        /// Gets a value indicating the service node of message.
        /// </summary>
        internal override ServiceNode Node
        {
            get
            {
                return ServiceNode.Avatar;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Avatar_Stream_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Avatar_Stream_Message(Device Device, List<AvatarStreamEntry> AvatarStreamEntries) : base (Device)
        {
            this.AvatarStreamEntries = AvatarStreamEntries;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddInt(this.AvatarStreamEntries.Count);

            this.AvatarStreamEntries.ForEach(Entry =>
            {
                this.Data.AddInt(Entry.Type);
                Entry.Encode(this.Data);
            });
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            this.Device.GameMode.GameLogManager.Logs = this.AvatarStreamEntries;
        }
    }
}
