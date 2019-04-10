namespace GL.Servers.CR.Logic.Slots
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using GL.Servers.CR.Core;
    using GL.Servers.CR.Logic.Entries;

    using Newtonsoft.Json;

    internal class AllianceMemberEntries
    {
        internal Alliance Alliance;
        internal ConcurrentDictionary<long, Player> Connected;

        [JsonProperty] private ConcurrentDictionary<long, AllianceMemberEntry> Entries;

        /// <summary>
        /// Initializes a new instance of the <see cref="Members"/> class.
        /// </summary>
        internal AllianceMemberEntries()
        {
            this.Entries    = new ConcurrentDictionary<long, AllianceMemberEntry>();
            this.Connected  = new ConcurrentDictionary<long, Player>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AllianceMemberEntries"/> class.
        /// </summary>
        /// <param name="Clan">The clan.</param>
        internal AllianceMemberEntries(Alliance Alliance) : this()
        {
            this.Alliance = Alliance;
        }

        /// <summary>
        /// Adds the specified player.
        /// </summary>
        /// <param name="Player">The player.</param>
        internal bool TryAdd(Player Player, bool NewAlliance)
        {
            if (!this.Entries.ContainsKey(Player.PlayerID))
            {
                if ((this.Entries.Count > 0 || NewAlliance) && this.Entries.Count < 50)
                {
                    AllianceMemberEntry MemberEntry = new AllianceMemberEntry(this.Alliance, Player);

                    if (!this.Entries.TryAdd(Player.PlayerID, MemberEntry))
                    {
                        Logging.Error(this.GetType(), "TryAdd() - Unable to add the player " + Player + " in alliance " + this.Alliance.AllianceID + ".");
                    }
                }
                else
                {
                    Logging.Error(this.GetType(), "TryAdd() - Player can't be added, the limit of 50 members has been reached.");
                }
            }
            else
            {
                Logging.Error(this.GetType(), "TryAdd() - Member can't be added, this instance is already in the list.");
            }

            return false;
        }

        /// <summary>
        /// Removes the specified member.
        /// </summary>
        /// <param name="Member">The member.</param>
        internal bool TryRemove(AllianceMemberEntry Member)
        {
            if (Member != null)
            {
                if (this.Entries.TryRemove(Member.PlayerID, out AllianceMemberEntry MemberEntry))
                {
                    if (!Member.Equals(MemberEntry))
                    {
                        Logging.Error(this.GetType(), "TryRemove() - Entry has been removed but TryRemove(PlayerID, out Member) returned a member not equal to the one we are trying to remove.");
                        return false;
                    }

                    return true;
                }

                this.Connected.TryRemove(Member.PlayerID, out _);
            }
            else
            {
                Logging.Error(this.GetType(), "TryRemove() - Member was null at Remove(Member).");
            }

            return false;
        }

        internal void ForEach(Action<AllianceMemberEntry> Action)
        {
            AllianceMemberEntry[] Entries = this.Entries.Values.ToArray();

            for (int i = 0; i < Entries.Length; i++)
            {
                Action.Invoke(Entries[i]);
            }
        }
    }
}