namespace GL.Clients.CoC.Logic
{
    using System;
    using System.Diagnostics;
    using System.Net.Sockets;
    using System.Threading;

    using GL.Clients.CoC.Core.Network;
    using GL.Clients.CoC.Logic.Enums;
    using GL.Clients.CoC.Packets.Messages.Client;

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
            // this.KeepAlive();

            this.Socket.Connect("game.clashofclans.com", 9339);

            if (this.Socket.Connected)
            {
                this.Gateway.Send(new Pre_Authentification(this.Device));
                
                Debug.WriteLine("[*] Warning : We sent the first message, bot is in auto-mode.");

                this.Gateway.Receive();

                // this.LetsCheat();
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
            Thread.Sleep(5000);

            while (this.Device.Connected)
            {
                this.Gateway.Send(new Ask_Battle_Result(this.Device));
                this.Device.State = State.IN_BATTLE;

                Thread.Sleep(5000);

                this.Gateway.Send(new Go_Home(this.Device));
                this.Device.State = State.LOGGED;

                Thread.Sleep(5000);
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
                        this.Gateway.Send(new Execute_Commands(this.Device));
                    }
                }
            };
            this.Timer.Start();
        }
    }
}