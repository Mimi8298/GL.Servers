namespace GL.Servers.CoC.Logic
{
    using System;
    using System.Net.Sockets;

    using GL.Servers.CoC.Core;
    using GL.Servers.CoC.Packets;
    using GL.Servers.CoC.Core.Network;
    using GL.Servers.CoC.Logic.Mode;
    using GL.Servers.CoC.Logic.Slots;
    using GL.Servers.CoC.Packets.Cryptography;
    using GL.Servers.CoC.Packets.Cryptography.State;
    using GL.Servers.Extensions;
    using GL.Servers.Logic.Enums;
    using GL.Servers.Extensions.Binary;

    internal class Device : IDisposable
    {
        internal Chat Chat;
        internal Token Token;
        internal Socket Socket;
        internal DeviceInfo Info;
        internal GameMode GameMode;
        internal DateTime LastGlobalChatEntry;

        internal Accounts.Account Account;

        internal IEncrypter ReceiveEncrypter;
        internal IEncrypter SendEncrypter;
        internal PepperState PepperState;

        internal State State = State.DISCONNECTED;

        internal bool Disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="Device"/> class.
        /// </summary>
        /// <param name="Socket">The socket.</param>
        internal Device(Socket Socket)
        {
            this.Socket   = Socket;
            this.GameMode = new GameMode(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Device"/> class.
        /// </summary>
        /// <param name="Socket">The socket.</param>
        /// <param name="Token">The token.</param>
        internal Device(Socket Socket, Token Token) : this(Socket)
        {
            this.Token = Token;
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
                return this.Info.Android ? "Android" : "iOS";
            }
        }

        /// <summary>
        /// Processes the specified buffer.
        /// </summary>
        /// <param name="Buffer">The buffer.</param>
        internal void Process(byte[] Buffer)
        {
            if (this.State != State.DISCONNECTED)
            {
                if (Buffer.Length >= 7 && Buffer.Length <= Constants.ReceiveBuffer)
                {
                    using (Reader Reader = new Reader(Buffer))
                    {
                        short Identifier = Reader.ReadInt16();
                        int Length = Reader.ReadInt24();
                        short Version = Reader.ReadInt16();

                        if (Buffer.Length - 7 >= Length)
                        {
                            if (this.ReceiveEncrypter == null)
                            {
                                this.InitializeEncrypter(Identifier);
                            }

                            byte[] Packet = this.ReceiveEncrypter.Decrypt(Identifier, Reader.ReadBytes(Length));

                            Message Message = Factory.CreateMessage(Identifier, this, null);

                            if (Message != null)
                            {
                                Message.Length     = Length;
                                Message.Version    = Version;

                                Message.Reader     = new Reader(Packet);

                                Logging.Info(this.GetType(), "Packet " + ConsolePad.Padding(Message.GetType().Name) + " received from " + this.Socket.RemoteEndPoint + ".");

                                try
                                {
                                    Message.Decode();
                                    Message.Process();
                                }
                                catch (Exception Exception)
                                {
                                    Logging.Error(this.GetType(), Exception.GetType().Name + " when handling the following message : ID " + Identifier + ", Length " + Length + ", Version " + Version + ".");
                                    // Logging.Error(Exception.GetType(), Exception.Message + " [" + (this.Player != null ? this.Player.HighID + ":" + this.Player.LowID : "---") + ']' + Environment.NewLine + Exception.StackTrace);
                                }
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
                        else
                        {
                            Logging.Error(this.GetType(), "The received buffer length is inferior the header length.");
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

        internal void InitializeEncrypter(int FirstMessageType)
        {
            if (FirstMessageType == 10100)
            {
                this.PepperState = new PepperState();

                this.ReceiveEncrypter = new PepperEncrypter(this.PepperState);
                this.SendEncrypter = new PepperEncrypter(this.PepperState);
            }
            else if (FirstMessageType == 10101)
            {
                this.ReceiveEncrypter = new RC4Encrypter(Factory.RC4Key, "nonce");
                this.SendEncrypter = new RC4Encrypter(Factory.RC4Key, "nonce");
            }
        }
        
        public void Dispose()
        {
            if (!this.Disposed)
            {
                this.Disposed = true;
                this.State = State.DISCONNECTED;
                
                this.Token = null;
                
                this.Socket.Dispose();
            }
        }
    }

    internal struct DeviceInfo
    {
        internal bool Android;
        internal bool Advertising;

        internal string UDID;
        internal string OpenUDID;
        internal string MacAddress;
        internal string DeviceModel;
        internal string ADID;
        internal string OSVersion;
        internal string PreferredLanguageId;
    }
}