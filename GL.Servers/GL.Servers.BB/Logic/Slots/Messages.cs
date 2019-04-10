namespace GL.Servers.BB.Logic.Slots
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    using GL.Servers.BB.Core;
    using GL.Servers.BB.Logic.Slots.Items;

    using GL.Servers.Extensions.List;

    using Newtonsoft.Json;

    internal class Messages
    {
        /*
        [JsonProperty("seed")]      private int Seed;

        [JsonProperty("clan")]      private Clan Clan;
        [JsonProperty("entries")]   private ConcurrentDictionary<long, IEntry> Entries;

        /// <summary>
        /// Initializes a new instance of the <see cref="Messages"/> class.
        /// </summary>
        internal Messages()
        {
            this.Entries    = new ConcurrentDictionary<long, IEntry>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Messages"/> class.
        /// </summary>
        /// <param name="Clan">The clan.</param>
        internal Messages(Clan Clan) : this()
        {
            this.Clan       = Clan;
        }

        /// <summary>
        /// Adds the specified entry.
        /// </summary>
        /// <param name="Entry">The entry.</param>
        internal bool TryAdd(IEntry Entry)
        {
            if (Entry != null)
            {
                if (Entry.MessageID > 0)
                {
                    if (!this.Entries.ContainsKey(Entry.MessageID))
                    {
                        if (this.Entries.TryAdd(Entry.MessageID, Entry))
                        {
                            foreach (Member Member in this.Clan.Members.Entries.Values.ToList())
                            {
                                if (Member.Connected)
                                {
                                    Player Player = Member.Player;

                                    if (Player != null)
                                    {
                                        if (Player.Device.Connected)
                                        {
                                            // new Clan_Stream_Entry(Player.Device, Entry).Send();
                                        }
                                    }
                                    else
                                    {
                                        Logging.Error(this.GetType(), "Entry failed to propagate, one (or more) player instance were null at the foreach.");
                                    }
                                }
                            }

                            return true;
                        }
                        else
                        {
                            Logging.Error(this.GetType(), "Entry can't be added, TryAdd(MessageID, Entry) returned false.");
                        }
                    }
                    else
                    {
                        Logging.Error(this.GetType(), "Entry can't be added with the specified MessageID because the slot is already used.");
                    }
                }
                else
                {
                    Entry.Message_LowID = Interlocked.Increment(ref this.Seed);

                    if (this.Entries.TryAdd(Entry.MessageID, Entry))
                    {
                        foreach (Member Member in this.Clan.Members.Entries.Values.ToList())
                        {
                            if (Member.Connected)
                            {
                                Player Player = Member.Player;

                                if (Player != null)
                                {
                                    if (Player.Device.Connected)
                                    {
                                        // new Clan_Stream_Entry(Player.Device, Entry).Send();
                                    }
                                }
                                else
                                {
                                    Logging.Error(this.GetType(), "Entry failed to propagate, one (or more) player instance were null at the foreach for Add(Entry).");
                                }
                            }
                        }

                        return true;
                    }
                    else
                    {
                        Logging.Error(this.GetType(), "Entry can't be added, TryAdd(MessageID, Entry) returned false.");
                    }
                }
            }
            else
            {
                Logging.Error(this.GetType(), "Entry was null at Add(Entry).");
            }

            return false;
        }

        /// <summary>
        /// Removes the specified entry.
        /// </summary>
        /// <param name="Entry">The entry.</param>
        internal bool TryRemove(IEntry Entry)
        {
            if (Entry != null)
            {
                if (this.Entries.ContainsKey(Entry.MessageID))
                {
                    IEntry TmpEntry;

                    if (this.Entries.TryRemove(Entry.MessageID, out TmpEntry))
                    {
                        if (TmpEntry.Equals(Entry))
                        {
                            foreach (Member Member in this.Clan.Members.Entries.Values.ToList())
                            {
                                if (Member.Connected)
                                {
                                    Player Player = Member.Player;

                                    if (Player != null)
                                    {
                                        if (Player.Device.Connected)
                                        {
                                            // new Clan_Stream_Entry_Removed(Player.Device, Entry).Send();
                                        }
                                    }
                                    else
                                    {
                                        Logging.Error(this.GetType(), "Entry failed to propagate, one (or more) player instance were null at the foreach for Remove(Entry).");
                                    }
                                }
                            }

                            return true;
                        }
                        else
                        {
                            Logging.Error(this.GetType(), "Entry has been removed but, TryRemove(MessageID, out Entry) returned an entry not equal to the one we are trying to remove.");
                        }
                    }
                    else
                    {
                        Logging.Error(this.GetType(), "Entry failed to remove, TryRemove(MessageID, out Entry) returned false.");
                    }
                }
                else
                {
                    Logging.Error(this.GetType(), "Entry wasn't in the list when Remove(Entry) has been called.");
                }
            }
            else
            {
                Logging.Error(this.GetType(), "Entry was null at Remove(Entry).");
            }

            return false;
        }

        internal byte[] ToBytes
        {
            get
            {
                List<IEntry> Messages   = this.Entries.Values.ToList();
                List<byte> Packet       = new List<byte>();

                Packet.AddVInt(Messages.Count);

                foreach (IEntry Entry in Messages)
                {
                    Packet.AddRange(Entry.ToBytes);
                }

                return Packet.ToArray();
            }
        }
        */
    }
}