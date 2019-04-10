namespace GL.Servers.CoC.Packets.Messages.Client.Account
{
    using GL.Servers.CoC.Core;
    using GL.Servers.CoC.Core.Network;
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Packets;
    using GL.Servers.CoC.Packets.Cryptography;
    using GL.Servers.CoC.Packets.Enums;
    using GL.Servers.CoC.Packets.Messages.Server.Account;
    using GL.Servers.Extensions.Binary;
    using GL.Servers.Files;
    using GL.Servers.Logic.Enums;

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
        /// Gets a value indicating the message type.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 10100;
            }
        }

        /// <summary>
        /// Gets a value indicating the service node of message.
        /// </summary>
        internal override ServiceNode Node
        {
            get
            {
                return ServiceNode.Account;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Client_Hello_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Client_Hello_Message(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Client_Hello_Message.
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
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

        internal override void Process()
        {
            if (this.Protocol == 1)
            {
                if (this.MajorVersion == Logic.Version.ClientMajorVersion && this.BuildVersion == Logic.Version.ClientBuildVersion)
                {
                    // if (this.ContentHash.Equals(Fingerprint.Sha))
                    {
                        if (this.Device.PepperState.LoadKey(this.KeyVersion))
                        {
                            new Server_Hello_Message(this.Device).Send();
                        }
                    }
                    /*else
                        new Authentification_Failed_Message(this.Device, Reason.Patch).Send();*/
                }
                else
                    new Authentification_Failed_Message(this.Device, Reason.Update).Send();
            }
        }
    }
}