namespace GL.Servers.CoC.Packets
{
    using GL.Servers.CoC.Logic;
    using GL.Servers.DataStream;
    using GL.Servers.Extensions.Binary;
    using GL.Servers.Extensions.List;

    internal class ServerCommand : Command
    {
        internal int Id = -1;

        internal virtual ChecksumEncoder Checksum
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerCommand"/> class.
        /// </summary>
        public ServerCommand()
        {
            // ServerCommand.
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode(Reader Reader)
        {
            this.Id = Reader.ReadInt32();
            base.Decode(Reader);
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal override void Encode(ByteWriter Packet)
        {
            Packet.AddInt(this.Id);
            base.Encode(Packet);
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        internal override void Execute(Level Level)
        {
            base.Execute(Level);
        }
    }
}