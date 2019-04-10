namespace GL.Servers.BS.Core.Network
{
    using System;
    using System.Collections.Generic;
    using System.Net.Sockets;

    using GL.Servers.BS.Logic;

    internal class Token
    {
        internal Device Device;
        internal SocketAsyncEventArgs Args;
        internal List<byte> Packet;

        internal byte[] Buffer;

        internal int Tries;

        internal bool Aborting;
        
        /// <summary>
        /// Gets a value indicating whether this <see cref="Token"/> failed.
        /// </summary>
        internal bool Failed
        {
            get
            {
                return this.Tries >= 10;
            }
        }

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
            if (!this.Aborting)
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
            if (this.Failed)
            {
                Resources.TCPGateway.Disconnect(this.Args);
            }
            else
            {
                this.Tries  = 0;
                byte[] Data = this.Packet.ToArray();

                this.Device.Process(Data);
            }
        }
    }
}