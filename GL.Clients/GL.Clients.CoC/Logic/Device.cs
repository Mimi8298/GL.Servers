namespace GL.Clients.CoC.Logic
{
    using System;
    using System.Diagnostics;
    using System.Net.Sockets;

    using GL.Clients.CoC.Core.Network;
    using GL.Clients.CoC.Logic.Enums;
    using GL.Clients.CoC.Packets;
    using GL.Servers.Extensions.Binary;
    using GL.Servers.Library;

    internal class Device
    {
        internal Socket Socket;
        internal Rjindael Crypto;
        internal Token Token;
        internal Client Client;

        internal byte[] SessionKey;

        internal State State = State.DISCONNECTED;

        /// <summary>
        /// Initializes a new instance of the <see cref="Device"/> class.
        /// </summary>
        internal Device()
        {
            this.Crypto     = new Rjindael();
            this.Token      = new Token(new SocketAsyncEventArgs(), this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Device"/> class.
        /// </summary>
        /// <param name="Socket">The socket.</param>
        internal Device(Socket Socket, Client Client) : this()
        {
            this.Socket     = Socket;
            this.Client     = Client;
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
                if (this.State == State.DISCONNECTED)
                {
                    return false;
                }

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
            if (Buffer.Length >= 7)
            {
                using (Reader Reader = new Reader(Buffer))
                {
                    ushort Identifier   = Reader.ReadUInt16();
                    uint Length         = Reader.ReadUInt24();
                    ushort Version      = Reader.ReadUInt16();

                    if (Buffer.Length - 7 >= Length)
                    {
                        if (MessageFactory.Messages.ContainsKey(Identifier))
                        {
                            Message Message     = Activator.CreateInstance(MessageFactory.Messages[Identifier], this, Reader) as Message;

                            Debug.WriteLine("[*] We received the following message : " + Message.GetType().Name + ", Version " + Version + ".");

                            Message.Identifier  = Identifier;
                            Message.Length      = Length;
                            Message.Version     = Version;

                            Message.Reader      = Reader;

                            try
                            {
                                Message.Decrypt();
                                Message.Decode();
                                Message.Process();
                            }
                            catch (Exception Exception)
                            {
                                Debug.WriteLine("[*] " + Exception.GetType().Name + " when processing " + Message.GetType().Name + ".");
                            }
                        }
                        else
                        {
                            Debug.WriteLine("[*] We can't handle the following message : ID " + Identifier + ", Length " + Length + ", Version " + Version + ".");

                            byte[] AltBuffer = Reader.ReadBytes((int) Length);
                            this.Crypto.Decrypt(ref AltBuffer);
                            AltBuffer = null;
                        }

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
                        // Debug.WriteLine("[*] We don't have enough data to process.");
                    }
                }
            }
            else
            {
                Debug.WriteLine("[*] We don't have enough data to read the header.");
            }
        }
    }
}