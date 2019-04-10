namespace GL.Servers.BS.Packets.Messages.Client
{
    using GL.Servers.BS.Core.Network;
    using GL.Servers.BS.Logic;
    using GL.Servers.BS.Packets.Messages.Server;
    using GL.Servers.Extensions.Binary;

    internal class Create_Gameroom : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Create_Gameroom"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Create_Gameroom(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Create_Gameroom.
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.Reader.ReadVInt();
            this.Reader.ReadVInt();
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            new Gameroom_Data(this.Device).Send();
        }
    }
}