namespace GL.Servers.SL.Packets.Messages.Client
{
    using GL.Servers.SL.Logic;
    using GL.Servers.Extensions.Binary;
    using GL.Servers.SL.Core.Network;
    using GL.Servers.SL.Packets.Messages.Server;

    internal class Ask_For_Avatar_Stream_Data : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Unknown_14405"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Ask_For_Avatar_Stream_Data(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Ask_For_Avatar_Stream_Data.
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Process()
        {
            new Avatar_Stream_Data(this.Device).Send();
        }
    }
}