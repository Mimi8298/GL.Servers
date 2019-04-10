namespace GL.Servers.SP.Logic
{
    using System;
    using System.Reflection;
    using System.Net.Sockets;

    using GL.Servers.SP.Core;
    using GL.Servers.SP.Packets;
    using GL.Servers.SP.Core.Network;
    using GL.Servers.SP.Logic.Mode;
    using GL.Servers.SP.Logic.Slots.Items;
    
    using GL.Servers.Extensions;
    using GL.Servers.Logic.Enums;
    using GL.Servers.Extensions.Binary;
    using GL.Servers.Library;

    internal class Device : IDisposable
    {
        internal Socket Socket;
        internal Token Token;
        internal GameMode GameMode;
        internal Chat Chat;

        internal Rjindael RC4;
        
        internal bool Android;
        internal bool Disposed;
        internal bool Advertising;

        internal string UDID;
        internal string OpenUDID;
        internal string MacAddress;
        internal string DeviceModel;
        internal string ADID;
        internal string OSVersion;
        internal string PreferredLanguageId;

        internal State State = State.DISCONNECTED;

        /// <summary>
        /// Initializes a new instance of the <see cref="Device"/> class.
        /// </summary>
        /// <param name="Socket">The socket.</param>
        internal Device(Socket Socket)
        {
            this.Socket = Socket;
            this.RC4 = new Rjindael("fhsd6f86f67rt8fw78fw789we78r9789wer6re" + "nonce");
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
                return this.Android ? "Android" : "iOS";
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
                            byte[] Packet = Reader.ReadBytes(Length);

                            this.RC4.Decrypt(ref Packet);

                            Message Message = Factory.CreateMessage(Identifier, this, null);

                            if (Message != null)
                            {
                                Message.Length     = Length;
                                Message.Version    = Version;

                                Message.Reader = new Reader(Packet);

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

        /// <summary>
        /// Disposes this instance.
        /// </summary>
        public void Dispose()
        {
            if (!this.Disposed)
            {
                this.Disposed = true;
                this.State = State.DISCONNECTED;                
                this.Token = null;
                this.GameMode = null;
                
                this.Socket.Dispose();
            }
        }
    }
}