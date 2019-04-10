namespace GL.Servers.CR.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Timers;
    using GL.Servers.CR.Core.Network;
    using GL.Servers.CR.Logic;
    using GL.Servers.CR.Packets.Messages.Server.Account;
    using GL.Servers.Logic.Enums;
    using Timer = System.Timers.Timer;

    internal class Timers
    {
        private static Timer TPlayers;
        private static Timer TClans;

        private static bool Initialized;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        internal static void Initialize()
        {
            if (Timers.Initialized)
            {
                return;
            }

            Timers.Initialized = true;

            Timers.TPlayers = new Timer();
            Timers.TClans = new Timer();

            Timers.TPlayers.Elapsed += SavePlayers;
            Timers.TClans.Elapsed += SaveClans;

            Timers.TPlayers.Interval = TimeSpan.FromSeconds(30).TotalMilliseconds;
            Timers.TClans.Interval = TimeSpan.FromSeconds(30).TotalMilliseconds;

            Timers.TPlayers.AutoReset = true;
            Timers.TClans.AutoReset = true;

            Timers.TPlayers.Start();
            Timers.TClans.Start();
        }

        /// <summary>
        /// Saves the players.
        /// </summary>
        private static void SavePlayers(object Sender, ElapsedEventArgs Args)
        {
            Player[] Players = Resources.Players.Values.ToArray();
            int Saved = 0;

            for (int i = 0; i < Players.Length; i++)
            {
                Player Player = Players[i];

                if (!Player.Connected)
                {
                    if (Player.GameMode?.Device != null)
                    {
                        Resources.TCPGateway.Disconnect(Player.GameMode.Device.Token.Args);
                    }
                }
                else
                {
                    Device Device = Player.GameMode.Device;
                    long TimeSinceLastKeepAliveMs = Device.MessageManager.TimeSinceLastKeepAliveMs;

                    if (TimeSinceLastKeepAliveMs > 30000)
                    {
                        if (Device.State != State.DISCONNECTED)
                        {
                            new Disconnected_Message(Device).Send();
                        }
                        else
                        {
                            Resources.TCPGateway.Disconnect(Player.GameMode.Device.Token.Args);
                        }
                    }
                    else
                    {
                        Resources.Players.Save(Player);
                        ++Saved;
                    }
                }
            }

            Logging.Info(typeof(Timers), "Warning, saved " + Saved + " players at " + DateTime.Now.ToString("T") + " in runtime.");
        }

        /// <summary>
        /// Saves the clans.
        /// </summary>
        private static void SaveClans(object Sender, ElapsedEventArgs Args)
        {
            List<Logic.Alliance> Clans = Resources.Clans.Values.ToList();

            foreach (Logic.Alliance Clan in Clans)
            {
                Resources.Clans.Save(Clan);
            }

            Logging.Info(typeof(Timers), "Warning, saved " + Clans.Count + " clans at " + DateTime.Now.ToString("T") + " in runtime.");
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        internal static void Stop()
        {
            Timers.TPlayers.Stop();
            Timers.TClans.Stop();

            Timers.Initialized = false;
        }
    }
}