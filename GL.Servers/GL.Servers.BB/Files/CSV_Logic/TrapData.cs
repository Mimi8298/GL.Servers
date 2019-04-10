namespace GL.Servers.BB.Files.CSV_Logic
{
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.Files.CSV_Reader;

    internal class TrapData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="TrapData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public TrapData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }
        
        public string TID
        {
            get; set;
        }

        public string InfoTID
        {
            get; set;
        }

        public string SubtitleTID
        {
            get; set;
        }

        public int DefenseValue
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

        public string IconSWF
        {
            get; set;
        }

        public string BigPictureSWF
        {
            get; set;
        }

        public string BigPicture
        {
            get; set;
        }

        public int UpgradeTimeH
        {
            get; set;
        }

        public string UpgradeCost
        {
            get; set;
        }

        public int XpGain
        {
            get; set;
        }

        public int UpgradeHouseLevel
        {
            get; set;
        }

        public int Damage
        {
            get; set;
        }

        public int DamageRadius
        {
            get; set;
        }

        public int TriggerRadius
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

        public string Effect
        {
            get; set;
        }

        public string Effect2
        {
            get; set;
        }

        public string DamageEffect
        {
            get; set;
        }

        public bool Passable
        {
            get; set;
        }

        public string BuildCost
        {
            get; set;
        }

        public string ExportNameTriggered
        {
            get; set;
        }

        public int ActionFrame
        {
            get; set;
        }

        public string PickUpEffect
        {
            get; set;
        }

        public string PlacingEffect
        {
            get; set;
        }

        public string AppearEffect
        {
            get; set;
        }

        public bool CountersArmored
        {
            get; set;
        }

        public int StunTimeMS
        {
            get; set;
        }
    }
}
