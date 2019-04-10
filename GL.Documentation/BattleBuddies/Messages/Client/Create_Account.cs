namespace GL.Servers.BU.Packets.Messages.Client
{
    using GL.Servers.BU.Core.Network;
    using GL.Servers.BU.Extensions.Binary;
    using GL.Servers.BU.Logic;
    using GL.Servers.BU.Logic.Enums;
    using GL.Servers.BU.Packets.Messages.Server;

    internal class Create_Account : Message
    {
        internal string GamecenterID;
        internal string FacebookID;

        /// <summary>
        /// Initializes a new instance of the <see cref="Create_Account"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        public Create_Account(Device Device, Reader Reader) : base(Device, Reader)
        {
            this.Device.State = State.LOGIN;
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            // 00-00-50-06  FF-FF-FF-FF  FF-FF-FF-FF  00-00-00-0B-47-3A-33-32-35-33-37-38-36-37-31  FF-FF-FF-FF  FF-FF-FF-FF
            // 00-00-50-06  FF-FF-FF-FF  FF-FF-FF-FF  00-00-00-0B-47-3A-33-32-35-33-37-38-36-37-31  FF-FF-FF-FF  FF-FF-FF-FF

            this.Reader.ReadInt32(); // Header

            this.Reader.ReadString();

            this.FacebookID     = this.Reader.ReadString();
            this.GamecenterID   = this.Reader.ReadString();

            this.Reader.ReadString();
            this.Reader.ReadString();
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            new Create_Account_OK(this.Device).Send();
        }
    }
}