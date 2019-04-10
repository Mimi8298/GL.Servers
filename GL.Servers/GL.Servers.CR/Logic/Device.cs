namespace GL.Servers.CR.Logic
{
    using System;
    using System.Net.Sockets;

    using GL.Servers.CR.Core;
    using GL.Servers.CR.Core.Network;
    using GL.Servers.CR.Logic.Mode;
    using GL.Servers.CR.Logic.Structures;
    using GL.Servers.CR.Packets.Messages.Manager;

    using GL.Servers.Logic.Enums;

    internal class Device
    {
        internal Token Token;
        internal Socket Socket;
        internal Defines Defines;
        internal GameMode GameMode;

        internal MessageManager MessageManager;

        internal State State = State.DISCONNECTED;

        /// <summary>
        /// Prevents a default instance of the <see cref="Device"/> class from being created.
        /// </summary>
        private Device()
        {
            this.MessageManager = new MessageManager(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Device"/> class.
        /// </summary>
        /// <param name="Socket">The socket.</param>
        internal Device(Socket Socket) : this()
        {
            this.Socket = Socket;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Device"/> class.
        /// </summary>
        /// <param name="Socket">The socket.</param>
        /// <param name="Token">The token.</param>
        internal Device(Socket Socket, Token Token) : this()
        {
            this.Socket = Socket;
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
                if (this.Token.Aborting)
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
        internal void TcpProcess(byte[] Buffer)
        {
            if (this.Connected)
            {
                Process:

                if (Buffer.Length >= 7 && Buffer.Length <= Constants.ReceiveBuffer)
                {
                    short Type    = (short) (Buffer[1] | Buffer[0] << 8);
                    int Length    = Buffer[4] | Buffer[3] << 8 | Buffer[2];
                    short Version = (short) (Buffer[6] | Buffer[5] << 8);

                    if (Length <= 0x7FFFFF)
                    {
                        if (Buffer.Length - 7 >= Length)
                        {
                            byte[] Packet = new byte[Length];
                            Array.Copy(Buffer, 7, Packet, 0, Length);
                            this.MessageManager.ReceiveMessage(Type, Version, Packet);

                            this.Token.Packet.RemoveRange(0, Length + 7);

                            if (Buffer.Length - 7 - Length >= 7)
                            {
                                byte[] Next = new byte[Buffer.Length - 7 - Length];
                                Array.Copy(Buffer, Length + 7, Next, 0, Next.Length);
                                this.TcpProcess(Next);
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
                    Resources.TCPGateway.Disconnect(this.Token.Args);
                }
            }
            else
            {
                Resources.TCPGateway.Disconnect(this.Token.Args);
            }
        }
    }
}