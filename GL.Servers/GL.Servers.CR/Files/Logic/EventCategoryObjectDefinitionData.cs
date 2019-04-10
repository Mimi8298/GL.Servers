namespace GL.Servers.CR.Files.Logic
{
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;

    internal class EventCategoryObjectDefinitionData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="EventCategoryObjectDefinitionData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public EventCategoryObjectDefinitionData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // EventCategoryObjectDefinitionData.
        }

        /// <summary>
        /// Called when all instances has been loaded for initialized members in instance.
        /// </summary>
		internal override void LoadingFinished()
		{
	    	// LoadingFinished.
		}
	
        internal string PropertyName
        {
            get; set;
        }

        internal string PropertyType
        {
            get; set;
        }

        internal bool IsRequired
        {
            get; set;
        }

        internal string ObjectType
        {
            get; set;
        }

        internal int DefaultInt
        {
            get; set;
        }

        internal string DefaultString
        {
            get; set;
        }

    }
}