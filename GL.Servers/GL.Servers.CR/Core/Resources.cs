namespace GL.Servers.CR.Core
{
    using GL.Servers.CR.Core.Consoles;
    using GL.Servers.CR.Core.Database;
    using GL.Servers.CR.Core.Events;
    using GL.Servers.CR.Core.Network;
    using GL.Servers.CR.Files;
    using GL.Servers.CR.Logic.Slots;
    using GL.Servers.CR.Packets;

    using GL.Servers.Core;
    using GL.Servers.Core.Network;
    using GL.Servers.CR.Extensions.Game;
    using GL.Servers.CR.Logic.Manager;
    using GL.Servers.Files;
    
    using NLog;

    internal class Resources
    {
        internal static Logger Logger;
        
        internal static IDatabase Database;
        internal static IDatabase CacheSystem;

        internal static Clans Clans;
        internal static Players Players;
        internal static XorShift Random;

        internal static BattleManager BattleManager;

        internal static TCPServer TCPGateway;
        internal static UDPServer UDPGateway;
        
        internal static bool Started;

        /// <summary>
        /// Initializes a new instance of the <see cref="Resources"/> class.
        /// </summary>
        internal static void Initialize()
        {
            Resources.Logger        = LogManager.GetCurrentClassLogger(typeof(Resources));

            CSV.Initialize();
            Home.Initialize();
            Factory.Initialize();
            Globals.Initialize();
            Fingerprint.Initialize();

            Resources.TCPGateway    = new TCPServer();
            Resources.UDPGateway    = new UDPServer();

            Resources.Database      = new MySqlDatabase();
            Resources.CacheSystem   = new RedisDatabase();
            
            Resources.Players       = new Players();
            Resources.Clans         = new Clans();
            Resources.Random        = new XorShift();

            Resources.BattleManager = new BattleManager();
            
            Resources.Started       = true;

            Parser.Initialize();
            EventsHandler.Initialize();
            Timers.Initialize();
        }
    }
}