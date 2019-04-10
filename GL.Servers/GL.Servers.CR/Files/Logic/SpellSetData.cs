namespace GL.Servers.CR.Files.Logic
{
	using System.Collections.Generic;
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;

	internal class SpellSetData : Data
    {
        internal SpellData[] SpellsData;

		/// <summary>
        /// Initializes a new instance of the <see cref="SpellSetData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public SpellSetData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // SpellSetData.
        }

        /// <summary>
        /// Called when all instances has been loaded for initialized members in instance.
        /// </summary>
		internal override void LoadingFinished()
		{
	    	this.SpellsData = new SpellData[this.Spells.Count];

		    for (int i = 0; i < this.Spells.Count; i++)
		    {
		        this.SpellsData[i] = CSV.Tables.GetSpellDataByName(this.Spells[i]);
		    }
		}
	
        internal List<string> Spells
        {
            get; set;
        }
    }
}