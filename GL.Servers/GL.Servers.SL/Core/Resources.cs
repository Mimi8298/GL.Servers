namespace GL.Servers.SL.Core
{
    using System;
    using GL.Servers.SL.Core.Consoles;
    using GL.Servers.SL.Core.Database;
    using GL.Servers.SL.Core.Events;
    using GL.Servers.SL.Core.Network;
    
    using GL.Servers.SL.Files;
    using GL.Servers.SL.Logic.Slots;
    using GL.Servers.SL.Packets;

    using GL.Servers.Core;
    using GL.Servers.Files;
    using GL.Servers.Logic.Enums;
    using GL.Servers.SL.Extensions.Game;
    using NLog;

    internal class Resources
    {
        internal static Logger Logger;

        internal static Redis Redis;

        internal static Players Players;
        internal static Clans Clans;
        // internal static Battles Battles;
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
            GameSettings.Initialize();

            if (Constants.Database == DBMS.Mongo)
            {
                Mongo.Initialize();
            }
            
            Resources.Redis         = new Redis();

            Resources.Players       = new Players();
            Resources.Clans         = new Clans();
            // Resources.Battles       = new Battles();
            Resources.Random        = new XorShift();

            Resources.TCPGateway    = new TCPServer();
            Resources.UDPGateway    = new UDPServer();

            Resources.Started       = true;

            Parser.Initialize();
            EventsHandler.Initialize();
        }
    }
}