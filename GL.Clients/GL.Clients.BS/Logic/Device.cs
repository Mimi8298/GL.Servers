namespace GL.Clients.BS.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Net.Sockets;
    using System.Threading;

    using GL.Clients.BS.Core;
    using GL.Clients.BS.Core.Network;
    using GL.Clients.BS.Packets;
    using GL.Clients.BS.Packets.Messages.Client;

    using GL.Servers.Extensions.Binary;
    using GL.Servers.Library;
    using GL.Servers.Logic.Enums;

    using Timer = System.Timers.Timer;

    internal class Device
    {
        internal int BotId;

        internal Socket Socket;
        internal Timer Timer;
        internal Gateway Gateway;
        internal Rjindael Crypto;
        internal Token Token;
        internal Player Player;

        internal List<Player> ReceivedPlayers;

        internal State State = State.DISCONNECTED;

        /// <summary>
        /// Initializes a new instance of the <see cref="Device"/> class.
        /// </summary>
        internal Device()
        {
            this.Socket             = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.Crypto             = new Rjindael();
            this.Token              = new Token(new SocketAsyncEventArgs(), this);
            this.Player             = new Player();
            this.Gateway            = new Gateway(this);
            this.ReceivedPlayers    = new List<Player>();

            this.KeepAlive();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Device"/> class.
        /// </summary>
        /// <param name="Socket">The socket.</param>
        internal Device(Socket Socket) : this()
        {
            this.Socket     = Socket;
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
                        if (Factory.Messages.ContainsKey(Identifier))
                        {
                            Message Message     = Activator.CreateInstance(Factory.Messages[Identifier], this, Reader) as Message;

                            // Logging.Info(this.GetType(), "[" + this.BotId + "] We  received    the following message : " + Message.GetType().Name + ", Version " + Version + ".");

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
                                Logging.Error(this.GetType(), Exception.GetType().Name + " when processing " + Message.GetType().Name + ".");
                            }
                        }
                        else
                        {
                            // Logging.Info(this.GetType(), "Warning, we can't handle the following message : ID " + Identifier + ", Length " + Length + ", Version " + Version + ".");

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
                        // Logging.Info(this.GetType(), "Warning, we don't have enough data to process.");
                    }
                }
            }
            else
            {
                Logging.Info(this.GetType(), "Warning, we don't have enough data to read the header.");
            }
        }

        /// <summary>
        /// Connects this instance to the official server.
        /// </summary>
        internal void Connect()
        {
            this.Socket.Connect("game.brawlstarsgame.com", 9339);

            if (this.Socket.Connected)
            {
                this.Gateway.Send(new Authentification(this));
                this.Gateway.Receive();

                while (this.State != State.LOGGED)
                {
                    Thread.Sleep(100);
                }

                Logging.Info(this.GetType(), "[" + this.BotId + "] Success, the bot is logged.");
            }
            else
            {
                Logging.Info(this.GetType(), "[" + this.BotId + "] Warning, we are not connected to the game server.");
            }
        }

        /// <summary>
        /// Lets cheat and win gold + experience.
        /// </summary>
        internal void LetsCheat()
        {
            while (true)
            {
                int Tries = 0;

                while (this.State != State.LOGGED)
                {
                    Thread.Sleep(100);

                    if (Tries++ == 5)
                    {
                        break;
                    }
                }

                this.State = State.IN_BATTLE;

                this.Gateway.Send(new Execute_Commands(this));
                this.Gateway.Send(new Ask_Battle_Result(this));

                Thread.Sleep(TimeSpan.FromSeconds(15));
            }
        }

        /// <summary>
        /// Keeps the alive.
        /// </summary>
        internal void KeepAlive()
        {
            bool SendCommands = false;

            this.Timer = new Timer();
            this.Timer.AutoReset = true;
            this.Timer.Interval = TimeSpan.FromSeconds(5).TotalMilliseconds;
            this.Timer.Elapsed += (Gobelin, Land) =>
            {
                if (this.Connected)
                {
                    if (this.State >= State.LOGIN)
                    {
                        this.Gateway.Send(new Keep_Alive(this));
                        // this.Gateway.Send(new Client_Capabilities(this));

                        if (SendCommands && this.State != State.IN_BATTLE && this.State > State.LOGIN)
                        {
                            // this.Gateway.Send(new Execute_Commands(this));
                        }
                        else
                        {
                            SendCommands = !SendCommands;
                        }
                    }
                }
            };
            this.Timer.Start();
        }
    }
}