namespace GL.Proxy.CR.Logic
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Collections.Generic;

    using GL.Proxy.CR.Core;
    using GL.Proxy.CR.Core.Crypto;
    using GL.Proxy.CR.Core.Network;
    using GL.Servers.Extensions.Binary;
    using GL.Servers.Extensions.List;

    public class Device
    {
        public Processor Processor;
        public EnDecrypt EnDecrypt;

        public Socket Client;
        public Socket Server;

        public IPEndPoint UDPClientEndPoint;
        public IPEndPoint UDPServerEndPoint;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Device"/> class.
        /// </summary>
        /// <param name="Socket">The socket.</param>
        public Device(Socket Socket)
        {
            this.Client     = Socket;
            this.Server     = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            this.Server.Connect("game.clashroyaleapp.com", 9339);

            Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\Logs\\" + ((IPEndPoint) this.Client.RemoteEndPoint).Address + "\\TCP");
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\Logs\\" + ((IPEndPoint) this.Client.RemoteEndPoint).Address + "\\UDP");

            this.EnDecrypt  = new EnDecrypt();
            this.Processor  = new Processor(this.Client, this.Server);
        }

        /// <summary>
        /// Receives the packet.
        /// </summary>
        internal void Receive(int Type, ref byte[] Packet)
        {
            using (Reader Reader = new Reader(Packet))
            {
                switch (Type)
                {
                    case 24112:
                    {
                        int Port = Reader.ReadVInt();
                        string Host = Reader.ReadString();
                        byte[] Session = Reader.ReadBytes();
                        string Nonce = Reader.ReadString();

                        List<byte> Data = new List<byte>(128);

                        Data.AddVInt(9339);
                        Data.AddString("176.159.83.126");
                        Data.AddBytes(Session);
                        Data.AddString(Nonce);

                        Packet = Data.ToArray();
                            
                        Resources.Gateway.AddUDPSession(this, Session);
                        
                        this.EnDecrypt.StartUDP(Nonce);
                        this.UDPServerEndPoint = new IPEndPoint(IPAddress.Parse(Host), Port);
                        
                        break;
                    }

                    case 21902:
                    {
                        int Turn = Reader.ReadVInt();
                        int Checksum = Reader.ReadVInt();
                        byte[] Others = Reader.ReadBytes((int) (Reader.BaseStream.Length - Reader.BaseStream.Position));

                        List<byte> Data = new List<byte>(128);

                        Data.AddVInt(Turn);
                        Data.AddVInt(0);
                        Data.AddRange(Others);

                        Console.WriteLine("SectorHeartbeat: Turn:" + Turn + " Checksum:" + Checksum);

                        Packet = Data.ToArray();

                        break;
                    }
                }
            }
        }
    }
}