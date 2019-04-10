namespace GL.Servers.CoC.Files.CSV_Helpers
{
    using System;
    using GL.Servers.CoC.Core;
    using GL.Servers.Files.CSV_Data;
    using GL.Servers.Files.CSV_Reader;

    using Newtonsoft.Json;

    internal class Data : IData
    {
        internal Row Row;
        internal DataTable DataTable;

        public int GlobalID
        {
            get;
            set;
        }

        public int InstanceID
        {
            get;
            set;
        }

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
            this.Row = Row;
            this.DataTable  = DataTable;
            this.InstanceID = DataTable.Datas.Count;
            this.GlobalID   = CSV_Helpers.GlobalID.Create(this.DataTable.Index, this.InstanceID);

            this.Load();
        }

        /// <summary>
        /// Loads this data.
        /// </summary>
        internal void Load()
        {
            this.Row.LoadData(this);
        }
        
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        internal int GetID()
        {
            return CSV_Helpers.GlobalID.GetID(this.GlobalID);
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        internal int GetDataType()
        {
            return this.DataTable.Index;
        }

        /// <summary>
        /// Intializes the class.
        /// </summary>
        internal virtual void LoadingFinished()
        {
            // LoadingFinished.
        }
    }

    internal class DataConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Data Data = (Data) value;

            if (Data != null)
            {
                writer.WriteValue(Data.GlobalID);
            }
            else 
                writer.WriteValue(0);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            int Id = (int) (long) reader.Value;

            if (Id != 0)
            {
                Data Data = CSV.Tables.GetWithGlobalID(Id);

                if (objectType == typeof(Data) || Data.GetType() == objectType)
                {
                    return Data;
                }
#if DEBUG
                Logging.Error(this.GetType(), "Data is not equals with objectType. Data:" + Data.GetType() + " objectType:" + objectType + ".");
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