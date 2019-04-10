namespace GL.Servers.BB.Logic
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Reflection;

    using GL.Servers.BB.Core;
    using GL.Servers.BB.Core.Network;
    using GL.Servers.BB.Logic.Enums;
    using GL.Servers.BB.Packets;
    using GL.Servers.BB.Packets.Cryptography;

    using GL.Servers.Core;
    using GL.Servers.Extensions;
    using GL.Servers.Extensions.Binary;

    internal class Device
    {
        internal Socket Socket;
        internal GameMode GameMode;
        internal Token Token;
        internal IPEndPoint UDPSocket;

        internal Sodium Crypto;

        internal bool Android;
        internal bool AdvertiserTrackingEnabled;

        internal string ADID;
        internal string IMEI;
        internal string UDID;
        internal string OpenUDID;
        internal string AndroidID;
        internal string OSVersion;
        internal string DeviceType;
        internal string IdentifierForVendor;
        internal string FacebookAttributionID;

        internal string KunlunSSO;
        internal string KunlunUID;

        internal string PreferredDeviceLanguage;

        internal DeviceState State = DeviceState.Disconnected;

        /// <summary>
        /// Initializes a new instance of the <see cref="Device"/> class.
        /// </summary>
        /// <param name="_Socket">The socket.</param>
        internal Device(Socket Socket)
        {
            this.Socket = Socket;

            this.Crypto   = new Sodium();
            this.GameMode = new GameMode(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Device"/> class.
        /// </summary>
        /// <param name="Socket">The socket.</param>
        /// <param name="Token">The token.</param>
        internal Device(Socket Socket, Token Token)
        {
            this.Socket = Socket;
            this.Token  = Token;

            this.Crypto   = new Sodium();
            this.GameMode = new GameMode(this);
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Device"/> is connected.
        /// </summary>
        /// <value>
        ///   True if connected, false if disconnected.
        /// </value>
        internal bool Connected
        {
            get
            {
                if (this.Socket.Connected)
                {
                    try
                    {
                        if (!this.Socket.Poll(1000, SelectMode.SelectRead) || this.Socket.Available != 0)
                        {
                            return true;
                        }
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Gets the operating system of this instance.
        /// </summary>
        internal string OS
        {
            get
            {
                return this.Android ? "Android" : "iOS";
            }
        }

        /// <summary>
        /// Processes the specified buffer.
        /// </summary>
        /// <param name="Buffer">The buffer.</param>
        internal void Process(byte[] Buffer)
        {
            if (this.State != DeviceState.Disconnected)
            {
                if (Buffer.Length >= 7 && Buffer.Length <= Constants.ReceiveBuffer)
                {
                    using (Reader Reader = new Reader(Buffer))
                    {
                        short Type = Reader.ReadInt16();
                        int Length = Reader.ReadInt24();
                        short Version = Reader.ReadInt16();

                        if (Buffer.Length - 7 >= Length)
                        {
                            byte[] Packet = this.Crypto.Decrypt(Type, Reader.ReadBytes(Length));

                            if (Packet != null)
                            {
                                Message Message = Packets.Factory.CreateMessage(Type, this, null);

                                if (Message != null)
                                {
                                    Message.Length  = Length;
                                    Message.Version = Version;

                                    Message.Reader  = new Reader(Packet);

                                    Logging.Info(this.GetType(), "Packet " + ConsolePad.Padding(Message.GetType().Name) + " received from " + this.Socket.RemoteEndPoint + ".");

                                    try
                                    {
                                        Message.Decode();
                                        Message.Process();
                                    }
                                    catch (Exception Exception)
                                    {
                                        Logging.Error(this.GetType(), Exception.GetType().Name + " when handling the following message : Type " + Type + ", Length " + Length + ", Version " + Version + ".");
                                        // Logging.Error(Exception.GetType(), Exception.Message + " [" + (this.Level.Player != null ? this.Level.Player.HighId + ":" + this.Level.Player.LowId : "---") + ']' + Environment.NewLine + Exception.StackTrace);
                                    }

                                    Message.Reader.Dispose();
                                }

                                if (!this.Token.Aborting)
                                {
                                    this.Token.Packet.RemoveRange(0, Length + 7);

                                    if (Buffer.Length - 7 - Length >= 7)
                                    {
                                        this.Process(Reader.ReadBytes(Buffer.Length - 7 - Length));
                                    }
                                }
                            }
                        }
                        else
                        {
                            Logging.Error(this.GetType(), "The received buffer length is inferior of the header length.");
                        }
                    }
                }
                else
                {
                    Resources.TCPGateway.Disconnect(this.Token.Args);
                }
            }
            else
            {
                if (this.Connected)
                {
                    Resources.TCPGateway.Disconnect(this.Token.Args);
                }
            }
        }

        internal void ShowValues()
        {
            foreach (FieldInfo Field in this.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (Field != null)
                {
                    Logging.Error(this.GetType(), ConsolePad.Padding(Field.Name) + " : " + ConsolePad.Padding(!string.IsNullOrEmpty(Field.Name) ? (Field.GetValue(this) != null ? Field.GetValue(this).ToString() : "(null)") : "(null)", 40));
                }
            }
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="Device"/> class.
        /// </summary>
        ~Device()
        {
            if (!this.Token.Aborting)
            {
                Resources.TCPGateway.Disconnect(this.Token.Args);
            }

            this.Crypto = null;
            this.Socket = null;

            GC.SuppressFinalize(this);
        }
    }
}