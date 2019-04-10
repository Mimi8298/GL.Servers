namespace GL.Servers.CR.Files.Logic
{
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;

    internal class AbilityData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="AbilityData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public AbilityData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // AbilityData.
        }

        /// <summary>
        /// Called when all instances has been loaded for initialized members in instance.
        /// </summary>
		internal override void LoadingFinished()
		{
	    	// LoadingFinished.
		}
	
        internal string IconFile
        {
            get; set;
        }

        internal string TID
        {
            get; set;
        }

        internal string AreaEffectObject
        {
            get; set;
        }

        internal string Buff
        {
            get; set;
        }

        internal int BuffTime
        {
            get; set;
        }

        internal string Effect
        {
            get; set;
        }

    }
}