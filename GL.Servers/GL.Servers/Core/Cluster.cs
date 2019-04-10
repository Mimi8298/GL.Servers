namespace GL.Servers.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;

    public class Cluster
    {
        private readonly Socket Checker;

        /// <summary>
        /// Array of slave servers endpoint.
        /// </summary>
        private readonly List<IPEndPoint> Servers;

        private readonly string IPAddr;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cluster"/> class.
        /// </summary>
        public Cluster()
        {
            this.Servers        = new List<IPEndPoint>();
            WebClient WebClient = new WebClient();

            this.Checker        = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.Checker.Bind(new IPEndPoint(IPAddress.Any, 0));

            try
            {
                if (string.IsNullOrWhiteSpace(this.IPAddr = WebClient.DownloadString("http://api.ipify.org/")))
                {
                    this.IPAddr = "0.0.0.0";
                }
                else
                {
                    this.Servers.Add(new IPEndPoint(IPAddress.Parse(this.IPAddr), 9339));
                }
            }
            catch (Exception)
            {
                // Triggered.
            }

            if (this.Servers.Count > 0)
            {
                if (this.Servers.Count == 1 && this.Servers[0].Address.ToString() == this.IPAddr)
                {
                    Console.WriteLine("Server(s) are synchronized, we can start loading the program.");
                }
                else
                {
                    Console.WriteLine("Waiting for slaves to synchronize...");

                    while (!this.Check())
                    {
                        Console.Write(".");

                        Thread.Sleep(1000);
                    }

                    Console.WriteLine("The whole cluster is synchronized !");
                }
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Checks this instance.
        /// </summary>
        /// <returns>Whether every servers are synchronized.</returns>
        internal bool Check()
        {
            bool Success = true;

            List<IPEndPoint> Disconnected = new List<IPEndPoint>(this.Servers.Count);

            foreach (IPEndPoint Endpoint in this.Servers)
            {
                try
                {
                    this.Checker.Connect(Endpoint);
                }
                catch (Exception)
                {
                    // Disconnected.
                }
                finally
                {
                    if (!this.Checker.Connected)
                    {
                        Success = false;
                        Disconnected.Add(Endpoint);
                    }
                    else
                    {
                        this.Checker.Disconnect(true);
                    }
                }
            }

            if (Disconnected.Count > 0)
            {
                Debug.WriteLine("[*] Cluster : " + string.Join(", ", Disconnected) + " " + (Disconnected.Count > 1 ? "are" : "is") + " disconnected.");
            }

            return Success;
        }
    }
}