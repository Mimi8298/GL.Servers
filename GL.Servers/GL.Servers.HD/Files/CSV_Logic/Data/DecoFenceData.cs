namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class DecoFenceData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="DecoFenceData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public DecoFenceData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);
        }

        public string ShopIconExportName
        {
            get; set;
        }

        public int UnlockLevel
        {
            get; set;
        }

        public int RepUnlockLevel
        {
            get; set;
        }

        public int ShopOrder
        {
            get; set;
        }

        public string FileName
        {
            get; set;
        }

        public string ExportName
        {
            get; set;
        }

        public string FenceBelowExportName
        {
            get; set;
        }

        public string FenceRightExportName
        {
            get; set;
        }

        public string FenceCornerExportName
        {
            get; set;
        }

        public int TileWidth
        {
            get; set;
        }

        public int TileHeight
        {
            get; set;
        }

        public int Price
        {
            get; set;
        }

        public int DiamondPrice
        {
            get; set;
        }

        public int PriceIncrease
        {
            get; set;
        }

        public int DoPriceIncreaseEveryNObject
        {
            get; set;
        }

        public string ShopDescriptionTID
        {
            get; set;
        }

        public string TID
        {
            get; set;
        }

        public string BuySound
        {
            get; set;
        }

        public string TapSound
        {
            get; set;
        }

        public bool CanBuy
        {
            get; set;
        }

        public bool SpinWheelPrize
        {
            get; set;
        }

        public int BuyExp
        {
            get; set;
        }

        public bool Passable
        {
            get; set;
        }

        public string Theme
        {
            get; set;
        }

        public bool TagSpecial
        {
            get; set;
        }

        public bool TagNature
        {
            get; set;
        }

        public bool TagDeco
        {
            get; set;
        }

        public bool TagSanctuary
        {
            get; set;
        }

        public bool HideFromLevelUpScreen
        {
            get; set;
        }

        public string SanctuaryAnimal
        {
            get; set;
        }
    }
}
