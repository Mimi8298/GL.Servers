namespace GL.Servers.BS.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using GL.Servers.BS.Core;
    using GL.Servers.BS.Logic.Slots;
    using GL.Servers.BS.Logic.Slots.Items;

    using GL.Servers.Extensions.List;
    using GL.Servers.Logic.Enums;

    using Newtonsoft.Json;

    using Resources = GL.Servers.BS.Logic.Slots.Resources;

    internal class Player
    {
        internal Device Device;
        internal Battle Battle;

        [JsonProperty("acc_hi")]        internal int HighID;
        [JsonProperty("acc_lo")]        internal int LowID;

        [JsonProperty("alli_hi")]       internal int ClanHighID;
        [JsonProperty("alli_lo")]       internal int ClanLowID;

        [JsonProperty("battle_hi")]     internal int BattleHighID;
        [JsonProperty("battle_lo")]     internal int BattleLowID;

        [JsonProperty("token")]         internal string Token;
        [JsonProperty("password")]      internal string Password;

        [JsonProperty("name")]          internal string Name;
        [JsonProperty("region")]        internal string Region;

        [JsonProperty("level")]         internal int Level          = 1;
        [JsonProperty("diamonds")]      internal int Diamonds       = 10000;

        [JsonProperty("wins")]          internal int Wins;
        [JsonProperty("loses")]         internal int Loses;
        [JsonProperty("games")]         internal int Games;

        [JsonProperty("rank")]          internal Rank Rank          = Rank.Administrator;

        [JsonProperty("resources")]     internal Resources Resources;
        [JsonProperty("deck")]          internal Deck Deck;
        [JsonProperty("player_info")]   internal PlayerInfo Info;
        [JsonProperty("objects")]       internal Objects Objects;
        [JsonProperty("facebook")]      internal Facebook Facebook;
        [JsonProperty("google")]        internal Google Google;
        [JsonProperty("gamecenter")]    internal Gamecenter Gamecenter;

        [JsonProperty("update_date")]   internal DateTime Update    = DateTime.UtcNow;
        [JsonProperty("creation_date")] internal DateTime Created   = DateTime.UtcNow;
        [JsonProperty("ban_date")]      internal DateTime BanTime   = DateTime.UtcNow;

        /// <summary>
        /// Gets the player identifier.
        /// </summary>
        internal long PlayerID
        {
            get
            {
                return (long) this.HighID << 32 | (uint) this.LowID;
            }
        }
        
        /// <summary>
        /// Gets the clan identifier.
        /// </summary>
        internal long ClanID
        {
            get
            {
                return (long) this.ClanHighID << 32 | (uint) this.ClanLowID;
            }
        }
        
        /// <summary>
        /// Gets the battle identifier.
        /// </summary>
        internal long BattleID
        {
            get
            {
                return (long) this.BattleHighID << 32 | (uint) this.BattleLowID;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Player"/> is banned.
        /// </summary>
        internal bool Banned
        {
            get
            {
                return this.BanTime > DateTime.UtcNow;
            }
        }

        /// <summary>
        /// Gets the player's clan.
        /// </summary>
        internal Clan Clan
        {
            get
            {
                return Core.Resources.Clans.Get(this.ClanHighID, this.ClanLowID, Store: false);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        internal Player()
        {
            this.Info           = new PlayerInfo(this);
            this.Objects        = new Objects(this);
            this.Resources      = new Resources(this);
            this.Deck           = new Deck(this);

            this.Facebook       = new Facebook(this);
            this.Google         = new Google(this);
            this.Gamecenter     = new Gamecenter(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="HighID">The high identifier.</param>
        /// <param name="LowID">The low identifier.</param>
        internal Player(Device Device, int HighID, int LowID) : this()
        {
            this.Device         = Device;
            this.HighID         = HighID;
            this.LowID          = LowID;;
        }

        internal byte[] ToBytes
        {
            get
            {
                List<byte> Packet = new List<byte>();

                Packet.AddVInt(this.LowID, this.HighID);
                Packet.AddVInt(this.LowID, this.HighID);
                Packet.AddVInt(this.LowID, this.HighID);

                Packet.AddString(this.Name);
                Packet.AddBool(!string.IsNullOrEmpty(this.Name));

                Packet.AddInt(-1);

                Packet.AddVInt(5);  // Commodity Array Count (fixe)
                {
                    {
                        Packet.AddVInt(this.Resources.Count + this.Deck.Values.ToArray().Sum(T => T.Competences.Count));

                        foreach (Card Card in this.Deck.Values.ToArray())
                        {
                            Packet.AddRange(Card.Competences.ToBytes);
                        }

                        Packet.AddRange(this.Resources.ToBytes);
                    }
                    {
                        Packet.AddVInt(this.Deck.Count);

                        foreach (Card Card in this.Deck.Values.ToArray())
                        {
                            Packet.AddVInt(Card.Type);
                            Packet.AddVInt(Card.Identifier);
                            Packet.AddVInt(Card.Trophies);
                        }
                    }
                    {
                        Packet.AddVInt(this.Deck.Count);

                        foreach (Card Card in this.Deck.Values.ToArray())
                        {
                            Packet.AddVInt(Card.Type);
                            Packet.AddVInt(Card.Identifier);
                            Packet.AddVInt(Card.Trophies);
                        }
                    }
                    {
                        Packet.AddVInt(this.Resources.Count);
                        Packet.AddRange(this.Resources.ToBytes);
                    }
                    {
                        Packet.AddVInt(0);
                    }
                }

                Packet.AddVInt(this.Diamonds);
                Packet.AddVInt(this.Diamonds); // Free Diamonds

                Packet.AddVInt(9);
                Packet.AddVInt(0);
                Packet.AddVInt(0);
                Packet.AddVInt(0);
                Packet.AddVInt(0);
                Packet.AddVInt(0);
                Packet.AddVInt(0);
                Packet.AddVInt(0);

                Packet.AddVInt(2);

                return Packet.ToArray();
            }
        }

        /// <summary>
        /// Ticks this instance.
        /// </summary>
        internal void Tick()
        {
            if (this.Info.Trophies > this.Info.HighTrophies)
            {
                this.Info.HighTrophies = this.Info.Trophies;
            }

            if (this.ClanLowID > 0)
            {
                Clan Clan = Core.Resources.Clans.Get(this.ClanHighID, this.ClanLowID, Constants.Database, false);

                if (Clan != null)
                {
                    Member Member;

                    if (Clan.Members.Entries.TryGetValue(this.PlayerID, out Member))
                    {
                        Member.Update(this);
                    }
                    else
                    {
                        Logging.Error(this.GetType(), "Failed to update the member instance, TryGetValue(PlayerID, out Member) returned false.");
                    }
                }
                else
                {
                    Logging.Error(this.GetType(), "Clan was null when Tick() has been called.");
                }
            }

            this.Objects.Tick();
        }
        
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.HighID + "-" + this.LowID;
        }
    }
}
