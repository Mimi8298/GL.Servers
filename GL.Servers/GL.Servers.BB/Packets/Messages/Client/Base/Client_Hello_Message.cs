namespace GL.Servers.BB.Packets.Messages.Client.Base
{
    using GL.Servers.BB.Core;
    using GL.Servers.BB.Logic;
    using GL.Servers.BB.Core.Network;
    using GL.Servers.BB.Packets.Cryptography;
    using GL.Servers.BB.Packets.Messages.Server.Base;

    using GL.Servers.Extensions.Binary;

    internal class Client_Hello_Message : Message
    {
        internal int Protocol;
        internal int KeyVersion;

        internal int MajorVersion;
        internal int MinorVersion;
        internal int BuildVersion;

        internal int AppStore;
        internal int DeviceType;

        internal string ContentHash;

        /// <summary>
        /// Initializes a new instance of the <see cref="Client_Hello_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Client_Hello_Message(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Client_Hello_Message.
        }

        internal override void Decode()
        {
            this.Protocol = this.Reader.ReadInt32();
            this.KeyVersion = this.Reader.ReadInt32();

            this.MajorVersion = this.Reader.ReadInt32();
            this.MinorVersion = this.Reader.ReadInt32();
            this.BuildVersion = this.Reader.ReadInt32();

            this.ContentHash = this.Reader.ReadString();

            this.DeviceType = this.Reader.ReadInt32();
            this.AppStore = this.Reader.ReadInt32();
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            if (this.Protocol == 1)
            {
                if (Sodium.Keys.PublicKeys.TryGetValue(this.KeyVersion, out this.Device.Crypto.OriginalPublicKey))
                {
                    this.Device.Crypto.SessionKey = new byte[24];

                    Resources.Random.NextBytes(this.Device.Crypto.SessionKey = new byte[24]);

                    new Server_Hello_Message(this.Device).Send();
                }
            }
        }
    }
}