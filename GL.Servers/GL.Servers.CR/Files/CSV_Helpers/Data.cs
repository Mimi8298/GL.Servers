namespace GL.Servers.CR.Files.CSV_Helpers
{
    using System;
    using GL.Servers.CR.Core;
    using GL.Servers.Files.CSV_Data;
    using GL.Servers.Files.CSV_Reader;
    using Newtonsoft.Json;

    [JsonConverter(typeof(DataConverter))]
    internal class Data : IData
    {
        internal DataTable DataTable;

        internal readonly int Type;
        internal readonly int Instance;

        /// <summary>
        /// Global ID of this data.
        /// </summary>
        public int GlobalID
        {
            get;
            set;
        }

        /// <summary>
        /// Name of this Data.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Data"/> class.
        /// </summary>
        internal Data()
        {
            // Data.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Data"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        internal Data(Row Row, DataTable DataTable)
        {
            this.DataTable  = DataTable;
            this.Type       = DataTable.Index;
            this.Instance   = DataTable.Datas.Count;
            this.GlobalID   = DataTable.Datas.Count + 1000000 * DataTable.Index;

            Row.LoadData(this);
        }

        /// <summary>
        /// Called when all instances has been loaded for initialized members in instance.
        /// </summary>
        internal virtual void LoadingFinished()
        {
            // LoadingFinished.
        }

        /// <summary>
        /// Called when all instances has been loaded for initialized members in instance.
        /// </summary>
        internal virtual void LoadingFinished2()
        {
            // LoadingFinished2.
        }
    }

    internal class DataConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Data Data = (Data)value;

            if (Data != null)
            {
                writer.WriteValue(Data.GlobalID);
            }
            else
                writer.WriteValue(0);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            int Id = (int)(long)reader.Value;

            if (Id != 0)
            {
                Data Data = CSV.Tables.GetWithGlobalID(Id);

                if (objectType == typeof(Data) || Data.GetType() == objectType)
                {
                    return Data;
                }
#if DEBUG
                Logging.Error(this.GetType(), "Data is not equals with objectType. Data:" + Data.GetType() + ", objectType:" + objectType + ".");
#endif
            }

            return null;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.BaseType == typeof(Data) || objectType == typeof(Data);
        }
    }
}