namespace GL.Servers.BS.Files.CSV_Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.BS.Files.CSV_Helpers;

    internal class Area_Effects : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="Area_Effects"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public Area_Effects(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string Name
        {
            get; set;
        }

        public string FileName
        {
            get; set;
        }

        public string BlueExportName
        {
            get; set;
        }

        public string RedExportName
        {
            get; set;
        }

        public string Layer
        {
            get; set;
        }

        public string Effect
        {
            get; set;
        }

        public int Scale
        {
            get; set;
        }

        public int TimeMs
        {
            get; set;
        }

        public int Radius
        {
            get; set;
        }

        public int Damage
        {
            get; set;
        }

        public int CustomValue
        {
            get; set;
        }

        public string Type
        {
            get; set;
        }

        public string BulletExplosionBullet
        {
            get; set;
        }

        public int BulletExplosionBulletDistance
        {
            get; set;
        }

        public bool DestroysEnvironment
        {
            get; set;
        }

        public int PushbackStrength
        {
            get; set;
        }

        public int PushbackStrengthSelf
        {
            get; set;
        }

        public int FreezeStrength
        {
            get; set;
        }
    }
}
