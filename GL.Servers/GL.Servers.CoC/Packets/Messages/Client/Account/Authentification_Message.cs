namespace GL.Servers.CoC.Packets.Messages.Client.Account
{
    using System;
    using System.Linq;

    using GL.Servers.CoC.Core;
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Core.Network;
    using GL.Servers.CoC.Packets.Enums;
    using GL.Servers.CoC.Extensions.Utils;
    using GL.Servers.CoC.Extensions.Helper;
    using GL.Servers.CoC.Files.CSV_Logic.Logic;
    using GL.Servers.CoC.Logic.Slots;
    using GL.Servers.CoC.Packets.Messages.Server.Account;
    using GL.Servers.CoC.Packets.Messages.Server.Home;
    using GL.Servers.CoC.Packets.Messages.Server.Avatar;
    using GL.Servers.CoC.Packets.Messages.Server.Alliance;

    using GL.Servers.Logic.Enums;
    using GL.Servers.Extensions.Binary;

    internal class Authentification_Message : Message
    {
        private int HighID;
        private int LowID;

        private string PassToken;
        private string MasterHash;
        private string GameVersion;

        private int MajorVersion;
        private int MinorVersion;
        private int BuildVersion;

        private int Seed;

        private LocaleData LocaleData;

        /// <summary>
        /// Gets a value indicating the message type.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 10101;
            }
        }

        /// <summary>
        /// Gets a value indicating the service node.
        /// </summary>
        internal override ServiceNode Node
        {
            get
            {
                return ServiceNode.Account;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Authentification_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Authentification_Message(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Authentification_Message.
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.HighID       = this.Reader.ReadInt32();
            this.LowID        = this.Reader.ReadInt32();

            this.PassToken    = this.Reader.ReadString();

            this.MajorVersion = this.Reader.ReadInt32();
            this.MinorVersion = this.Reader.ReadInt32();
            this.BuildVersion = this.Reader.ReadInt32();

            this.MasterHash   = this.Reader.ReadString();

            this.Reader.ReadString();
            this.Device.Info.UDID        = this.Reader.ReadString();
            this.Device.Info.OpenUDID    = this.Reader.ReadString();
            this.Device.Info.DeviceModel = this.Reader.ReadString();

            if (!this.Reader.EndOfStream)
            {
                this.LocaleData     = this.Reader.ReadData<LocaleData>();
                this.Device.Info.PreferredLanguageId = this.Reader.ReadString();

                if (!this.Reader.EndOfStream)
                {
                    this.Device.Info.ADID = this.Reader.ReadString();

                    if (!this.Reader.EndOfStream)
                    {
                        this.Device.Info.OSVersion = this.Reader.ReadString();

                        if (!this.Reader.EndOfStream)
                        {
                            this.Device.Info.Android = this.Reader.ReadBoolean();

                            if (!this.Reader.EndOfStream)
                            {
                                this.Reader.ReadString(); // Never null
                                this.Reader.ReadString(); // Never null

                                if (!this.Reader.EndOfStream)
                                {
                                    this.Reader.ReadString(); // Never null

                                    if (!this.Reader.EndOfStream)
                                    {
                                        this.Device.Info.Advertising = this.Reader.ReadBoolean();
                                        this.Reader.ReadString();

                                        if (!this.Reader.EndOfStream)
                                        {
                                            this.Seed = this.Reader.ReadInt32();

                                            if (!this.Reader.EndOfStream)
                                            {
                                                this.Reader.ReadVInt(); // AppStore

                                                this.Reader.ReadString(); // Never null, probably KunlunSSO
                                                this.Reader.ReadString(); // Never null, probably KunlunUID 

                                                if (!this.Reader.EndOfStream)
                                                {
                                                    this.GameVersion = this.Reader.ReadString(); // Never null

                                                    if (!this.Reader.EndOfStream)
                                                    {
                                                        this.Reader.ReadString(); // Never null
                                                        this.Reader.ReadString(); // Never null

                                                        this.Reader.ReadVInt(); // Unknown
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Processes this message.
        /// </summary>
        internal override void Process()
        {
            if (this.Trusted())
            {
                if (this.LowID == 0)
                {
                    Accounts.Account Account = Resources.Accounts.CreateAccount();

                    if (Account.Player != null && Account.Home != null)
                    {
                        this.Login(Account);
                    }
                    else
                    {
                        new Authentification_Failed_Message(this.Device, Reason.Pause).Send();
                    }
                }
                else if (this.LowID > 0)
                {
                    Accounts.Account Account = Resources.Accounts.LoadAccount(this.HighID, this.LowID, this.PassToken);

                    if (Account.Player != null && Account.Home != null)
                    {
                        this.Login(Account);
                    }
                    else
                    {
                        new Authentification_Failed_Message(this.Device, Reason.Reset).Send();
                    }
                }
                else
                {
                    Logging.Error(this.GetType(), this.Device, "Player tried to login with a player id inferior to 0.");
                }
            }
        }

        internal bool Trusted()
        {
            // if (this.ValuesAreCorrect())
            {
                if (this.MajorVersion == Logic.Version.ClientMajorVersion && this.MinorVersion == 0 && this.BuildVersion == Logic.Version.ClientBuildVersion)
                {
                    if (Constants.MaintenanceTime < DateTime.UtcNow)
                    {
                        // if (string.Equals(this.MasterHash, Fingerprint.Sha))
                        {
                            return true;
                        }
                        // else
                        {
                            // new Authentification_Failed(this.Device, Reason.Patch).Send();
                        }
                    }
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
            /*else
            {
                Resources.TCPGateway.Disconnect(this.Device.Token.Args);
            }*/

            return false;
        }

        /// <summary>
        /// Checks if values are correct.
        /// </summary>
        /// <returns>A bool indicating whether the values are correct or no.</returns>
        internal bool ValuesAreCorrect()
        {
            if (this.HighID < 0)
            {
                return false;
            }

            if (this.LowID < 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Logins this instance.
        /// </summary>
        internal void Login(Accounts.Account Account)
        {
            if (this.Device.ReceiveEncrypter.IsRC4)
            {
                new Encryption_Message(this.Device, this.Seed).Send();
            }

            new Authentification_Ok_Message(this.Device).Send();

            return;

            new Own_Home_Data_Message(this.Device, Account.Home, Account.Player, TimeUtil.Timestamp, 0).Send();
            new Avatar_Stream_Message(this.Device, this.Device.GameMode.Level.Player.Logs).Send();

            if (this.Device.GameMode.Level.Player.InAlliance)
            {
                new Alliance_Stream_Message(this.Device, this.Device.GameMode.Level.Player.Alliance.Streams.Slots.Values.ToArray()).Send();
                this.Device.GameMode.Level.Player.Alliance.Members.Connected.TryAdd(this.Device.GameMode.Level.Player.PlayerID, this.Device.GameMode.Level.Player);
            }

            if (this.Device.Chat == null)
            {
                Resources.Chats.Join(this.Device);
            }
        }
    }
}
