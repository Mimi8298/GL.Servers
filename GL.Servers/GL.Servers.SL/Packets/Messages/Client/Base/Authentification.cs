namespace GL.Servers.SL.Packets.Messages.Client
{
    using System;
    using System.Linq;

    using GL.Servers.SL.Core;
    using GL.Servers.SL.Core.Network;
    using GL.Servers.SL.Logic;
    using GL.Servers.SL.Logic.Enums;
    using GL.Servers.SL.Packets.Messages.Server;
	
    using GL.Servers.Logic.Enums;
    using GL.Servers.Extensions.Binary;

    internal class Authentification : Message
    {
        private int HighID;
        private int LowID;

        private string Token;
        private string MasterHash;
        private string PreferredLanguage;
        private string SVersion;

        private int Major;
        private int Minor;
        private int Build;

        private int Seed;

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
            this.Minor              = this.Reader.ReadInt32();
            this.Build              = this.Reader.ReadInt32();

            this.MasterHash         = this.Reader.ReadString();

            this.Reader.ReadString();
            this.Reader.ReadString(); // 6C8C8125-2BFC-4BAD-8DAB-7689A6FF346B
            this.Device.MACAddress = this.Reader.ReadString();
            this.Device.Model = this.Reader.ReadString();
            this.Reader.ReadString();

            if (!this.Reader.EndOfStream)
            {
                this.PreferredLanguage = this.Reader.ReadString();

                if (!this.Reader.EndOfStream)
                {
                    this.Reader.ReadString(); // 6C8C8125-2BFC-4BAD-8DAB-7689A6FF346B

                    if (!this.Reader.EndOfStream)
                    {
                        this.Device.OSVersion = this.Reader.ReadString();
                        this.Device.Android = this.Reader.ReadBoolean();
                        this.Reader.ReadInt32(); // 0

                        this.Reader.ReadByte(); // 0
                        this.Reader.ReadByte(); // 0
                        this.Reader.ReadByte(); // 0
                        this.Reader.ReadByte(); // 0

                        this.Reader.ReadString(); // Empty

                        this.Reader.ReadBoolean(); // true

                        this.Reader.ReadString(); // 79A51533-D88E-4F46-8292-97C49E8CE4A7

                        this.Seed = this.Reader.ReadInt32();
                    }
                }
            }
        }

        /// <summary>
        /// Processes this message.
        /// </summary>
        internal override async void Process()
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
                    this.Device.Player          = await Resources.Players.Get(this.Device, this.HighID, this.LowID);

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
                            new Authentification_Failed(this.Device, Reason.Banned).Send();
                        }
                    }
                    else
                    {
                        new Authentification_Failed(this.Device, Reason.Locked).Send();
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
            //if (this.ValuesAreCorrect())
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

                if (this.Major == (int)CVersion.Major && this.Build == (int)CVersion.Build)
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

            if (string.IsNullOrEmpty(this.MasterHash))
            {
                return false;
            }

            if (this.MasterHash.Length != 40)
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
            // this.Device.Player.Region   = this.PreferredLanguage.ToUpper();

            // new Encryption(this.Device, Seed).Send();
            new Authentification_OK(this.Device).Send();
            new Own_Avatar_Data(this.Device).Send();

            return;

            if (this.Device.Player.ClanLowID > 0)
            {
                Clan Clan = this.Device.Player.Clan;

                if (Clan != null)
                {
                    //new Clan_Info(this.Device, Clan).Send();
                    //new Clan_Stream(this.Device, Clan).Send();
                }
                else
                {
                    Logging.Error(this.GetType(), "Clan not found when Process() has been called.");
                }
            }
            else
            {
                // new Clan_Info(this.Device, null).Send();
            }
        }
    }
}
