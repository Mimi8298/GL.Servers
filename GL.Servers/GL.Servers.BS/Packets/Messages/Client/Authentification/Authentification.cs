namespace GL.Servers.BS.Packets.Messages.Client
{
    using System;
    using System.Linq;

    using GL.Servers.BS.Core;
    using GL.Servers.BS.Core.Network;
    using GL.Servers.BS.Extensions;
    using GL.Servers.BS.Files.CSV_Logic;
    using GL.Servers.BS.Logic;
    using GL.Servers.BS.Logic.Enums;
    using GL.Servers.BS.Packets.Messages.Server;
	
    using GL.Servers.Extensions.Binary;
    using GL.Servers.Logic.Enums;

    using Resources = GL.Servers.BS.Core.Resources;

    internal class Authentification : Message
    {
        private int HighID;
        private int LowID;

        private string Token;
        private string MasterHash;
        private string Region;
        private string SVersion;

        private int Major;
        private int Minor;
        private int Revision;

        /// <summary>
        /// Initializes a new instance of the <see cref="Authentification"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Authentification(Device Device, Reader Reader) : base(Device, Reader)
        {
            this.Device.State = State.LOGIN;
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.HighID             = this.Reader.ReadInt32();
            this.LowID              = this.Reader.ReadInt32();
            this.Token              = this.Reader.ReadString();

            this.Major              = this.Reader.ReadInt32();
            this.Revision           = this.Reader.ReadInt32();
            this.Minor              = this.Reader.ReadInt32();

            this.MasterHash         = this.Reader.ReadString();

            this.Reader.ReadString();

            this.Device.OpenUDID    = this.Reader.ReadString();
            this.Device.MACAddress  = this.Reader.ReadString();
            this.Device.Model       = this.Reader.ReadString();

            if (!this.Reader.EndOfStream)
            {
                this.Reader.ReadData<Locales>();
                this.Reader.ReadString();

                if (!this.Reader.EndOfStream)
                {
                    this.Reader.ReadString();

                    if (!this.Reader.EndOfStream)
                    {
                        this.Reader.ReadString();

                        if (!this.Reader.EndOfStream)
                        {
                            this.Reader.ReadBoolean();

                            if (!this.Reader.EndOfStream)
                            {
                                this.Reader.ReadString();
                                this.Reader.ReadString();

                                if (!this.Reader.EndOfStream)
                                {
                                    this.Reader.ReadString();

                                    if (!this.Reader.EndOfStream)
                                    {
                                        this.Reader.ReadBoolean();
                                        this.Reader.ReadString();

                                        if (!this.Reader.EndOfStream)
                                        {
                                            this.Reader.ReadInt32();

                                            if (!this.Reader.EndOfStream)
                                            {
                                                this.Reader.ReadVInt();

                                                this.Reader.ReadString();
                                                this.Reader.ReadString();

                                                if (!this.Reader.EndOfStream)
                                                {
                                                    this.Reader.ReadString();

                                                    if (!this.Reader.EndOfStream)
                                                    {
                                                        this.Reader.ReadString();
                                                        this.Reader.ReadString();

                                                        this.Reader.ReadVInt();
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
                    this.Device.Player          = Resources.Players.New(this.Device);

                    if (this.Device.Player != null)
                    {
                        this.Login();
                    }
                    else
                    {
                        new Authentification_Failed(this.Device, Reason.Pause).Send();
                    }
                }
                else if (this.LowID > 0)
                {
                    this.Device.Player          = Resources.Players.Get(this.Device, this.HighID, this.LowID);

                    if (this.Device.Player != null)
                    {
                        if (string.Equals(this.Token, this.Device.Player.Token))
                        {
                            if (!this.Device.Player.Banned)
                            {
                                this.Login();
                            }
                            else
                            {
                                new Authentification_Failed(this.Device, Reason.Banned).Send();
                            }
                        }
                        else
                        {
                            new Authentification_Failed(this.Device, Reason.Reset).Send();
                        }
                    }
                    else
                    {
                        new Authentification_Failed(this.Device, Reason.Reset).Send();
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
            if (this.ValuesAreCorrect())
            {
                if (Constants.Local)
                {
                    string IPAddress = this.Device.Socket.RemoteEndPoint.ToString();

                    if (!(Constants.AuthorizedIP.Contains(IPAddress.Split(':')[0]) || IPAddress.StartsWith("192.168.")))
                    {
                        new Authentification_Failed(this.Device, Reason.Maintenance).Send();

                        return false;
                    }
                }

                if (this.Major == (int) CVersion.Major && this.Minor == (int) CVersion.Minor)
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
                        new Authentification_Failed(this.Device, Reason.Maintenance).Send();
                    }
                }
                else
                {
                    new Authentification_Failed(this.Device, Reason.Update).Send();
                }
            }
            else
            {
                Resources.TCPGateway.Disconnect(this.Device.Token.Args);
            }

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

            if (string.IsNullOrEmpty(this.MasterHash))
            {
                return false;
            }

            if (this.MasterHash.Length != 40)
            {
                return false;
            }

            if (string.IsNullOrEmpty(this.Region))
            {
                return false;
            }

            if (string.IsNullOrEmpty(this.Device.OpenUDID))
            {
                return false;
            }

            if (string.IsNullOrEmpty(this.Device.AdvertiseID))
            {
                return false;
            }

            if (string.IsNullOrEmpty(this.Device.Model))
            {
                return false;
            }

            if (string.IsNullOrEmpty(this.Device.OSVersion))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Logins this instance.
        /// </summary>
        internal void Login()
        {
            this.Device.Player.Region   = this.Region.ToUpper();

            new Authentification_OK(this.Device).Send();
            new Own_Home_Data(this.Device).Send();

            if (this.Device.Player.ClanLowID > 0)
            {
                Clan Clan = this.Device.Player.Clan;

                if (Clan != null)
                {
                    new Clan_Info(this.Device, Clan).Send();
                    new Clan_Stream(this.Device, Clan).Send();
                }
                else
                {
                    Logging.Error(this.GetType(), "Clan not found when Process() has been called.");
                }
            }
            else
            {
                new Clan_Info(this.Device, null).Send();
            }
        }
    }
}
