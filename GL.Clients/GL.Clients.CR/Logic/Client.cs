namespace GL.Clients.CR.Logic
{
    using System;
    using System.Diagnostics;
    using System.Net.Sockets;
    using System.Threading;
    using System.Threading.Tasks;

    using GL.Clients.CR.Core.Network;
    using GL.Clients.CR.Logic.Enums;
    using GL.Clients.CR.Packets.Messages.Client;
    using GL.Servers.Library;

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

            Debug.WriteLine("[*] Connecting to game.clashroyaleapp.com on port 9339...");

            this.Socket.Connect("game.clashroyaleapp.com", 9339);

            if (this.Socket.Connected)
            {
                Debug.WriteLine("[*]  Connected to " + this.Socket.RemoteEndPoint + ".");

                this.Gateway.Send(new Pre_Authentification(this.Device));
                this.Gateway.Receive();

                // Task.Run(() => this.LetsCheat());
            }
            else
            {
                Debug.WriteLine("[*] Warning : We are not connected to the game server.");
            }
        }

        /// <summary>
        /// Lets cheat and win gold + experience.
        /// </summary>
        internal void LetsCheat()
        {
            while (this.Device.Connected)
            {
                this.Device.Crypto = new Rjindael();
                Thread.Sleep(100);

                this.Gateway.Send(new Authentification(this.Device));
                Thread.Sleep(100);
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
                    this.Gateway.Send(new Keep_Alive(this.Device));

                    if (this.Device.State != State.IN_BATTLE && this.Device.State > State.SESSION_OK)
                    {
                        // this.Gateway.Send(new Execute_Commands(this.Device));
                    }
                }
            };
            this.Timer.Start();
        }
    }
}