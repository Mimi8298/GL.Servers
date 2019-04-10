namespace GL.Servers.GS.Packets.Messages.Server
{
    using GL.Servers.GS.Extensions.List;
    using GL.Servers.GS.Logic;

    internal class Server_Message : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Server_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        internal Server_Message(Device Device) : base(Device)
        {
            this.Identifier     = 20140;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddString(string.Empty);
        }
    }
}