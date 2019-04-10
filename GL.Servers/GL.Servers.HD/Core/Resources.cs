namespace GL.Servers.HD.Core
{
    using System;
    using GL.Servers.HD.Core.Consoles;
    using GL.Servers.HD.Core.Database;
    using GL.Servers.HD.Core.Events;
    using GL.Servers.HD.Core.Network;
    
    using GL.Servers.HD.Files;
    using GL.Servers.HD.Logic.Slots;
    using GL.Servers.HD.Packets;

    using GL.Servers.Core;
    using GL.Servers.Files;
    using GL.Servers.Logic.Enums;

    using NLog;

    internal class Resources
    {
        internal static Logger Logger;

        internal static Redis Redis;

        internal static Players Players;
        internal static XorShift Random;

        internal static TCPServer TCPGateway;
        internal static UDPServer UDPGateway;

        internal static bool Started;

        /// <summary>
        /// Initializes a new instance of the <see cref="Resources"/> class.
        /// </summary>
        internal static void Initialize()
        {
            Resources.Logger        = LogManager.GetCurrentClassLogger(typeof(Resources));

            Factory.Initialize();
            CSV.Initialize();
            Home.Initialize();
            Fingerprint.Initialize();

            if (Constants.Database == DBMS.Mongo)
            {
                Mongo.Initialize();
            }
            
            Resources.Redis         = new Redis();

            Resources.Players       = new Players();
            Resources.Random        = new XorShift();

            Resources.TCPGateway    = new TCPServer();
            Resources.UDPGateway    = new UDPServer();

            Resources.Started       = true;

            Parser.Initialize();
            EventsHandler.Initialize();
        }
    }
}