namespace GL.Servers.Core.Network
{
    using System;
    using System.Diagnostics;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;

    using GL.Servers.Extensions.Binary;

    public class UDPServer
    {
        internal UdpClient Server;
        internal Thread Thread;
        internal IPEndPoint Endpoint;

        internal ManualResetEvent Event;

        /// <summary>
        /// Initializes a new instance of the <see cref="UDPServer"/> class.
        /// </summary>
        public UDPServer()
        {
            this.Thread             = new Thread(this.Listen);
            this.Endpoint           = new IPEndPoint(IPAddress.Any, 9339);
            this.Server             = new UdpClient(this.Endpoint);
            this.Event              = new ManualResetEvent(false);

            Console.WriteLine("UDP Server is listening on " + this.Endpoint + ".");

            this.Thread.Start();
        }

        /// <summary>
        /// Listens this instance.
        /// </summary>
        internal void Listen()
        {
            while (true)
            {
                this.Event.Reset();
                this.Server.BeginReceive(this.ProcessAccept, null);
                this.Event.WaitOne();
            }
        }

        /// <summary>
        /// Begins to accept the client.
        /// </summary>
        /// <param name="AsyncResult">The asynchronous result.</param>
        internal void ProcessAccept(IAsyncResult AsyncResult)
        {
            IPEndPoint Device   = null;
            byte[] Buffer       = this.Server.EndReceive(AsyncResult, ref Device);

            this.Event.Set();

            Debug.WriteLine("[*] " + this.GetType().Name + " : " + BitConverter.ToString(Buffer));

            this.ProcessPacket(Buffer);

            if (Buffer.Length >= 1400)
            {
                this.Server.Send(Buffer, Buffer.Length, Device);
            }
        }

        /// <summary>
        /// Processes the packet.
        /// </summary>
        /// <param name="Buffer">The buffer.</param>
        internal void ProcessPacket(byte[] Buffer)
        {
            using (Reader Reader = new Reader(Buffer))
            {
                string SessionID = Encoding.UTF8.GetString(Reader.ReadBytes(10));

                if (Reader.BaseStream.Length > 10)
                {
                    Debug.WriteLine("[*] " + this.GetType().Name + " : " + BitConverter.ToString(Reader.ReadBytes((int) (Reader.BaseStream.Length - Reader.BaseStream.Position))));
                }
            }
        }
    }
}