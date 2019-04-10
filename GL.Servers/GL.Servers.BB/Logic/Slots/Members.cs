namespace GL.Servers.BB.Logic.Slots
{
    using System.Collections.Concurrent;

    using GL.Servers.BB.Core;
    using GL.Servers.BB.Logic.Enums;
    using GL.Servers.BB.Logic.Slots.Items;

    using Newtonsoft.Json;

    internal class Members
    {
        [JsonProperty("clan")]      internal Clan Clan;
        [JsonProperty("entries")]   internal ConcurrentDictionary<long, Member> Entries;

        /// <summary>
        /// Initializes a new instance of the <see cref="Members"/> class.
        /// </summary>
        internal Members()
        {
            this.Entries    = new ConcurrentDictionary<long, Member>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Members"/> class.
        /// </summary>
        /// <param name="Clan">The clan.</param>
        internal Members(Clan Clan) : this()
        {
            this.Clan       = Clan;
        }

        /// <summary>
        /// Adds the specified player.
        /// </summary>
        /// <param name="Player">The player.</param>
        internal bool TryAdd(Player Player)
        {
            Member Member = new Member(Player);

            if (!this.Entries.ContainsKey(Member.PlayerID))
            {
                if (this.Entries.Count < 50)
                {
                    if (this.Entries.Count < 1)
                    {
                        // Member.Role = Role.Leader;
                    }

                    if (this.Entries.TryAdd(Member.PlayerID, Member))
                    {
                        Player.AllianceHighID   = this.Clan.HighID;
                        Player.AllianceLowID    = this.Clan.LowID;

                        return true;
                    }
                    else
                    {
                        Logging.Error(this.GetType(), "Failed to add a member, TryAdd(Member) returned false.");
                    }
                }
                else
                {
                    Logging.Error(this.GetType(), "Player can't be added, the limit of 50 members has been reached.");
                }
            }
            else
            {
                Logging.Error(this.GetType(), "Member can't be added, this instance is already in the list.");
            }

            return false;
        }

        /// <summary>
        /// Removes the specified member.
        /// </summary>
        /// <param name="Member">The member.</param>
        internal bool TryRemove(Member Member)
        {
            if (Member != null)
            {
                if (this.Entries.ContainsKey(Member.PlayerID))
                {
                    Member TmpMember;

                    if (this.Entries.TryRemove(Member.PlayerID, out TmpMember))
                    {
                        if (TmpMember.Equals(Member))
                        {
                            return true;
                        }
                        else
                        {
                            Logging.Error(this.GetType(), "Entry has been removed but TryRemove(PlayerID, out Member) returned a member not equal to the one we are trying to remove.");
                        }
                    }
                    else
                    {
                        Logging.Error(this.GetType(), "Member failed to remove, TryRemove(PlayerID, out Member) returned false.");
                    }
                }
                else
                {
                    Logging.Error(this.GetType(), "Member can't be removed, instance is not present in the list.");
                }
            }
            else
            {
                Logging.Error(this.GetType(), "Member was null at Remove(Member).");
            }

            return false;
        }
    }
}