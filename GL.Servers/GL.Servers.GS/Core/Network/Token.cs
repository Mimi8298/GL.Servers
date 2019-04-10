namespace GL.Servers.GS.Core.Network
{
    using System;
    using System.Collections.Generic;
    using System.Net.Sockets;

    using GL.Servers.GS.Logic;
    
    internal class Token : IDisposable
    {
        internal Device Device;
        internal SocketAsyncEventArgs Args;
        internal List<byte> Packet;

        internal byte[] Buffer;

        internal bool Aborting;
        internal bool Disposed;

        internal int Tries;

        /// <summary>
        /// Initializes a new instance of the <see cref="Token"/> class.
        /// </summary>
        /// <param name="Args">The <see cref="SocketAsyncEventArgs"/> instance containing the event data.</param>
        /// <param name="Device">The device.</param>
        internal Token(SocketAsyncEventArgs Args, Device Device)
        {
            this.Device         = Device;
            this.Device.Token   = this;

            this.Args           = Args;
            this.Args.UserToken = this;

            this.Buffer         = new byte[Constants.ReceiveBuffer];
            this.Packet         = new List<byte>(Constants.ReceiveBuffer);
        }

        /// <summary>
        /// Sets the data.
        /// </summary>
        internal void SetData()
        {
            if (!this.Disposed)
            {
                byte[] Data = new byte[this.Args.BytesTransferred];
                Array.Copy(this.Args.Buffer, 0, Data, 0, this.Args.BytesTransferred);
                this.Packet.AddRange(Data);
            }

            this.Tries += 1;
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal void Process()
        {
            if (this.Tries > 10)
            {
                Resources.Gateway.Disconnect(this.Args);
            }
            else
            {
                this.Tries = 0;

                byte[] Data = this.Packet.ToArray();
                this.Device.Process(Data);
            }
        }

        /// <summary>
        /// Exécute les tâches définies par l'application associées à la libération ou à la redéfinition des ressources non managées.
        /// </summary>
        public void Dispose()
        {
            this.Buffer = null;
            this.Packet = null;
            this.Device = null;

            this.Tries  = 0;

            this.Disposed = true;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="Token"/> class.
        /// </summary>
        ~Token()
        {
            if (!this.Aborting)
            {
                Resources.Gateway.Disconnect(this.Args);
            }

            if (!this.Disposed)
            {
                this.Dispose();
            }

            GC.SuppressFinalize(this);
        }
    }
}