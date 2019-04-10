namespace GL.Servers.CoC.Files.CSV_Logic.Logic
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.CoC.Files.CSV_Helpers;

    internal class BuildingClassData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="BuildingClassData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public BuildingClassData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // BuildingClassData.
        }

        public string TID
        {
            get; set;
        }

        public bool CanBuy
        {
            get; set;
        }

        public bool ShopCategoryResource
        {
            get; set;
        }

        public bool ShopCategoryArmy
        {
            get; set;
        }

        public bool ShopCategoryDefense
        {
            get; set;
        }
    }
}
