namespace GL.Servers.CR.Logic.Slots
{
    using System.Linq;
    using System.Threading;
    using System.Collections.Generic;

    using GL.Servers.CR.Core;
    using GL.Servers.CR.Logic.Stream;

    using Newtonsoft.Json;

    internal class AllianceStreamEntries
    {
        internal Alliance Alliance;

        [JsonProperty] private int Seed;
        [JsonProperty] private Dictionary<long, StreamEntry> Slots;

        /// <summary>
        /// Initializes a new instance of the <see cref="Messages"/> class.
        /// </summary>
        internal AllianceStreamEntries()
        {
            this.Slots = new Dictionary<long, StreamEntry>(50);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Messages"/> class.
        /// </summary>
        /// <param name="Clan">The clan.</param>
        internal AllianceStreamEntries(Alliance Alliance) : this()
        {
            this.Alliance = Alliance;
        }

        /// <summary>
        /// Add a new entry in the collection.
        /// </summary>
        internal void AddEntry(StreamEntry Entry)
        {
            Entry.HighID = Constants.ServerID;
            Entry.LowID = Interlocked.Increment(ref this.Seed);

            if (this.Slots.Count > 50)
            {
                this.RemoveEntry(this.Slots.Values.First());
            }

            this.Slots.Add(Entry.StreamID, Entry);

            foreach (Player Player in this.Alliance.Members.Connected.Values.ToArray())
            {
                if (Player.Connected)
                {
                    // TODO Implement AllianceStreamEntryMessage().
                }
            }
        }

        /// <summary>
        /// Remove the specified entry of the collection.
        /// </summary>
        internal void RemoveEntry(StreamEntry Entry)
        {
            if (!Entry.Removed)
            {
                if (this.Slots.Remove(Entry.StreamID))
                {
                    Entry.Removed = true;

                    foreach (Player Player in this.Alliance.Members.Connected.Values.ToArray())
                    {
                        if (Player.Connected)
                        {
                            // TODO Implement AllianceStreamEntryRemovedMessage().
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Updates the specified entry.
        /// </summary>
        internal void UpdateEntry(StreamEntry Entry)
        {
            if (this.Slots.ContainsKey(Entry.StreamID))
            {
                foreach (Player Player in this.Alliance.Members.Connected.Values.ToArray())
                {
                    if (Player.Connected)
                    {
                        // TODO Implement AllianceStreamEntryMessage().
                    }
                }
            }
        }
    }
}