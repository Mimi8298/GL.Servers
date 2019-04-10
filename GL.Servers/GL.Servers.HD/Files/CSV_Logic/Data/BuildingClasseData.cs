namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class BuildingClasseData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="BuildingClasseData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public BuildingClasseData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
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
