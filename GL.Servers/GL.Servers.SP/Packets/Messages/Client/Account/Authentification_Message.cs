namespace GL.Servers.SP.Packets.Messages.Client.Account
{
    using System;
    using GL.Servers.Extensions.Binary;
    using GL.Servers.Logic.Enums;
    using GL.Servers.SP.Core;
    using GL.Servers.SP.Core.Network;
    using GL.Servers.SP.Extensions.Game;
    using GL.Servers.SP.Extensions.Helper;
    using GL.Servers.SP.Logic;
    using GL.Servers.SP.Packets.Enums;
    using GL.Servers.SP.Packets.Messages.Server.Account;
    using GL.Servers.SP.Packets.Messages.Server.Home;

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

        private int RandomKey;
        
        internal override ServiceNode Node
        {
            get
            {
                return ServiceNode.Base;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Authentification_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Authentification_Message(Device Device, Reader Reader) : base(Device, Reader)
        {

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

            this.Device.UDID = this.Reader.ReadString();
            this.Device.OpenUDID = this.Reader.ReadString();
            this.Device.MacAddress = this.Reader.ReadString();
            this.Device.DeviceModel = this.Reader.ReadString();
            
            if (!this.Reader.EndOfStream)
            {
                this.Reader.ReadData();
                this.Device.PreferredLanguageId = this.Reader.ReadString();

                if (!this.Reader.EndOfStream)
                {
                    this.Device.ADID = this.Reader.ReadString();

                    if (!this.Reader.EndOfStream)
                    {
                        this.Device.OSVersion = this.Reader.ReadString();

                        if (!this.Reader.EndOfStream)
                        {
                            this.Device.Android = this.Reader.ReadBoolean();

                            if (!this.Reader.EndOfStream)
                            {
                                this.Reader.ReadString(); // IMEI
                                this.Reader.ReadString(); // AndroidID

                                if (!this.Reader.EndOfStream)
                                {
                                    this.Reader.ReadString(); // FacebookAttributionID

                                    if (!this.Reader.EndOfStream)
                                    {
                                        this.Device.Advertising = this.Reader.ReadBoolean();
                                        this.Reader.ReadString(); // AppleIFV

                                        if (!this.Reader.EndOfStream)
                                        {
                                            this.RandomKey = this.Reader.ReadInt32();
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
        internal override async void Process()
        {
            if (this.Trusted())
            {
                if (this.LowID == 0)
                {
                    Player Player = Resources.Players.New(this.Device.GameMode);

                    if (Player != null)
                    {
                        this.Login(Player);
                    }
                    else
                    {
                        new Authentification_Failed_Message(this.Device, Reason.Pause).Send();
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
                                this.Login(Player);
                            }
                            else
                            {
                                new Authentification_Failed_Message(this.Device, Reason.Banned).Send();
                            }
                        }
                        else
                        {
                            new Authentification_Failed_Message(this.Device, Reason.Reset).Send();
                        }
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
                if (this.MajorVersion == Logic.Version.ClientMajorVersion && this.MinorVersion == Logic.Version.ClientMinorVersion && this.BuildVersion == Logic.Version.ClientBuildVersion)
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
            return this.HighID >= 0 && this.LowID >= 0;
        }

        /// <summary>
        /// Logins this instance.
        /// </summary>
        internal void Login(Player Player)
        {
            this.Device.GameMode.SetPlayer(Player);
            this.Device.GameMode.Player.LoginCountry = this.Device.PreferredLanguageId;
            
            new Authentification_Ok_Message(this.Device).Send();
            new Own_Home_Data_Message(this.Device).Send();
        }
    }
}
