namespace GL.Servers.CoC.Logic.Clan.Slots
{
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Threading;
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Logic.Clan.Items;
    using GL.Servers.Extensions.List;
    using Newtonsoft.Json;

    internal class Members
    {
        internal Alliance Alliance;
        
        [JsonProperty] internal ConcurrentDictionary<long, Member> Slots;
        internal ConcurrentDictionary<long, Player> Connected;

        
        public Members()
        {
            this.Slots     = new ConcurrentDictionary<long, Member>();
            this.Connected = new ConcurrentDictionary<long, Player>();
        }

        public Members(Alliance Alliance) : this()
        {
            this.Alliance = Alliance;
        }

        internal bool Join(Player Player, out Member Member)
        {
            if (this.Alliance.Header.NumberOfMembers < 50)
            {
                int Count = Interlocked.Increment(ref this.Alliance.Header.NumberOfMembers);

                if (Count <= 50)
                {
                    Member = new Member(Player);

                    if (this.Slots.TryAdd(Player.PlayerID, Member))
                    {
                        if (Player.Connected)
                        {
                            this.Connected.TryAdd(Player.PlayerID, Player);
                        }

                        return true;
                    }
                }

                this.Alliance.Header.NumberOfMembers--;
            }

            Member = null;

            return false;
        }

        internal bool Quit(long Player)
        {
            if (this.Slots.TryRemove(Player, out _))
            {
                this.Connected.TryRemove(Player, out _);
                return true;
            }

            return false;
        }

        internal bool Quit(Player Player)
        {
            return this.Quit(Player.PlayerID);
        }

        internal void Encode(ByteWriter Packet)
        {
            Member[] Items = this.Slots.Values.ToArray();

            Packet.AddInt(Items.Length);

            for (int i = 0; i < Items.Length; i++)
            {
                Items[i].Encode(Packet);
            }
        }
    }
}