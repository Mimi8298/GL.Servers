namespace GL.Servers.CR.Files.Logic
{
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;

    internal class EventTargetingDefinitionData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="EventTargetingDefinitionData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public EventTargetingDefinitionData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // EventTargetingDefinitionData.
        }

        /// <summary>
        /// Called when all instances has been loaded for initialized members in instance.
        /// </summary>
		internal override void LoadingFinished()
		{
	    	// LoadingFinished.
		}
	
        internal string MetadataType
        {
            get; set;
        }

        internal string MetadataPath
        {
            get; set;
        }

        internal string EvaluationLocation
        {
            get; set;
        }

        internal string ParameterName
        {
            get; set;
        }

        internal string ParameterType
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

        internal string MatchingRuleType
        {
            get; set;
        }

    }
}