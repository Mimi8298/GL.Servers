namespace GL.Servers.CR.Logic.Items
{
    using System;
    using GL.Servers.CR.Extensions.Helper;
    using GL.Servers.CR.Files.CSV_Helpers;
    using GL.Servers.DataStream;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    [JsonConverter(typeof(DataSlotConverter))]
    internal class DataSlot
    {
        internal Data Data;
        internal int Count;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataSlot"/> class.
        /// </summary>
        internal DataSlot()
        {
            // DataSlot.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataSlot"/> class.
        /// </summary>
        internal DataSlot(Data Data, int Count)
        {
            this.Data = Data;
            this.Count = Count;
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal void Decode(ByteStream Packet)
        {
            this.Data = Packet.DecodeData();
            this.Count = Packet.ReadVInt();
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal void Encode(ByteStream Packet)
        {
            Packet.EncodeData(this.Data);
            Packet.AddVInt(this.Count);
        }

        /// <summary>
        /// Loads this instance from json.
        /// </summary>
        internal void Load(JToken Json)
        {
            JsonHelper.GetJsonData(Json, "id", out this.Data);
            JsonHelper.GetJsonNumber(Json, "cnt", out this.Count);
        }

        /// <summary>
        /// Saves this instance to json.
        /// </summary>
        internal JObject Save()
        {
            JObject Json = new JObject();

            if (this.Data != null)
            {
                Json.Add("id", this.Data.GlobalID);
            }

            Json.Add("cnt", this.Count);

            return Json;
        }
    }

    internal class DataSlotConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            DataSlot DataSlot = (DataSlot) value;

            if (DataSlot != null)
            {
                DataSlot.Save().WriteTo(writer);
            }
            else
                writer.WriteValue(0);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            DataSlot DataSlot = (DataSlot) existingValue;

            if (DataSlot == null)
            {
                DataSlot = new DataSlot();
            }

            DataSlot.Load(JObject.Load(reader));

            return DataSlot;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.BaseType == typeof(DataSlot) || objectType == typeof(DataSlot);
        }
    }
}