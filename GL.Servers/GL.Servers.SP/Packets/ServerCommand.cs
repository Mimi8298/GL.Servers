namespace GL.Servers.SP.Packets
{
    using GL.Servers.SP.Logic;
    using GL.Servers.Extensions.Binary;
    using GL.Servers.Extensions.List;
    using GL.Servers.SP.Logic.Mode;

    internal class ServerCommand : Command
    {
        internal int Id = -1;

        internal virtual int Checksum
        {
            get
            {
                return 0;
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
        internal override void Execute(GameMode GameMode)
        {
            base.Execute(GameMode);
        }
    }
}