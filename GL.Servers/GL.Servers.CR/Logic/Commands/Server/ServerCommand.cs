namespace GL.Servers.CR.Logic.Commands.Server
{
    using GL.Servers.DataStream;

    internal class ServerCommand : Command
    {
        internal int Id;

        /// <summary>
        /// Gets a value indicating whether the this command is a server command.
        /// </summary>
        internal override bool IsServerCommand
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerCommand"/> class.
        /// </summary>
        public ServerCommand() : base()
        {
            // ServerCommand.
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode(ByteStream Packet)
        {
            this.Id = Packet.ReadVInt();
            base.Decode(Packet);
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal override void Encode(ChecksumEncoder Packet)
        {
            Packet.AddVInt(this.Id);
            base.Encode(Packet);
        }
    }
}