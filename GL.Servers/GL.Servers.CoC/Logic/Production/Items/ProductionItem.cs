namespace GL.Servers.CoC.Logic.Production.Items
{
    using GL.Servers.CoC.Extensions.Helper;
    using GL.Servers.CoC.Files.CSV_Helpers;
    using GL.Servers.CoC.Logic.Items;

    using Newtonsoft.Json.Linq;

    internal class ProductionItem : Item
    {
        internal bool Terminate;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductionItem"/> class.
        /// </summary>
        public ProductionItem() : base()
        {
            // ProductionItem.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductionItem"/> class.
        /// </summary>
        public ProductionItem(Data Data, int Count) : base(Data, Count)
        {
            // ProductionItem.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductionItem"/> class.
        /// </summary>
        public ProductionItem(Data Data, int Count, bool Terminate) : base(Data, Count)
        {
            this.Terminate = Terminate;
        }

        /// <summary>
        /// Loads this instance from json.
        /// </summary>
        internal override void Load(JToken Json)
        {
            base.Load(Json);
            JsonHelper.GetJsonBoolean(Json, "t", out this.Terminate);
        }

        /// <summary>
        /// Saves this instance to json.
        /// </summary>
        internal override JObject Save()
        {
            JObject Json = base.Save();
            Json.Add("t", this.Terminate);
            return Json;
        }
    }
}