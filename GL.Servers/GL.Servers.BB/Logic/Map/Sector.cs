namespace GL.Servers.BB.Logic.Map
{
    using GL.Servers.BB.Files;
    using GL.Servers.BB.Files.CSV_Logic;
    using GL.Servers.Extensions.List;
    using Newtonsoft.Json;

    internal class Sector
    {
        internal SectorData SectorData;
        internal PlayerMap PlayerMap;

        /// <summary>
        /// Returns the global id of SectorData.
        /// </summary>
        [JsonProperty]
        internal int Id
        {
            get
            {
                return this.SectorData != null ? this.SectorData.GlobalID : 0;
            }
            set
            {
                if (value != 0)
                {
                    this.SectorData = CSV.Tables.GetDataById(value) as SectorData;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sector"/> class.
        /// </summary>
        public Sector()
        {
            // Sector.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sector"/> class.
        /// </summary>
        public Sector(SectorData Data, PlayerMap PlayerMap) : this()
        {
            this.SectorData = Data;
            this.PlayerMap  = PlayerMap;
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        /// <param name="Packet"></param>
        internal void Encode(ByteWriter Packet)
        {
            Packet.AddInt(0); // Timer
        }

        /// <summary>
        /// Skips the specified time.
        /// </summary>
        /// <param name="seconds">The time in seconds.</param>
        internal void FastForwardTime(int seconds)
        {
            // FastForwardTime.
        }
    }
}