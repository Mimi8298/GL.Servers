namespace GL.Servers.HD.Files.CSV_Logic.Data
{
	using GL.Servers.Files.CSV_Reader;
	using GL.Servers.HD.Files.CSV_Helpers;

    internal class DecorationData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="DecorationData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public DecorationData(Row Row, DataTable DataTable) : base(Row, DataTable)
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

        public string Vouchers
        {
            get; set;
        }

        public int VoucherCount
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

        public bool AttractsButterflies
        {
            get; set;
        }

        public bool BirdsLand
        {
            get; set;
        }

        public bool SpawnsBirds
        {
            get; set;
        }

        public bool SpawnsFrogs
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
