namespace GL.Servers.BS.Packets.Messages.Client
{
    using GL.Servers.BS.Core.Network;
    using GL.Servers.BS.Logic;
    using GL.Servers.BS.Packets.Messages.Server;
    using GL.Servers.Extensions.Binary;
    using GL.Servers.Logic.Enums;

    internal class Go_Home : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Go_Home"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Go_Home(Device Device, Reader Reader) : base(Device, Reader)
        {
            if (this.Device.State == State.IN_BATTLE)
            {
                this.Device.State = State.LOGGED;
            }
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.Reader.Read();
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            new Own_Home_Data(this.Device).Send();
        }
    }
}