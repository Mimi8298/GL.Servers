namespace GL.Servers.BU.Packets.Messages.Client
{
    using GL.Servers.BU.Core.Network;
    using GL.Servers.BU.Extensions.Binary;
    using GL.Servers.BU.Logic;
    using GL.Servers.BU.Logic.Enums;
    using GL.Servers.BU.Packets.Messages.Server;

    internal class Create_Avatar : Message
    {
        internal string Name;

        /// <summary>
        /// Initializes a new instance of the <see cref="Create_Avatar"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Create_Avatar(Device Device, Reader Reader) : base(Device, Reader)
        {
            this.Device.State = State.LOGGED;
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.Name = this.Reader.ReadString();
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            new Create_Avatar_Failed(this.Device).Send();
        }
    }
}