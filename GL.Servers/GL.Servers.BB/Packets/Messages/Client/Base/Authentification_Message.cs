namespace GL.Servers.BB.Packets.Messages.Client.Base
{
    using System;
    using System.Linq;

    using GL.Servers.BB.Core;
    using GL.Servers.BB.Core.Network;
    using GL.Servers.BB.Extensions;
    using GL.Servers.BB.Extensions.Helper;
    using GL.Servers.BB.Files.CSV_Logic;
    using GL.Servers.BB.Logic;
    using GL.Servers.BB.Logic.Enums;
    using GL.Servers.BB.Packets.Messages.Server.Base;
    using GL.Servers.BB.Packets.Messages.Server.Player;

    using GL.Servers.Extensions.Binary;
    using GL.Servers.Logic.Enums;

    internal class Authentification_Message : Message
    {
        private int HighID;
        private int LowID;

        private string PassToken;
        private string ResourceSha;

        private int MajorVersion;
        private int MinorVersion;
        private int BuildVersion;

        private int AppStore;

        private LocaleData Locale;

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
            this.HighID             = this.Reader.ReadInt32();
            this.LowID              = this.Reader.ReadInt32();
            this.PassToken          = this.Reader.ReadString();

            this.MajorVersion       = this.Reader.ReadInt32();
            this.MinorVersion       = this.Reader.ReadInt32();
            this.BuildVersion       = this.Reader.ReadInt32();

            this.ResourceSha        = this.Reader.ReadString();

            this.Device.UDID        = this.Reader.ReadString();
            this.Device.OpenUDID    = this.Reader.ReadString();
            this.Device.DeviceType  = this.Reader.ReadString();

            this.Locale             = this.Reader.ReadData<LocaleData>();

            this.Device.PreferredDeviceLanguage = this.Reader.ReadString();

            this.Device.ADID        = this.Reader.ReadString();
            this.Device.OSVersion   = this.Reader.ReadString();

            if (!this.Reader.EndOfStream)
            {
                this.Device.Android   = this.Reader.ReadBoolean();

                this.Device.IMEI      = this.Reader.ReadString();
                this.Device.AndroidID = this.Reader.ReadString();

                if (!this.Reader.EndOfStream)
                {
                    this.Device.FacebookAttributionID = this.Reader.ReadString();

                    if (!this.Reader.EndOfStream)
                    {
                        this.Device.IdentifierForVendor = this.Reader.ReadString();

                        if (!this.Reader.EndOfStream)
                        {
                            this.Device.AdvertiserTrackingEnabled = this.Reader.ReadBoolean();

                            if (!this.Reader.EndOfStream)
                            {
                                this.AppStore         = this.Reader.ReadInt32();

                                this.Device.KunlunSSO = this.Reader.ReadString();
                                this.Device.KunlunUID = this.Reader.ReadString();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Processes this message.
        /// </summary>
        internal override async void Process()
        {
            if (this.Device.State == DeviceState.Login)
            {
                if (this.Trusted())
                {
                    if (this.LowID == 0)
                    {
                        Player Player = Resources.Players.New();

                        if (Player != null)
                        {
                            this.Device.GameMode.Level.SetPlayer(Player);
                            this.Login();
                        }
                        else
                        {
                            new Authentification_Failed_Message(this.Device).Send();
                        }
                    }
                    else if (this.LowID > 0)
                    {
                        Player Player = await Resources.Players.GetAsync(this.HighID, this.LowID);

                        if (Player != null)
                        {
                            if (string.Equals(this.PassToken, Player.PassToken))
                            {
                                if (!Player.Banned)
                                {
                                    this.Device.GameMode.Level.SetPlayer(Player);
                                    this.Login();
                                }
                                else
                                {
                                    new Authentification_Failed_Message(this.Device, Authentification_Failed_Message.ErrorReason.Banned).Send();
                                }
                            }
                            else
                            {
                                new Authentification_Failed_Message(this.Device, Authentification_Failed_Message.ErrorReason.Reset).Send();
                            }
                        }
                        else
                        {
                            new Authentification_Failed_Message(this.Device, Authentification_Failed_Message.ErrorReason.Reset).Send();
                        }
                    }
                    else
                    {
                        Resources.TCPGateway.Disconnect(this.Device.Token.Args);
                    }
                }
            }
        }

        internal bool Trusted()
        {
            if (Constants.Local)
            {
                string IPAddress = this.Device.Socket.RemoteEndPoint.ToString();

                if (!(Constants.AuthorizedIP.Contains(IPAddress.Split(':')[0]) || IPAddress.StartsWith("192.168.")))
                {
                    new Authentification_Failed_Message(this.Device, Authentification_Failed_Message.ErrorReason.Maintenance).Send();

                    return false;
                }
            }

            if (this.MajorVersion == GameSettings.MajorVersion && this.MinorVersion == 0 && this.BuildVersion == GameSettings.BuildVersion)
            {
                if (Constants.MaintenanceTime < DateTime.UtcNow)
                {
                    //if (string.Equals(this.ResourceSha, Fingerprint.Sha))
                    {
                        return true;
                    }
                    /*else
                    {
                        new Authentification_Failed_Message(this.Device, Reason.Patch).Send();
                    }*/
                }
                else
                {
                    new Authentification_Failed_Message(this.Device, Authentification_Failed_Message.ErrorReason.Maintenance).Send();
                }
            }
            else
            {
                new Authentification_Failed_Message(this.Device, Authentification_Failed_Message.ErrorReason.Update).Send();
            }

            return false;
        }

        /// <summary>
        /// Logins this instance.
        /// </summary>
        internal void Login()
        {
            new Authentification_Ok_Message(this.Device).Send();
            new Own_Home_Data_Message(this.Device).Send();
        }
    }
}
