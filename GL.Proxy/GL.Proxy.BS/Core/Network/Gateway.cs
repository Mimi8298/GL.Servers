namespace GL.Proxy.BS.Core.Network
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;

    using GL.Proxy.BS.Logic;
    using GL.Servers.Extensions.Binary;

    public class Gateway
    {
        private readonly Socket Server;
        
        private readonly UdpClient UdpClient;

        private readonly Thread TCPThread;
        private readonly Thread UDPThread;

        private Dictionary<string, IPEndPoint> ClientEndPoints;
        private Dictionary<string, IPEndPoint> ServerEndPoints;

        /// <summary>
        /// Initializes a new instance of the <see cref="Gateway"/> class.
        /// </summary>
        public Gateway()
        {
            this.TCPThread  = new Thread(this.ListenTCP);
            this.UDPThread  = new Thread(this.ListenUDP);

            this.UdpClient  = new UdpClient(new IPEndPoint(IPAddress.Any, 9339));
            this.Server     = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            this.Server.Bind(new IPEndPoint(IPAddress.Any, 9339));
            this.Server.Listen(100);

            this.TCPThread.Start();
            this.UDPThread.Start();
            
            this.ClientEndPoints = new Dictionary<string, IPEndPoint>(50);
            this.ServerEndPoints = new Dictionary<string, IPEndPoint>(50);

            Console.WriteLine("Gateway started on " + this.Server.LocalEndPoint + ", nigger !\n");
        }

        private void ListenTCP()
        {
            while (true)
            {
                Socket Socket = this.Server.Accept();

                if (Socket.Connected)
                {
                    Resources.Devices.Add(new Device(Socket));
                }
            }
        }

        private void ListenUDP()
        {
            Debug.WriteLine("[*] We are listening on UDP...");

            while (true)
            {
                IPEndPoint EndPoint = null;
                byte[] Buffer       = this.UdpClient.Receive(ref EndPoint);

                if (Buffer.Length >= 10)
                {
                    string SessionID = Convert.ToBase64String(Buffer.Take(10).ToArray());

                    if (!EndPoint.Address.ToString().StartsWith("192"))
                    {
                        // Console.WriteLine("[*] Received a UDP packet from SERVER : " + BitConverter.ToString(Buffer));

                        if (this.ClientEndPoints.TryGetValue(SessionID, out EndPoint))
                        {
                            Debug.WriteLine("[*] We sent " + this.UdpClient.Send(Buffer, Buffer.Length, EndPoint) + " bytes to the (" + EndPoint + "), using UDP protocol");
                        }
                        else
                            Console.WriteLine("[*] Unable to send to client the message. Unable to find the Client EndPoint.");
                    }
                    else
                    {
                        // Console.WriteLine("[*] Received a UDP packet from CLIENT " + EndPoint.Address + " : " + BitConverter.ToString(Buffer));

                        if (this.ServerEndPoints.TryGetValue(SessionID, out IPEndPoint SEndPoint))
                        {
                            if (!this.ClientEndPoints.ContainsKey(SessionID))
                            {
                                this.ClientEndPoints.Add(SessionID, EndPoint);
                            }

                            Debug.WriteLine("[*] We sent " + this.UdpClient.Send(Buffer, Buffer.Length, SEndPoint) + " bytes to the server (" + SEndPoint + "), using UDP protocol");
                        }
                        else
                            Console.WriteLine("[*] Unable to send to server the message. Unable to find the Server EndPoint.");
                    }
                }

                // Thread.Sleep(250);
            }
        }

        /// <summary>
        /// Adds a udp session.
        /// </summary>
        internal void AddUDPSession(byte[] Session, IPEndPoint ServerEndPoint)
        {
            string SessionID = Convert.ToBase64String(Session);

            if (!this.ServerEndPoints.ContainsKey(SessionID) && !this.ClientEndPoints.ContainsKey(SessionID))
            {
                this.ServerEndPoints.Add(SessionID, ServerEndPoint);
            }
            else 
                Logging.Error(this.GetType(), "Unable to add udp session. This session already exists.");
        }
    }
}