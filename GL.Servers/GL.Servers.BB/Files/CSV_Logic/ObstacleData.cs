namespace GL.Servers.BB.Files.CSV_Logic
{
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.BB.Files.Enums;
    using GL.Servers.Files.CSV_Reader;

    internal class ObstacleData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="ObstacleData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public ObstacleData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string TID
        {
            get; set;
        }

        public string TIDIce
        {
            get; set;
        }

        public string TIDFire
        {
            get; set;
        }

        public string SWF
        {
            get; set;
        }

        public string ExportName
        {
            get; set;
        }

        public string ExportNameIce
        {
            get; set;
        }

        public string ExportNameFire
        {
            get; set;
        }

        public string ExportNameBase
        {
            get; set;
        }

        public int ClearTimeSeconds
        {
            get; set;
        }

        public int Width
        {
            get; set;
        }

        public int Height
        {
            get; set;
        }

        public bool Passable
        {
            get; set;
        }

        public string ClearCost
        {
            get; set;
        }

        public string LootResource
        {
            get; set;
        }

        public int LootCount
        {
            get; set;
        }

        public string ClearEffect
        {
            get; set;
        }

        public string ClearEffectIce
        {
            get; set;
        }

        public string ClearEffectFire
        {
            get; set;
        }

        public string PickUpEffect
        {
            get; set;
        }

        public string PickUpEffectIce
        {
            get; set;
        }

        public string PickUpEffectFire
        {
            get; set;
        }

        public int RespawnWeight
        {
            get; set;
        }

        public string DepositResource
        {
            get; set;
        }

        public int RequiredTownHallLevel
        {
            get; set;
        }

        internal ResourceData LottResourceData
        {
            get
            {
                return (ResourceData) CSV.Tables.Get(Gamefile.Resource).GetData(this.LootResource);
            }
        }
    }
}
