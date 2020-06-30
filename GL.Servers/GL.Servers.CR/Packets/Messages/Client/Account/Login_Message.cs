namespace GL.Servers.CR.Packets.Messages.Client.Account
{
    using System;
    using GL.Servers.CR.Core;
    using GL.Servers.CR.Core.Network;
    using GL.Servers.CR.Logic;
    using GL.Servers.CR.Logic.Enums;
    using GL.Servers.CR.Logic.Mode;
    using GL.Servers.CR.Packets.Messages.Server.Account;
    using GL.Servers.CR.Packets.Messages.Server.Home;
    using GL.Servers.DataStream;
    using GL.Servers.Extensions.Binary;
    using GL.Servers.Files;
    using GL.Servers.Logic.Enums;

    internal class Login_Message : Message
    {
        private int HighID;
        private int LowID;

        private string Token;
        private string MasterHash;
        private string SVersion;

        private int MajorVersion;
        private int MinorVersion;
        private int BuildVersion;

        /// <summary>
        /// Gets the type of this message.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 10101;
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
        /// Initializes a new instance of the <see cref="Login_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Login_Message(Device Device, ByteStream ByteStream) : base(Device, ByteStream)
        {
            this.Device.State       = State.LOGIN;
        }

        /// <summary>
        /// Decodes the <see cref="Message" />, using the <see cref="Reader" /> instance.
        /// </summary>
        internal override void Decode()
        {
            this.HighID             = this.Data.ReadInt();
            this.LowID              = this.Data.ReadInt();
            this.Token              = this.Data.ReadString();

            this.MajorVersion       = this.Data.ReadVInt();
            this.MinorVersion       = this.Data.ReadVInt();
            this.BuildVersion       = this.Data.ReadVInt();

            this.MasterHash         = this.Data.ReadString();

            this.Data.ReadInt();

            this.Device.Defines.OpenUDID    = this.Data.ReadString();
            this.Device.Defines.MACAddress  = this.Data.ReadString();
            this.Device.Defines.Model       = this.Data.ReadString();

            this.Device.Defines.AdvertiseID = this.Data.ReadString();
            this.Device.Defines.OSVersion   = this.Data.ReadString();

            this.Device.Defines.Android     = this.Data.ReadBoolean();

            this.Data.ReadInt();

            this.Device.Defines.AndroidID   = this.Data.ReadString();
            this.Device.Defines.Region      = this.Data.ReadString();
        }

        /// <summary>
        /// Returns whether we should handle this device or nah.
        /// </summary>
     /*   internal bool Trusted()
        {
            if (this.ValuesAreCorrect())
            {
                if (this.MajorVersion == Logic.Version.ClientMajorVersion && this.MinorVersion == 0 && this.BuildVersion == Logic.Version.ClientBuildVersion)
                {
                    if (Constants.Maintenance <= DateTime.UtcNow)
                    {
                        if (Resources.Started)
                        {
                            if (string.Equals(this.MasterHash, Fingerprint.Sha))
                            {
                                return true;
                            }
                            else
                            {
                                new Authentification_Failed_Message(this.Device, Reason.Patch).Send();
                            }
                        }
                        else
                        {
                            new Authentification_Failed_Message(this.Device, Reason.Maintenance).Send();
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
            else
            {
                Resources.TCPGateway.Disconnect(this.Device.Token.Args);
            }

            return false;
        }

        /// <summary>
        /// Processes this message.
        /// </summary>*/
        internal override void Process()
        {
        
         new Authentification_Failed_Message(this.Device, Reason.Patch).Send();
          /*  if (!this.Trusted())
            {
                return;
            }

            if (this.HighID == 0 && this.LowID == 0 && this.Token == null)
            {
                Player Player = Resources.Players.Create();
                
                if (Player != null)
                {
                    this.Login(Player);
                }
                else
                {
                    new Authentification_Failed_Message(this.Device, Reason.Maintenance).Send();
                }
            }
            else
            {
                Player Player = Resources.Players.Get(this.HighID, this.LowID);

                if (Player != null)
                {
                    if (string.Equals(this.Token, Player.PassToken))
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
        }

        /// <summary>
        /// Checks if values are correct.
        /// </summary>
        /// <returns>A bool indicating whether the values are correct or no.</returns>
        internal bool ValuesAreCorrect()
        {
            if (this.HighID >= 0)
            {
                if (this.LowID >= 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Logins this instance.
        /// </summary>
        internal void Login(Player Player)
        {
            this.Device.GameMode = new GameMode(this.Device);
            this.Device.MessageManager.AccountHighId = Player.HighID;
            this.Device.MessageManager.AccountLowId = Player.LowID;

            new Authentification_Ok_Message(this.Device, Player.PassToken).Send();
            new Own_Home_Data_Message(this.Device, Player).Send();*/
        }
    }
}
