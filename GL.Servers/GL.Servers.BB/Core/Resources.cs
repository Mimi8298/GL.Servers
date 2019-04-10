namespace GL.Servers.BB.Core
{
    using GL.Servers.BB.Core.Consoles;
    using GL.Servers.BB.Core.Database;
    using GL.Servers.BB.Core.Events;
    using GL.Servers.BB.Core.Network;
    using GL.Servers.BB.Extensions;
    using GL.Servers.BB.Files;
    using GL.Servers.BB.Logic.Slots;
    using GL.Servers.BB.Packets;
    using GL.Servers.Core;
    using GL.Servers.Files;
    using GL.Servers.Logic.Enums;
    using NLog;

    internal class Resources
    {
        internal static Logger Logger;
        internal static GameSettings GameSettings;

        internal static Redis Redis;

        internal static Players Players;
        internal static Clans Clans;
        internal static Battles Battles;
        internal static XorShift Random;

        internal static TCPServer TCPGateway;

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
            Update.Initialize();
            Fingerprint.Initialize();

            Resources.GameSettings  = new GameSettings();

            if (Constants.Database == DBMS.Mongo)
            {
                Mongo.Initialize();
            }
            else
                Resources.Redis     = new Redis();

            Resources.Players       = new Players();
            Resources.Clans         = new Clans();
            Resources.Battles       = new Battles();
            Resources.Random        = new XorShift();

            Resources.TCPGateway    = new TCPServer();

            Resources.Started       = true;

            Parser.Initialize();
            EventsHandler.Initialize();
        }
    }
}