namespace GL.Servers.GS.Logic
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Reflection;
    using System.Text;

    using GL.Servers.Core;
    using GL.Servers.Extensions;
    using GL.Servers.Extensions.Binary;
    using GL.Servers.GS.Core;
    using GL.Servers.GS.Core.Network;
    using GL.Servers.GS.Packets;
    using GL.Servers.Library;
    using GL.Servers.Logic.Enums;

    internal class Device
    {
        internal Socket Socket;
        internal Player Player;
        internal Token Token;
        internal IPEndPoint UDPSocket;

        internal Rjindael Crypto;
        internal XorShift Random;

        internal bool Android;
        internal bool Advertising;

        internal int Ping;

        internal string Interface;
        internal string AndroidID;
        internal string OpenUDID;
        internal string Model;
        internal string OSVersion;
        internal string MACAddress;
        internal string AdvertiseID;
        internal string VendorID;

        internal State State = State.DISCONNECTED;

        /// <summary>
        /// Initializes a new instance of the <see cref="Device"/> class.
        /// </summary>
        /// <param name="Socket">The socket.</param>
        internal Device(Socket Socket)
        {
            this.Socket = Socket;

            this.Crypto = new Rjindael("88778f76fwe67r5f78wer678r9we7" + "nonce");
            this.Random = new XorShift(Resources.Random.Next());
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

            this.Crypto = new Rjindael("88778f76fwe67r5f78wer678r9we7" + "nonce");
            this.Random = new XorShift(Resources.Random.Next());
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
        /// Processes the specified buffer.
        /// </summary>
        /// <param name="Buffer">The buffer.</param>
        internal void Process(byte[] Buffer)
        {
            if (Buffer.Length >= 5 && Buffer.Length <= Constants.ReceiveBuffer)
            {
                if (Buffer.SequenceEqual(Constants.PolicyFileRequest))
                {
                    Resources.Gateway.Send(this, Encoding.UTF8.GetBytes("<?xml version=\"/1.0\"?><cross-domain-policy><allow-access-from domain=\"*\" to-ports=\"*\" /></cross-domain-policy>"));
                    Resources.Gateway.Disconnect(this.Token.Args);

                    return;
                }

                using (Reader Reader = new Reader(Buffer))
                {
                    ushort Identifier   = Reader.ReadUInt16();
                    uint Length         = Reader.ReadUInt24();

                    if (Buffer.Length - 5 >= Length)
                    {
                        if (Factory.Messages.ContainsKey(Identifier))
                        {
                            Message Message     = Activator.CreateInstance(Factory.Messages[Identifier], this, Reader) as Message;

                            Message.Identifier  = Identifier;
                            Message.Length      = Length;

                            Message.Reader      = Reader;

                            Logging.Error(this.GetType(), "Packet " + ConsolePad.Padding(Message.GetType().Name) + " received from " + this.Socket.RemoteEndPoint + ".");

                            try
                            {
                                // Message.Decrypt();
                                Message.Decode();
                                Message.Process();
                            }
                            catch (Exception Exception)
                            {
                                Logging.Error(this.GetType(), Exception.GetType().Name + " when handling the following message : ID " + Identifier + ", Length " + Length + ".");
                                Logging.Error(Exception.GetType(), Exception.Message + " [" + (this.Player != null ? this.Player.HighID + ":" + this.Player.LowID : "---") + ']' + Environment.NewLine + Exception.StackTrace);
                            }
                        }
                        else
                        {
                            Logging.Error(this.GetType(), "Can't handle the following message : ID " + Identifier + ", Length " + Length + ".");

                            byte[] AltBuffer = Reader.ReadBytes((int)Length);
                            this.Crypto.Decrypt(ref AltBuffer);
                            AltBuffer = null;
                        }

                        if (!this.Token.Aborting)
                        {
                            this.Token.Packet.RemoveRange(0, (int)(Length + 5));

                            if (Buffer.Length - 5 - Length >= 5)
                            {
                                this.Process(Reader.ReadBytes((int)(Buffer.Length - 5 - Length)));
                            }
                        }
                    }
                    else
                    {
                        Logging.Error(this.GetType(), "The received buffer length is inferior to the header length.");
                        Resources.Gateway.Disconnect(this.Token.Args);
                    }
                }
            }
            else
            {
                Resources.Gateway.Disconnect(this.Token.Args);
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
                Resources.Gateway.Disconnect(this.Token.Args);
            }

            this.Crypto = null;
            this.Random = null;
            this.Socket = null;

            GC.SuppressFinalize(this);
        }
    }
}