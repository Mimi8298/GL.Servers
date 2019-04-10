namespace GL.Servers.BS.Logic.Slots.Items
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Timers;

    using GL.Servers.BS.Core;
    using GL.Servers.BS.Core.Network;
    using GL.Servers.BS.Packets.Messages.Server;

    using Timer = System.Timers.Timer;

    internal class Battle
    {
        /// <summary>
        /// Gets the battle identifier.
        /// </summary>
        internal long BattleID
        {
            get
            {
                return (long) this.HighID << 32 | (uint) this.LowID;
            }
        }

        internal int HighID;
        internal int LowID;

        internal int Tick;

        internal bool Started;
        internal bool Stopped;

        internal Timer Timer;
        internal Dictionary<long, Player> Players;

        private object Gate;

        /// <summary>
        /// Gets a value indicating whether all players are disconnected.
        /// </summary>
        /// <value>
        ///   <c>true</c> if all players are disconnected, otherwise <c>false</c>.
        /// </value>
        internal bool AllDisconnected
        {
            get
            {
                foreach (Player Player in this.Players.Values)
                {
                    if (Player.Device.Connected)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is running.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is running, otherwise <c>false</c>.
        /// </value>
        internal bool isRunning
        {
            get
            {
                return this.Started && !this.Stopped;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Battle"/> class.
        /// </summary>
        internal Battle()
        {
            this.Timer              = new Timer();
            this.Timer.AutoReset    = true;
            this.Timer.Interval     = 2000;
            this.Timer.Elapsed     += Process;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Battle"/> class.
        /// </summary>
        /// <param name="Players">The players.</param>
        internal Battle(IEnumerable<Player> Players) : this()
        {
            this.Players = new Dictionary<long, Player>();

            foreach (Player Player in Players)
            {
                this.Players.Add(Player.PlayerID, Player);
            }
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        internal void Start()
        {
            if (!this.Started)
            {
                this.Started = true;

                if (!this.Stopped)
                {
                    foreach (Player Player in this.Players.Values)
                    {
                        new Sector_PC(Player.Device, this).Send();
                    }

                    this.Timer.Start();
                }
                else
                {
                    Logging.Error(this.GetType(), "Battle was already stopped when Start() has been called.");
                }
            }
            else
            {
                Logging.Error(this.GetType(), "Battle was already started when Start() has been called.");
            }
        }

        /// <summary>
        /// Processes the specified sender.
        /// </summary>
        /// <param name="Sender">The sender.</param>
        /// <param name="Args">The <see cref="ElapsedEventArgs"/> instance containing the event data.</param>
        private void Process(object Sender, ElapsedEventArgs Args)
        {
            if (!this.Stopped)
            {
                if (this.Started)
                {
                    int CurrentSeed = Interlocked.Increment(ref this.Tick);

                    Logging.Info(this.GetType(), "Heartbeat n°" + CurrentSeed + " !");

                    foreach (Player Player in this.Players.Values)
                    {
                        if (Player.Device.Connected)
                        {
                            new Battle_Heartbeat(Player.Device, this, CurrentSeed).Send();
                        }
                    }

                    if (this.AllDisconnected)
                    {
                        this.Stop();
                    }
                }
                else
                {
                    Logging.Error(this.GetType(), "Battle wasn't started when Process() has been called.");
                }
            }
            else
            {
                Logging.Error(this.GetType(), "Battle was already stopped when Process() has been called.");
            }
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        internal void Stop()
        {
            if (!this.Stopped)
            {
                this.Stopped = true;

                if (this.Started)
                {
                    this.Timer.Stop();

                    foreach (Player Player in this.Players.Values)
                    {
                        new Battle_Result(Player.Device, this).Send();
                    }
                }
                else
                {
                    Logging.Error(this.GetType(), "Battle wasn't even started when Stop() has been called");
                }
            }
            else
            {
                Logging.Error(this.GetType(), "Battle was already stopped when Stop() has been called.");
            }
        }
    }
}
 