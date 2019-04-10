namespace GL.Servers.HD.Packets.Messages.Account
{
    using System;
    using GL.Servers.Extensions.Binary;
    using GL.Servers.HD.Core.Network;
    using GL.Servers.HD.Logic;

    internal class ClientHelloMessage : Message
    {
        private int Protocol;
        private int KeyVersion;
        private int DeviceType;
        private int AppStore;

        private int MajorVersion;
        private int MinorVersion;
        private int BuildVersion;

        private string ResourceSHA;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientHelloMessage"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public ClientHelloMessage(Device Device, Reader Reader) : base(Device, Reader)
        {
            // ClientHelloMessage.
        }

        internal override void Decode()
        {
            this.Protocol = this.Reader.ReadInt32();
            this.KeyVersion = this.Reader.ReadInt32();

            this.MajorVersion = this.Reader.ReadInt32();
            this.MinorVersion = this.Reader.ReadInt32();
            this.BuildVersion = this.Reader.ReadInt32();

            this.ResourceSHA = this.Reader.ReadString();

            this.DeviceType = this.Reader.ReadInt32();
            this.AppStore = this.Reader.ReadInt32();
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Process()
        {
            //if (this.Device.State == State.SESSION)
            {
                new ServerHelloMessage(this.Device).Send();
            }
        }
    }
}