namespace GL.Servers.CoC.Logic.Clan.Slots
{
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Threading;
    using GL.Servers.CoC.Core.Network;
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Logic.Clan.StreamEntry;
    using GL.Servers.CoC.Packets.Messages.Server.Alliance;
    using Newtonsoft.Json;

    internal class Streams
    {
        internal Alliance Alliance;

        [JsonProperty] internal int Seed;
        [JsonProperty] internal ConcurrentDictionary<long, StreamEntry> Slots;

        public Streams()
        {
            this.Slots = new ConcurrentDictionary<long, StreamEntry>();
        }

        public Streams(Alliance Alliance) : this()
        {
            this.Alliance = Alliance;
        }

        internal void AddEntry(StreamEntry StreamEntry)
        {
            StreamEntry.LowId = Interlocked.Increment(ref this.Seed);

            if (this.Slots.TryAdd(StreamEntry.StreamID, StreamEntry))
            {
                if (this.Slots.Count > 50)
                {
                    if (this.Slots.TryRemove(this.Slots.Keys.First(), out StreamEntry Removed))
                    {
                        this.RemoveEntry(Removed);
                    }
                }

                foreach (Player Player in this.Alliance.Members.Connected.Values.ToArray())
                {
                    if (Player.Connected)
                    {
                        new Alliance_Stream_Entry_Message(Player.Level.GameMode.Device, StreamEntry).Send();
                    }
                    else
                        this.Alliance.Members.Connected.TryRemove(Player.PlayerID, out _);
                }
            }
        }

        internal void RemoveEntry(StreamEntry StreamEntry)
        {
            foreach (Player Player in this.Alliance.Members.Connected.Values.ToArray())
            {
                if (Player.Connected)
                {
                    new Alliance_Stream_Entry_Removed_Message(Player.Level.GameMode.Device, StreamEntry).Send();
                }
                else
                    this.Alliance.Members.Connected.TryRemove(Player.PlayerID, out _);
            }
        }
    }
}