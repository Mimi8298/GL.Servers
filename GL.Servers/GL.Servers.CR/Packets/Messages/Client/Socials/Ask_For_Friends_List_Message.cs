namespace GL.Servers.CR.Packets.Messages.Client.Socials
{
    using System.Collections.Generic;

    using GL.Servers.CR.Logic;
    using GL.Servers.CR.Core.Network;
    using GL.Servers.CR.Packets.Messages.Server.Socials;
    using GL.Servers.DataStream;

    internal class Ask_For_Friends_List_Message : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Ask_For_Friends_List_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Ask_For_Friends_List_Message(Device Device, ByteStream ByteStream) : base(Device, ByteStream)
        {
            // Friends_Update.
        }

        /// <summary>
        /// Processes this message.
        /// </summary>
        internal override void Process()
        {
            new Friends_List_Message(this.Device, new List<Player>(0)).Send();
        }
    }
}