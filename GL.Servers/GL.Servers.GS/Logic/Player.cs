using GL.Servers.Extensions.List;

namespace GL.Servers.GS.Logic
{
    using System;
    using System.Collections.Generic;
    
    using Newtonsoft.Json;

    internal class Player
    {
        internal Device Device;

        [JsonProperty("home")]          internal Objects Objects;

        [JsonProperty("acc_hi")]        internal int HighID;
        [JsonProperty("acc_lo")]        internal int LowID;
        
        [JsonProperty("token")]         internal string Token;

        [JsonProperty("password")]      internal string Password;
        [JsonProperty("ban_reason")]    internal string BanReason;

        [JsonProperty("name")]          internal string Name;
        [JsonProperty("region")]        internal string Region;

        [JsonProperty("creation")]      internal DateTime Creation;
        [JsonProperty("ban_time")]      internal DateTime BanTime;

        /// <summary>
        /// Gets the player identifier.
        /// </summary>
        internal long PlayerID
        {
            get
            {
                return (long) this.HighID << 32 | (long)(uint) this.LowID;
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
        /// Gets this instance generated checksum.
        /// </summary>
        internal uint Checksum
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        internal Player()
        {
            this.Objects        = new Objects(this);
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

                {
                    {
                        Packet.AddInt(0);
                        Packet.AddInt(0);

                        Packet.AddInt(0); // Array
                    }
                    {
                        Packet.AddInt(0); // Array
                    }
                    {
                        Packet.AddInt(0); // Array
                    }
                    { // Attributes
                        Packet.AddInt(1); // ExpLevel
                        Packet.AddInt(1); // Alive

                        // if (Alive <= 0) Packet.AddBool(true);

                        Packet.AddInt(0);
                    }
                }

                Packet.AddInt(this.HighID);
                Packet.AddInt(this.LowID);

                Packet.AddString(this.Name);
                Packet.AddString("Unknown1");

                {
                    Packet.AddInt(0);
                } // UnknownClass (array)

                Packet.AddInt(0);
                Packet.AddBool(false); // dailyRewardCollected
                Packet.AddInt(0);

                {
                    Packet.AddInt(0); // MissionArray
                    {
                        Packet.AddInt(0);
                    }
                    Packet.AddInt(0);
                }
                {
                    Packet.AddInt(0);
                }
                {
                    Packet.AddInt(0);
                }

                for (int i = 0; i < 40; i++)
                {
                    Packet.Add(1);
                }

                return Packet.ToArray();
            }
        }
        
        /// <summary>
        /// Updates this instance.
        /// </summary>
        internal void Tick(int Time)
        {
            this.Objects.Update(Time);
            this.Objects.Timestamp += Time;
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
