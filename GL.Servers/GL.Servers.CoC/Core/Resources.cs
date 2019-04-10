namespace GL.Servers.CoC.Core
{
    using System.Text.RegularExpressions;

    using GL.Servers.CoC.Core.Consoles;
    using GL.Servers.CoC.Core.Database;
    using GL.Servers.CoC.Core.Events;
    using GL.Servers.CoC.Core.Network;
    
    using GL.Servers.CoC.Files;
    using GL.Servers.CoC.Logic.Slots;
    using GL.Servers.CoC.Packets;

    using GL.Servers.Core;
    using GL.Servers.Files;
    using GL.Servers.Logic.Enums;
    using GL.Servers.CoC.Extensions.Game;
    using GL.Servers.CoC.Logic;
    using NLog;

    internal class Resources
    {
        internal static Logger Logger;

        internal static Redis Redis;
        internal static Regex Regex;

        internal static Accounts Accounts;
        internal static Clans Clans;
        internal static Chats Chats;
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
            LevelFile.Initialize();
            Fingerprint.Initialize();
            Globals.Initialize();

            if (Constants.Database == DBMS.Mongo)
            {
                Mongo.Initialize();
            }
            
            Resources.Redis         = new Redis();
            Resources.Regex         = new Regex("[ ]{2,}", RegexOptions.Compiled);

            Resources.Accounts      = new Accounts();
            Resources.Clans         = new Clans();
            Resources.Chats         = new Chats();
            Resources.Battles       = new Battles();
            Resources.Random        = new XorShift();

            Resources.TCPGateway    = new TCPServer();

            Resources.Started       = true;

            Parser.Initialize();
            EventsHandler.Initialize();
        }
    }
}