namespace GL.Servers.CR.Files.Client
{
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;

    internal class HealthBarData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="HealthBarData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public HealthBarData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // HealthBarData.
        }

        /// <summary>
        /// Called when all instances has been loaded for initialized members in instance.
        /// </summary>
		internal override void LoadingFinished()
		{
	    	// LoadingFinished.
		}
	
        internal string FileName
        {
            get; set;
        }

        internal string PlayerExportName
        {
            get; set;
        }

        internal string EnemyExportName
        {
            get; set;
        }

        internal string NoDamagePlayerExportName
        {
            get; set;
        }

        internal string NoDamageEnemyExportName
        {
            get; set;
        }

        internal int MinimumHitpointValue
        {
            get; set;
        }

        internal bool ShowOwnAlways
        {
            get; set;
        }

        internal bool ShowEnemyAlways
        {
            get; set;
        }

        internal int YOffset
        {
            get; set;
        }

        internal bool ShowAsShield
        {
            get; set;
        }

    }
}