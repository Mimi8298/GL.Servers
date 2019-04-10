namespace GL.Servers.SL.Packets.Messages.Server
{
    using GL.Servers.Extensions.List;
    using GL.Servers.SL.Logic;

    internal class Server_Commands : Message
    {
        private readonly Command Command;

        /// <summary>
        /// Initializes a new instance of the <see cref="Server_Commands"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Command">The command.</param>
        public Server_Commands(Device Device, Command Command) : base(Device)
        {
            this.Identifier = 24111;
            this.Command    = Command;
        }

        /// <summary>
        /// Encodes the <see cref="Message" />, using the <see cref="Writer" /> instance.
        /// </summary>
        internal override void Encode()
        {
            this.Data.AddInt(this.Command.Type);
            this.Command.Encode(this.Data);
        }
    }
}