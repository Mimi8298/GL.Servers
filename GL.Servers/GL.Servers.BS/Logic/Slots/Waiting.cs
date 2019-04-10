namespace GL.Servers.BS.Logic.Slots
{
    using System.Collections.Concurrent;
    using System.Linq;

    using GL.Servers.BS.Core;

    using GL.Servers.Logic.Enums;

    internal class Waiting : ConcurrentQueue<Player>
    {
        private object Gate;

        /// <summary>
        /// Initializes a new instance of the <see cref="Waiting"/> class.
        /// </summary>
        internal Waiting()
        {
            this.Gate = new object();
        }

        /// <summary>
        /// Dequeues and return a player from this instance.
        /// </summary>
        internal Player Dequeue()
        {
            Player Player;

            lock (this.Gate)
            {
                if (this.Count > 0)
                {
                    if (this.TryDequeue(out Player))
                    {
                        if (Player.Device.State != State.MATCHMAKING)
                        {
                            Logging.Info(this.GetType(), "Warning, TryDequeue(out Player) returned a player who wasn't in the matchmaking system.");

                            while (this.Count > 0)
                            {
                                if (this.TryDequeue(out Player))
                                {
                                    if (Player.Device.State == State.MATCHMAKING)
                                    {
                                        return Player;
                                    }
                                    else
                                    {
                                        Logging.Info(this.GetType(), "Warning, TryDequeue(out Player) in the while loop returned a player who wasn't in the matchmaking system.");
                                    }
                                }
                                else
                                {
                                    Logging.Info(this.GetType(), "Warning, TryDequeue(out Player) inside the while loop returned false.");
                                }
                            }

                            Logging.Info(this.GetType(), "Warning, the while used to search a player currently in a matchmake has terminated.");
                        }
                        else
                        {
                            return Player;
                        }
                    }
                    else
                    {
                        Logging.Info(this.GetType(), "Warning, TryDequeue(out Player) returned false.");
                    }
                }
                else
                {
                    Logging.Info(this.GetType(), "Warning, not enough players in the waiting list at Dequeue().");
                }
            }

            return null;
        }

        /// <summary>
        /// Dequeues the specified count of players from this instance.
        /// </summary>
        /// <param name="Count">The count.</param>
        internal Player[] Dequeue(int Count)
        {
            lock (this.Gate)
            {
                if (this.Count >= Count)
                {
                    Player[] Players = new Player[Count];

                    for (int i = 0; i < Count; i++)
                    {
                        Player Player;

                        if (this.TryDequeue(out Player))
                        {
                            Players[i] = Player;

                            if (Player.Device.State != State.MATCHMAKING)
                            {
                                Logging.Info(this.GetType(), "Warning, TryDequeue(out Player) at Dequeue(Count) returned a player who left the matchmaking system.");
                            }
                        }
                        else
                        {
                            Logging.Error(this.GetType(), "Error, TryDequeue(out Player) at Dequeue(Count), even if enough players in the list, returned false for one of them.");
                        }
                    }

                    bool Ready = true;

                    foreach (Player Player in Players)
                    {
                        if (Player.Device.State != State.MATCHMAKING)
                        {
                            Ready = false;
                            break;
                        }
                    }

                    if (Ready == false)
                    {
                        Logging.Info(this.GetType(), "Warning, Ready == false at Dequeue(Count).");

                        foreach (Player Player in Players)
                        {
                            if (Player.Device.State == State.MATCHMAKING)
                            {
                                this.Enqueue(Player);
                            }
                        }
                    }
                    else
                    {
                        return Players;
                    }
                }
                else
                {
                    Logging.Info(this.GetType(), "Warning, not enough players in the waiting list at Dequeue(Count).");
                }
            }

            return null;
        }

        /// <summary>
        /// Enqueues the specified player.
        /// </summary>
        /// <param name="Player">The player.</param>
        internal void Enqueue(Player Player)
        {
            if (!this.Contains(Player))
            {
                base.Enqueue(Player);
            }
        }
    }
}