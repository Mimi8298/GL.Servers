namespace GL.Servers.CR.Files.Logic
{
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;

    internal class EventCategoryDefinitionData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="EventCategoryDefinitionData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public EventCategoryDefinitionData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // EventCategoryDefinitionData.
        }

        /// <summary>
        /// Called when all instances has been loaded for initialized members in instance.
        /// </summary>
		internal override void LoadingFinished()
		{
	    	// LoadingFinished.
		}
	
        internal string ObjectType
        {
            get; set;
        }

    }
}