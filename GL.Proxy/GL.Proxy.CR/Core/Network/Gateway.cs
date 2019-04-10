namespace GL.Proxy.CR.Core.Network
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;

    using GL.Proxy.CR.Logic;
    using GL.Servers.Extensions.Binary;

    public class Gateway
    {
        private readonly Socket Server;
        
        private readonly UdpClient UdpClient;

        private readonly Thread TCPThread;
        private readonly Thread UDPThread;

        private readonly Dictionary<string, Device> UdpDevices;

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
            
            this.UdpDevices = new Dictionary<string, Device>(32);

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

                    if (this.UdpDevices.TryGetValue(SessionID, out Device Device))
                    {
                        bool fromServer = !EndPoint.Address.ToString().StartsWith("192");

                        if (!fromServer)
                        {
                            Device.UDPClientEndPoint = EndPoint;
                        }

                        this.ProcessReceiveUDP(Buffer, Device, fromServer);
                    }
                    else
                        Console.WriteLine("Session " + SessionID + " doesn't exists.");
                }
            }
        }

        /// <summary>
        /// Processes the received packet from udp.
        /// </summary>
        internal void ProcessReceiveUDP(byte[] Buffer, Device Device, bool fromServer)
        {
            if (Buffer.Length >= 10)
            {
                List<byte> Rebuilt = new List<byte>(4096);

                using (Reader Reader = new Reader(Buffer))
                {
                    byte[] Session = Reader.ReadBytes(10);

                    Rebuilt.AddRange(Session);

                    /*
                    if (Buffer.Length >= 12)
                    {
                        Rebuilt.AddVInt(Reader.ReadVInt());

                        int Count = Reader.ReadVInt();

                        Rebuilt.AddVInt(Count);

                        for (int i = Count; i > 0; i--)
                        {
                            Rebuilt.AddRange(new PacketUDP(Reader.ReadBytes(Reader.ReadVInt()), Device).ToBytes);
                        }
                    }
                    */

                    Rebuilt.AddRange(Reader.ReadBytes((int) (Reader.BaseStream.Length - Reader.BaseStream.Position)));
                }

                this.SendUDP(Rebuilt.ToArray(), fromServer ? Device.UDPClientEndPoint : Device.UDPServerEndPoint);
            }
        }

        /// <summary>
        /// Sends udp packet.
        /// </summary>
        internal async void SendUDP(byte[] Buffer, IPEndPoint EndPoint)
        {
            try
            {
                Logging.Info(this.GetType(), await this.UdpClient.SendAsync(Buffer, Buffer.Length, EndPoint) + " bytes sended to " + EndPoint + " from udp socket.");
            }
            catch (Exception)
            {
                
            }
        }
        
        /// <summary>
        /// Adds a udp session.
        /// </summary>
        internal void AddUDPSession(Device Device, byte[] Session)
        {
            this.UdpDevices.Add(Convert.ToBase64String(Session), Device);
        }
    }
}