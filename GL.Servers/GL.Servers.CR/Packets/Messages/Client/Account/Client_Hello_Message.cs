namespace GL.Servers.CR.Packets.Messages.Client.Account
{
    using System;
    using System.Linq;
    using GL.Servers.CR.Core;
    using GL.Servers.CR.Core.Network;
    using GL.Servers.CR.Logic;
    using GL.Servers.CR.Logic.Enums;
    using GL.Servers.CR.Packets.Crypto;
    using GL.Servers.CR.Packets.Messages.Server.Account;
    using GL.Servers.DataStream;
    using GL.Servers.Extensions.Binary;
    using GL.Servers.Files;
    using GL.Servers.Library.TweetNaCl;
    using GL.Servers.Logic.Enums;

    internal class Client_Hello_Message : Message
    {
        private int AppStore;
        private int DeviceType;
        private int KeyVersion;
        private int Protocol;
        private int MajorVersion;
        private int MinorVersion;
        private int BuildVersion;

        private string MasterHash;

        internal const string ProdHost = "gcroyale.gobelinland.fr";
        internal const string DevHost = "";
        internal const string KunlunHost = "";


        /// <summary>
        /// Gets the type of this message.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 10100;
            }
        }

        /// <summary>
        /// Gets the service node of this message.
        /// </summary>
        internal override Node ServiceNode
        {
            get
            {
                return Node.Account;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Pre_Authentification"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Client_Hello_Message(Device Device, ByteStream ByteStream) : base(Device, ByteStream)
        {
            this.Device.State = State.SESSION;
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.Protocol   = this.Data.ReadInt();
            this.KeyVersion = this.Data.ReadInt();
            this.MajorVersion = this.Data.ReadInt();
            this.MinorVersion = this.Data.ReadInt();
            this.BuildVersion = this.Data.ReadInt();
            this.MasterHash = this.Data.ReadString();
            this.DeviceType = this.Data.ReadInt();
            this.AppStore   = this.Data.ReadInt();
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            if (this.Protocol == 1)
            {
                if (this.MajorVersion == Logic.Version.ClientMajorVersion && this.MinorVersion == 0 && this.BuildVersion == Logic.Version.ClientBuildVersion)
                {
                    if (Constants.Maintenance <= DateTime.UtcNow)
                    {
                        if (string.Equals(this.MasterHash, Fingerprint.Sha))
                        {
                            if (PepperFactory.ServerSecretKeys.TryGetValue(this.KeyVersion, out byte[] SecretKey))
                            {
                                if (this.DeviceType == 3)
                                {
                                    if (!Logic.Version.IsDev)
                                    {
                                        new Authentification_Failed_Message(this.Device, Reason.Redirection)
                                        {
                                            RedirectDomain = DevHost
                                        }.Send();

                                        return;
                                    }
                                    else
                                    {
                                        if (this.KeyVersion != PepperFactory.ServerSecretKeys.Keys.Last())
                                        {
                                            new Authentification_Failed_Message(this.Device, Reason.Update).Send();
                                            return;
                                        }
                                    }
                                }
                                else
                                {
                                    if (Logic.Version.IsDev)
                                    {
                                        new Authentification_Failed_Message(this.Device, Reason.Redirection)
                                        {
                                            RedirectDomain = ProdHost
                                        }.Send();

                                        return;
                                    }
                                }

                                if (this.DeviceType == 30)
                                {
                                    if (!Logic.Version.IsKunlunServer)
                                    {
                                        new Authentification_Failed_Message(this.Device, Reason.Redirection)
                                        {
                                            RedirectDomain = KunlunHost
                                        }.Send();

                                        return;
                                    }
                                }
                                else
                                {
                                    if (Logic.Version.IsKunlunServer)
                                    {
                                        new Authentification_Failed_Message(this.Device, Reason.Redirection)
                                        {
                                            RedirectDomain = ProdHost
                                        }.Send();

                                        return;
                                    }
                                }

                                if (Resources.Started)
                                {
                                    this.Device.MessageManager.PepperInit.KeyVersion = this.KeyVersion;
                                    this.Device.MessageManager.PepperInit.ServerPublicKey = new byte[32];

                                    curve25519xsalsa20poly1305.crypto_box_getpublickey(this.Device.MessageManager.PepperInit.ServerPublicKey, SecretKey);

                                    new Server_Hello_Message(this.Device).Send();
                                }
                                else
                                {
                               //     new Authentification_Failed_Message(this.Device, Reason.Maintenance).Send();
                                }
                            }
                            else
                            {
                                new Authentification_Failed_Message(this.Device, Reason.Update).Send();
                            }
                        }
                        else
                        {
                            new Authentification_Failed_Message(this.Device, Reason.Patch).Send();
                        }
                  /*  }
                    else
                    {
                        new Authentification_Failed_Message(this.Device, Reason.Maintenance).Send();
                    }
                }
                else
                {
                    new Authentification_Failed_Message(this.Device, Reason.Update).Send();
                }
            }
            else
            {
                new Authentification_Failed_Message(this.Device, Reason.Update).Send();
            }*/
            
        }
    }
}
