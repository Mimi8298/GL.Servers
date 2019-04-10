namespace GL.Clients.BB.Logic
{
    using System;
    using System.Net.Sockets;

    using GL.Clients.BB.Core;
    using GL.Clients.BB.Core.Network;
    using GL.Clients.BB.Packets.Messages.Client;

    using GL.Servers.Logic.Enums;

    using Timer = System.Timers.Timer;

    internal class Client
    {
        internal Socket Socket;
        internal Device Device;
        internal Timer Timer;
        internal Gateway Gateway;

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        internal Client()
        {
            this.Socket     = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.Device     = new Device(this.Socket, this);
            this.Gateway    = new Gateway(this.Device);
            this.KeepAlive();

            this.Socket.Connect("game.boombeachgame.com", 9339);

            if (this.Socket.Connected)
            {
                this.Gateway.Send(new Authentification(this.Device));

                Logging.Info(this.GetType(), "Warning, we sent the first message, bot is in auto-mode.");

                this.Gateway.Receive();
            }
            else
            {
                Logging.Info(this.GetType(), "Warning, we are not connected to the game server.");
            }
        }

        /// <summary>
        /// Keeps the alive.
        /// </summary>
        internal void KeepAlive()
        {
            this.Timer              = new Timer();
            this.Timer.AutoReset    = true;
            this.Timer.Interval     = TimeSpan.FromSeconds(3).TotalMilliseconds;
            this.Timer.Elapsed     += (Gobelin, Land) =>
            {
                if (this.Device.Connected)
                {
                    if (this.Device.State >= State.LOGIN)
                    {
                        this.Gateway.Send(new Keep_Alive(this.Device));

                        if (this.Device.State != State.IN_BATTLE && this.Device.State > State.LOGIN)
                        {
                            // this.Gateway.Send(new Execute_Commands(this.Device));
                        }
                    }
                }
            };
            this.Timer.Start();
        }
    }
}