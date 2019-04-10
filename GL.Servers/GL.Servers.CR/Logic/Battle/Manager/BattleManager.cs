namespace GL.Servers.CR.Logic.Manager
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Timers;
    using GL.Servers.CR.Core.Network;
    using GL.Servers.CR.Logic.Mode;
    using GL.Servers.CR.Logic.Mode.Enums;
    using GL.Servers.CR.Packets.Messages.Server.Home;
    using Math = GL.Servers.CR.Logic.Math;

    internal class BattleManager
    {
        internal Timer Timer;
        internal ConcurrentDictionary<long, GameMode> Waitings;

        /// <summary>
        /// Initializes a new instance of the <see cref="BattleManager"/> class.
        /// </summary>
        public BattleManager()
        {
            this.Waitings = new ConcurrentDictionary<long, GameMode>();
            this.Timer    = new Timer();
            this.Timer.Interval = 1000;
            this.Timer.Elapsed += this.Matchmake;
            this.Timer.Start();
        }

        private void Matchmake(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            GameMode[] GameModes = this.Waitings.Values.ToArray();

            for (int i = 0; i < GameModes.Length; i++)
            {
                GameMode GameMode = GameModes[i];

                if (GameMode.Connected)
                {
                    if (GameMode.State == State.Home)
                    {
                        GameMode Best = null;

                        Search:

                        for (int j = Math.Min(GameModes.Length - 1, 100); j >= 0; j--)
                        {
                            if (i != j)
                            {
                                if (GameModes[j].Connected || true)
                                {
                                    //if (GameModes[j].Player.Score + 200 <= GameMode.Player.Score && GameModes[j].Player.Score - 200 >= GameMode.Player.Score)
                                    {
                                        if (Best == null || Math.Abs(GameMode.Player.Score - GameModes[j].Player.Score) < Math.Abs(GameMode.Player.Score - Best.Player.Score))
                                        {
                                            Best = GameModes[j];
                                        }
                                    }
                                }
                                else
                                {
                                    this.Waitings.TryRemove(GameModes[j].Player.PlayerID, out _);
                                }
                            }
                        }

                        if (Best != null)
                        {
                            if (this.Waitings.TryRemove(Best.Player.PlayerID, out _))
                            {
                                if (this.Waitings.TryRemove(GameMode.Player.PlayerID, out _))
                                {
                                    Player BestPlayer = Best.Player;

                                    Best.LoadAttackState(BestPlayer, GameMode.Player);
                                    GameMode.LoadAttackState(GameMode.Player, BestPlayer);
                                }
                                else
                                {
                                    this.Waitings.TryAdd(Best.Player.PlayerID, Best);
                                }
                            }
                            else
                                goto Search;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Adds a player in queue.
        /// </summary>
        internal void AddPlayer(GameMode GameMode)
        {
            if (GameMode.Player != null)
            {
                if (this.Waitings.TryAdd(GameMode.Player.PlayerID, GameMode))
                {
                    int Estimed;
                    int Count = this.Waitings.Count;

                    if (Count > 0)
                    {
                        if (Count > 5)
                        {
                            if (Count > 25)
                            {
                                if (Count > 100)
                                {
                                    Estimed = 5;
                                }
                                else
                                    Estimed = 15;
                            }
                            else
                                Estimed = 60;
                        }
                        else
                            Estimed = 600;
                    }
                    else
                        Estimed = 900;

#if DEBUG
                    Mode.GameMode Bot = new GameMode(null);
                    Bot.LoadHomeState(new Player(0, 1), 0, 0);
                    Bot.Player.SetName("BOT");
                    Bot.Player.SetNameSetByUser(true);
                    this.Waitings.TryAdd(Bot.Player.PlayerID, Bot);
#endif

                    new Matchmake_Info_Message(GameMode.Device, Estimed).Send();
                }
            }
        }
    }
}