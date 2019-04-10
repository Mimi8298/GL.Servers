namespace GL.Clients.CR.Packets.Messages.Client
{
    using GL.Clients.CR.Logic;
    using GL.Servers.Extensions.List;

    internal class Go_Home : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Go_Home"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Go_Home(Device Device) : base(Device)
        {
            this.Identifier = 14109;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.Add(0);
            this.Data.Add(0);
        }
    }
}