namespace GL.Servers.SP.Core
{
    using System;
    using System.Text.RegularExpressions;
    using GL.Servers.SP.Core.Consoles;
    using GL.Servers.SP.Core.Database;
    using GL.Servers.SP.Core.Events;
    using GL.Servers.SP.Core.Network;
    
    using GL.Servers.SP.Files;
    using GL.Servers.SP.Logic.Slots;
    using GL.Servers.SP.Packets;

    using GL.Servers.Core;
    using GL.Servers.Files;
    using GL.Servers.Logic.Enums;
    using GL.Servers.SP.Extensions.Game;
    using NLog;

    internal class Resources
    {
        internal static Logger Logger;

        internal static Redis Redis;
        internal static Regex Regex;

        internal static Players Players;
        internal static Clans Clans;
        internal static Chats Chats;
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
            LevelFile.Initialize();
            Fingerprint.Initialize();
            Globals.Initialize();

            if (Constants.Database == DBMS.Mongo)
            {
                Mongo.Initialize();
            }
            
            Resources.Redis         = new Redis();
            Resources.Regex         = new Regex("[ ]{2,}", RegexOptions.Compiled);

            Resources.Players       = new Players();
            Resources.Clans         = new Clans();
            Resources.Chats         = new Chats();
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