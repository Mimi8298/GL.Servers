namespace GL.Servers.GS.Core
{
    using GL.Servers.Core;
    using GL.Servers.GS.Core.Consoles;
    using GL.Servers.GS.Core.Database;
    using GL.Servers.GS.Core.Events;
    using GL.Servers.GS.Core.Network;
    using GL.Servers.GS.Files;
    using GL.Servers.GS.Logic.Slots;

    using NLog;

    internal class Resources
    {
        internal static XorShift Random;
        internal static Redis Redis;
        internal static Parser Parser;
        internal static CSV CSV;
        internal static Home Home;
        internal static Timers Checker;
        internal static Test Test;
        internal static Players Players;
        internal static Logger Logger;
        internal static TCPServer Gateway;
        internal static EventsHandler Events;

        internal static bool Started;

        /// <summary>
        /// Initializes a new instance of the <see cref="Resources"/> class.
        /// </summary>
        internal static void Initialize()
        {
            Resources.Logger    = LogManager.GetCurrentClassLogger(typeof(Resources));
            
            Resources.CSV       = new CSV();
            Resources.Home      = new Home();

            Resources.Redis     = new Redis();

            Resources.Players   = new Players();
            Resources.Random    = new XorShift();

            Resources.Test      = new Test();

            Resources.Gateway   = new TCPServer();

            Resources.Checker   = new Timers();
            Resources.Events    = new EventsHandler();
            Resources.Parser    = new Parser();

            Resources.Started   = true;
        }
    }
}