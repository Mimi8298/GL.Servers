namespace GL.Servers.HD.Logic
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Reflection;

    using GL.Servers.HD.Core;
    using GL.Servers.HD.Core.Network;
    using GL.Servers.HD.Packets;

    using GL.Servers.Core;
    using GL.Servers.Extensions;
    using GL.Servers.Extensions.Binary;
    using GL.Servers.HD.Packets.Cryptography;
    using GL.Servers.Library;
    using GL.Servers.Logic.Enums;

    internal class Device
    {
        internal Socket Socket;
        internal Player Player;
        internal Token Token;
        internal IPEndPoint UDPSocket;
        
        internal XorShift Random;
        internal Sodium Crypto;

        internal bool Android;
        internal bool Advertising;

        internal int Ping;

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
            this.Crypto = new Sodium();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Device"/> class.
        /// </summary>
        /// <param name="Socket">The socket.</param>
        /// <param name="Token">The token.</param>
        internal Device(Socket Socket, Token Token) : this(Socket)
        {
            this.Token  = Token;
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
            if (Buffer.Length >= 7 && Buffer.Length <= Constants.ReceiveBuffer)
            {
                using (Reader Reader = new Reader(Buffer))
                {
                    short Identifier   = Reader.ReadInt16();
                    int Length         = (int) Reader.ReadUInt24();
                    short Version      = Reader.ReadInt16();

                    if (Buffer.Length - 7 >= Length)
                    {
                        byte[] Packet = Reader.ReadBytes(Length);
                        
                        Message Message = Factory.CreateMessage(Identifier, this, null);
                        Packet = this.Crypto.Decrypt(Identifier, Packet);

                        if (Packet != null)
                        {
                            if (Message != null)
                            {
                                Message.Length = Length;
                                Message.Version = Version;

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
                                    Logging.Error(Exception.GetType(), Exception.Message + " [" + (this.Player != null ? this.Player.HighID + ":" + this.Player.LowID : "---") + ']' + Environment.NewLine + Exception.StackTrace);
                                }
                            }
                        }
                        else
                            Logging.Error(this.GetType(), "Crypto Error");

                        if (!this.Token.Aborting)
                        {
                            this.Token.Packet.RemoveRange(0, (int)(Length + 7));

                            if (Buffer.Length - 7 - Length >= 7)
                            {
                                this.Process(Reader.ReadBytes((int)(Buffer.Length - 7 - Length)));
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

        internal void ShowValues()
        {
            foreach (FieldInfo Field in this.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (Field != null)
                {
                    Logging.Info(this.GetType(), ConsolePad.Padding(Field.Name) + " : " + ConsolePad.Padding(!string.IsNullOrEmpty(Field.Name) ? (Field.GetValue(this) != null ? Field.GetValue(this).ToString() : "(null)") : "(null)", 40));
                }
            }
        }
    }
}