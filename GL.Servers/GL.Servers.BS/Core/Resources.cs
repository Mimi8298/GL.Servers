namespace GL.Servers.BS.Core
{
    using GL.Servers.BS.Core.Consoles;
    using GL.Servers.BS.Core.Database;
    using GL.Servers.BS.Core.Events;
    using GL.Servers.BS.Core.Network;

    using GL.Servers.BS.Extensions;
    using GL.Servers.BS.Files;
    using GL.Servers.BS.Logic.Slots;
    using GL.Servers.BS.Packets;

    using GL.Servers.Core;
    using GL.Servers.Files;

    using NLog;

    internal class Resources
    {
        internal static Logger Logger;
        internal static GameTools Gametools;

        internal static Redis Redis;

        internal static Players Players;
        internal static Clans Clans;
        internal static Battles Battles;
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
            GameDatas.Initialize();

            Resources.Gametools     = new GameTools();

            Resources.Redis         = new Redis();

            Resources.Players       = new Players();
            Resources.Clans         = new Clans();
            Resources.Battles       = new Battles();
            Resources.Random        = new XorShift();

            Resources.TCPGateway    = new TCPServer();
            Resources.UDPGateway    = new UDPServer();

            Resources.Started       = true;

            Parser.Initialize();
            EventsHandler.Initialize();
        }
    }
}