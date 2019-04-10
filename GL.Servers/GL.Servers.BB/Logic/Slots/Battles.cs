namespace GL.Servers.BB.Logic.Slots
{
    using System.Collections.Generic;
    using System.Linq;

    using GL.Servers.BB.Core;
    using GL.Servers.BB.Logic.Slots.Items;

    internal class Battles : Dictionary<long, Battle>
    {
        internal int Seed = 1;

        internal object GateWaiting = new object();
        internal object GateBattle  = new object();

        internal List<Player> Waiting;

        /// <summary>
        /// Initializes a new instance of the <see cref="Battles"/> class.
        /// </summary>
        internal Battles()
        {
            this.Waiting    = new List<Player>();
        }

        /// <summary>
        /// Adds the specified battle.
        /// </summary>
        /// <param name="Battle">The battle.</param>
        internal void Add(Battle Battle)
        {
            lock (this.GateBattle)
            {
                Battle.LowID = this.Seed++;

                if (!this.ContainsKey(Battle.LowID))
                {
                    this.Add(Battle.LowID, Battle);
                }
                else
                {
                    Logging.Error(this.GetType(), "There is already a battle with the same id in the list.");
                }
            }
        }

        /// <summary>
        /// Removes the specified battle.
        /// </summary>
        /// <param name="BattleID">The battle identifier.</param>
        internal new void Remove(long BattleID)
        {
            lock (this.GateBattle)
            {
                if (this.ContainsKey(BattleID))
                {
                    base.Remove(BattleID);
                }
                else
                {
                    Logging.Error(this.GetType(), "There is no battle with this id in the list.");
                }
            }
        }

        /// <summary>
        /// Dequeues this instance.
        /// </summary>
        /// <returns></returns>
        internal Player Dequeue()
        {
            Player Player;

            lock (this.GateWaiting)
            {
                Player = this.Waiting[0];
                this.Waiting.RemoveAt(0);
            }

            return Player;
        }

        /// <summary>
        /// Dequeues the specified count of player.
        /// </summary>
        /// <param name="Count">The count.</param>
        internal List<Player> Dequeue(int Count)
        {
            lock (this.GateWaiting)
            {
                if (this.Waiting.Count >= Count)
                {
                    List<Player> Players = this.Waiting.GetRange(0, Count).ToList();

                    this.Waiting.RemoveRange(0, Count);

                    return Players;
                }
                else
                {
                    Logging.Error(this.GetType(), "We don't have enough player in the waiting list to dequeue.");
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
            lock (this.GateWaiting)
            {
                this.Waiting.Add(Player);
            }
        }

        /// <summary>
        /// Gets the specified battle using the battle identifier.
        /// </summary>
        /// <param name="_BattleID">The battle identifier.</param>
        internal Battle Get(int HighID, int LowID)
        {
            if (this.ContainsKey(LowID))
            {
                return this[LowID];
            }
            else
            {
                Logging.Error(this.GetType(), "The battle we are trying to get is not in the list.");
            }

            return null;
        }
    }
}