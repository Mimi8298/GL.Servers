namespace GL.Servers.BS.Logic.Slots
{
    using System.Collections.Concurrent;
    using System.Threading;

    using GL.Servers.BS.Core;
    using GL.Servers.BS.Core.Database;
    using GL.Servers.BS.Logic.Slots.Items;

    internal class Battles : ConcurrentDictionary<long, Battle>
    {
        internal Waiting Waiting;
        internal ConcurrentDictionary<long, Battle> Completed;

        private int Seed;

        /// <summary>
        /// Initializes a new instance of the <see cref="Battles"/> class.
        /// </summary>
        internal Battles()
        {
            this.Seed       = MySQL_Backup.GetSeed("Battles");

            this.Waiting    = new Waiting();
            this.Completed  = new ConcurrentDictionary<long, Battle>();
        }

        /// <summary>
        /// Adds the specified battle.
        /// </summary>
        /// <param name="Battle">The battle.</param>
        internal void Add(Battle Battle)
        {
            if (Battle.LowID == 0)
            {
                Battle.LowID = Interlocked.Increment(ref this.Seed);
            }

            if (this.ContainsKey(Battle.BattleID))
            {
                Logging.Info(this.GetType(), "Warning, the battle list already contains this battle, or at least this battle identifier.");

                if (!this.TryUpdate(Battle.BattleID, Battle, Battle))
                {
                    Logging.Error(this.GetType(), "Error, TryUpdate(BattleID, Battle, Battle) return false.");
                }
            }
            else
            {
                if (!this.TryAdd(Battle.BattleID, Battle))
                {
                    Logging.Error(this.GetType(), "Error, TryAdd(BattleID, Battle) returned false.");
                }
            }
        }

        /// <summary>
        /// Removes the specified battle.
        /// </summary>
        /// <param name="Battle">The battle.</param>
        internal void Remove(Battle Battle)
        {
            if (this.ContainsKey(Battle.BattleID))
            {
                Battle TmpBattle;

                if (this.TryRemove(Battle.BattleID, out TmpBattle))
                {
                    if (Battle.BattleID == TmpBattle.BattleID)
                    {
                        if (!this.Completed.TryAdd(Battle.BattleID, Battle))
                        {
                            Logging.Error(this.GetType(), "Error, TryAdd(BattleID, Battle) at Remove(Battle) returned false.");
                        }
                    }
                    else
                    {
                        Logging.Error(this.GetType(), "Error, the returned TmpBattle is not equal to our Battle at Remove(Battle).");
                    }
                }
                else
                {
                    Logging.Error(this.GetType(), "Error, TryRemove(BattleID, out TmpBattle) returned false.");
                }
            }
            else
            {
                Logging.Error(this.GetType(), "Error, ContainsKey(Battle.BattleID) at Remove(Battle) returned false.");
            }
        }

        /// <summary>
        /// Gets the battle using the specified battle identifier.
        /// </summary>
        /// <param name="BattleID">The battle identifier.</param>
        internal Battle Get(long BattleID)
        {
            Battle Battle;

            if (this.ContainsKey(BattleID))
            {
                if (this.TryGetValue(BattleID, out Battle))
                {
                    if (Battle.BattleID == BattleID)
                    {
                        return Battle;
                    }
                    else
                    {
                        Logging.Error(this.GetType(), "Error, the returned battle identifier is not equal to the one we requested.");
                    }
                }
                else
                {
                    Logging.Info(this.GetType(), "Warning, TryGetValue(BattleID, out Battle) returned false.");
                }
            }
            else
            {
                Logging.Info(this.GetType(), "Warning, ContainsKey(BattleID) at Remove(BattleID) returned false.");

                if (this.Completed.ContainsKey(BattleID))
                {
                    if (this.Completed.TryGetValue(BattleID, out Battle))
                    {
                        if (Battle.BattleID == BattleID)
                        {
                            return Battle;
                        }
                        else
                        {
                            Logging.Error(this.GetType(), "Error, the returned battle identifier from the completed battles list is not equal to the one we requested.");
                        }
                    }
                    else
                    {
                        Logging.Error(this.GetType(), "Error, TryGetValue(BattleID, out Battle) for the completed battles list at Get(BattleID) returned false.");
                    }
                }
                else
                {
                    Logging.Info(this.GetType(), "Warning, ContainsKey(BattleID) for the completed battles list at Get(BattleID) also returned false.");
                }
            }

            return null;
        }
    }
}