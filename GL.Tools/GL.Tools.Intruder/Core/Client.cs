namespace GL.Tools.Intruder.Core
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;

    using GL.Servers.Library;
    using GL.Tools.Intruder.Extensions.Binary;
    using GL.Tools.Intruder.Messages;
    using GL.Tools.Intruder.Messages.Client;

    internal class Client
    {
        internal Socket Socket;
        internal IPEndPoint EndPoint;
        internal Rjindael RC4;

        internal byte[] Buffer;
        internal bool Found;
        internal int[] Version;
        internal string ServerIP = "stage.brawlstarsgame.com";

        internal bool UseRC4 = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        internal Client()
        {
            this.Socket     = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.EndPoint   = new IPEndPoint(IPAddress.Any, 0);
            this.RC4        = new Rjindael();
            this.Buffer     = new byte[512];

            this.Version    = new int[3];
            this.Version[0] = 2;
            this.Version[1] = 0;
            this.Version[2] = 57;

            this.Socket.Bind(this.EndPoint);
            this.Socket.Connect(this.ServerIP, 9339);

            while (!this.Found)
            {
                this.Send();
                this.Receive();
                this.Reconnect();
            }

            this.Socket.Close(5);
        }

        /// <summary>
        /// Sends the message.
        /// </summary>
        internal void Send()
        {
            if (this.UseRC4)
            {
                Authentification Auth   = new Authentification();
                Auth.ClientVersion      = this.Version;
                Auth.Encode();

                byte[] Encrypted        = Auth.Writer.ToArray();

                this.RC4.Encrypt(ref Encrypted);

                Auth.Writer.Clear();
                Auth.Writer.AddRange(Encrypted);

                Auth.Header.Length = (uint) Encrypted.Length;

                int Sent = this.Socket.Send(Auth.ToBytes);

                Debug.WriteLine("-> We sent " + Auth.Header.Identifier + ". [" + this.Version[0] + "." + this.Version[1] + "." + this.Version[2] + "]");
            }
            else
            {
                Pre_Authentification _PreAuth = new Pre_Authentification();
                _PreAuth.Version = this.Version;
                _PreAuth.Encode();

                int Sent = this.Socket.Send(_PreAuth.ToBytes);

                Debug.WriteLine("-> We sent " + _PreAuth.Header.Identifier + ". [" + this.Version[0] + "." + this.Version[1] + "." + this.Version[2] + "]");
            }
        }

        /// <summary>
        /// Receives the message.
        /// </summary>
        internal void Receive()
        {
            int Received    = this.Socket.Receive(this.Buffer, 0);
            Header Header   = new Header();
            Header.ToBytes  = this.Buffer.Take(7).ToArray();

            Debug.WriteLine("-> We received a " + Header.Identifier + ".");

            if (Header.Identifier == 20100 || Header.Identifier == 20104)
            {
                this.Found = true;

                Console.WriteLine("-> We got the Auth_OK message. [" + this.Version[0] + "." + this.Version[1] + "." + this.Version[2] + "]");
            }
            else if (Header.Identifier == 20103)
            {
                if (this.UseRC4)
                {
                    byte[] Encrypted = this.Buffer.Skip(7).ToArray();

                    this.RC4.Decrypt(ref Encrypted);

                    byte[] Decrypted = this.Buffer.Take(7).ToArray().Concat(Encrypted).ToArray();

                    this.Buffer = Decrypted;
                }

                using (Reader Reader = new Reader(this.Buffer.Skip(7).ToArray()))
                {
                    int Code = Reader.ReadInt32();

                    switch (Code)
                    {
                        case 7:
                        {
                            Console.WriteLine("-> We got the patch. [" + this.Version[0] + "." + this.Version[1] + "." + this.Version[2] + "]");
                            this.Found = true;
                            break;
                        }

                        case 8:
                        {
                            Console.WriteLine("-> Our client version is outdated. [" + this.Version[0] + "." + this.Version[1] + "." + this.Version[2] + "]");
                            break;
                        }

                        case 10:
                        {
                            Console.WriteLine("-> Server is in maintenance mode. [" + this.Version[0] + "." + this.Version[1] + "." + this.Version[2] + "]");
                            break;
                        }

                        default:
                        {
                            Debug.WriteLine("Authentification failed, code " + Code + ".");
                            break;
                        }
                    }
                }
            }

            this.Increment();
        }

        /// <summary>
        /// Increments the version.
        /// </summary>
        internal void Increment()
        {
            if (this.Version[2] >= 3000)
            {
                this.Version[2] = 2000;
                this.Version[1] += 1;

                if (this.Version[1] >= 2)
                {
                    this.Version[0] += 1;
                }
            }
            else
            {
                this.Version[2] += 1;
            }
        }

        /// <summary>
        /// Reconnects this instance.
        /// </summary>
        internal void Reconnect()
        {
            if (this.UseRC4)
            {
                this.RC4 = new Rjindael();
            }

            this.Buffer = new byte[512];
            this.Socket.Dispose();
            this.Socket = null;
            this.Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.Socket.Bind(this.EndPoint);
            this.Socket.Connect(this.ServerIP,  9339);
        }
    }
}