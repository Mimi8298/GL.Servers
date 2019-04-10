namespace GL.Servers.CR.Files.Client
{
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;

    internal class BillingPackageData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="BillingPackageData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public BillingPackageData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // BillingPackageData.
        }

        /// <summary>
        /// Called when all instances has been loaded for initialized members in instance.
        /// </summary>
		internal override void LoadingFinished()
		{
	    	// LoadingFinished.
		}
	
        internal string TID
        {
            get; set;
        }

        internal bool Disabled
        {
            get; set;
        }

        internal bool ExistsApple
        {
            get; set;
        }

        internal bool ExistsAndroid
        {
            get; set;
        }

        internal bool ExistsKunlun
        {
            get; set;
        }

        internal bool ExistsJupiter
        {
            get; set;
        }

        internal int Diamonds
        {
            get; set;
        }

        internal int USD
        {
            get; set;
        }

        internal int RMB
        {
            get; set;
        }

        internal int Order
        {
            get; set;
        }

        internal string IconFile
        {
            get; set;
        }

        internal string JupiterID
        {
            get; set;
        }

        internal string StarterPackName
        {
            get; set;
        }

        internal bool IsRedPackage
        {
            get; set;
        }

        internal string RumblePackName
        {
            get; set;
        }

        internal string ChronosOfferName
        {
            get; set;
        }

        internal int RedeemMax
        {
            get; set;
        }

        internal int CampaignId
        {
            get; set;
        }

    }
}