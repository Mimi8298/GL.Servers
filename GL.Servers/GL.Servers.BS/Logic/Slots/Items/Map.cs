namespace GL.Servers.BS.Logic.Slots.Items
{
    using System;
    using System.Collections.Generic;

    using GL.Servers.BS.Files;
    using GL.Servers.BS.Files.CSV_Helpers;
    using GL.Servers.BS.Files.CSV_Logic;
    using GL.Servers.BS.Logic.Enums;

    using GL.Servers.Extensions.List;

    using Newtonsoft.Json;

    using Maps      = GL.Servers.BS.Logic.Slots.Maps;
    using Resources = GL.Servers.BS.Core.Resources;

    internal class Map
    {
        [JsonProperty("maps")]              internal Maps Maps;
        [JsonProperty("location")]          internal Locations Location;
        [JsonProperty("updated")]           internal DateTime Updated;

        [JsonProperty("identifier")]        internal int Identifier;

        [JsonProperty("earnable_gold")]     internal int MaxGold;
        [JsonProperty("earned_gold")]       internal int EarnedGold;

        [JsonProperty("is_first_match")]    internal bool isFirstMatch  = true;
        [JsonProperty("is_double_xp")]      internal bool isDoubleXP    = true;

        internal int Booleans
        {
            get
            {
                int Value = 0;

                if (this.isFirstMatch)
                {
                    Value += 1;
                }

                if (this.isDoubleXP)
                {
                    Value += 2;
                }

                return Value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is expired.
        /// </summary>
        internal bool isExpired
        {
            get
            {
                return this.Updated.AddHours(24) <= DateTime.UtcNow;
            }
        }

        /// <summary>
        /// Gets the time left before this instance expire.
        /// </summary>
        internal uint TimeLeft
        {
            get
            {
                return !this.isExpired ? (uint) DateTime.UtcNow.AddHours(24).Subtract(this.Updated).TotalSeconds : 0;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Map"/> class.
        /// </summary>
        internal Map(bool Initialize = true)
        {
            this.Updated    = DateTime.UtcNow;

            if (Initialize)
            {
                this.Generate();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Map"/> class.
        /// </summary>
        /// <param name="Maps">The maps.</param>
        internal Map(Maps Maps, bool Initialize = true) : this(Initialize)
        {
            this.Maps       = Maps;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Map"/> class.
        /// </summary>
        /// <param name="Location">The location.</param>
        internal Map(Locations Location, bool Initialize = true) : this(Initialize)
        {
            this.Location   = Location;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Map"/> class.
        /// </summary>
        /// <param name="Maps">The maps.</param>
        /// <param name="Location">The location.</param>
        internal Map(Maps Maps, Locations Location, bool Initialize = true) : this(Initialize)
        {
            this.Maps       = Maps;
            this.Location   = Location;
        }

        /// <summary>
        /// Generates this instance.
        /// </summary>
        internal void Generate()
        {
            if (this.Location != null)
            {
                this.Identifier     = this.Location.GetID() + 1;

                if (this.MaxGold <= 0)
                {
                    this.MaxGold    = 9999;
                }
            }
            else
            {
                List<Data> Datas    = CSV.Tables.Get(Gamefile.Locations).Datas;
                int Index           = Resources.Random.Next(0, Datas.Count - 1);

                this.Location       = (Locations) Datas[Index];
                this.Identifier     = Index + 1;
                this.Updated        = DateTime.UtcNow;

                this.MaxGold        = 9999; 
            }
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        internal void Update()
        {
            if (this.isExpired)
            {
                this.Generate();
            }
        }

        internal byte[] ToBytes
        {
            get
            {
                List<byte> Packet = new List<byte>();

                Packet.AddVInt(this.Identifier);
                Packet.AddVInt(this.Maps.Entries.IndexOf(this) + 1);
                Packet.AddVInt(0);

                Packet.AddVInt(this.TimeLeft);

                Packet.AddVInt(0);
                Packet.AddVInt(0);

                Packet.AddVInt(this.MaxGold);

                Packet.AddBools(false, false);

                Packet.AddVInt(GlobalID.GetType(this.Location.GlobalID));
                Packet.AddVInt(GlobalID.GetID(this.Location.GlobalID));

                Packet.AddVInt(this.EarnedGold);
                Packet.AddVInt(0);

                Packet.AddString(null);

                Packet.AddVInt(0);

                return Packet.ToArray();
            }
        }
    }
}
