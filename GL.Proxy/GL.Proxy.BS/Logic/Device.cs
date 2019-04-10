namespace GL.Proxy.BS.Logic
{
    using System.IO;
    using System.Net;
    using System.Net.Sockets;

    using GL.Proxy.BS.Core.Crypto;
    using GL.Proxy.BS.Core.Network;

    public class Device
    {
        public Processor Processor;
        public EnDecrypt EnDecrypt;

        public Socket Client;
        public Socket Server;

        /// <summary>
        /// Initializes a new instance of the <see cref="Device"/> class.
        /// </summary>
        /// <param name="Socket">The socket.</param>
        public Device(Socket Socket)
        {
            this.Client     = Socket;
            this.Server     = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            this.Server.Connect("game.brawlstarsgame.com", 9339);
            // this.Server.Connect("stage.brawlstarsgame.com", 9339);

            Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\Logs\\" + ((IPEndPoint)this.Client.RemoteEndPoint).Address);

            this.EnDecrypt  = new EnDecrypt();
            this.Processor  = new Processor(this.Client, this.Server);
        }
    }
}