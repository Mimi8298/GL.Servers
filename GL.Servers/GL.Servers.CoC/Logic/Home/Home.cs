namespace GL.Servers.CoC.Logic
{
    using System;

    using GL.Servers.CoC.Files;
    using GL.Servers.CoC.Extensions.Helper;

    using GL.Servers.Extensions.List;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    [JsonConverter(typeof(HomeConverter))]
    internal class Home
    {
        internal Level Level;
        
        internal int HighID;
        internal int LowID;

        internal JToken LastSave;
        
        internal JToken HomeJSON
        {
            get
            {
                return this.Level != null ? this.Level.Save() : this.LastSave;
            }
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Home"/> class.
        /// </summary>
        public Home()
        {
            // Home.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Home"/> class.
        /// </summary>
        public Home(int HighID, int LowID)
        {
            this.HighID = HighID;
            this.LowID = LowID;
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal void Encode(ByteWriter Packet)
        {
            Packet.AddInt(this.HighID);
            Packet.AddInt(this.LowID);

            Packet.AddInt(0); // Shield
            Packet.AddInt(0); // Guard
            Packet.AddInt(365 * 86400);
            
            Packet.AddCompressableString(this.HomeJSON.ToString());
            Packet.AddCompressableString("{}");
            Packet.AddCompressableString("{}");
        }
        
        /// <summary>
        /// Loads this instance from json.
        /// </summary>
        internal void Load(JToken Json)
        {
            JsonHelper.GetJsonNumber(Json, "home_id_high", out this.HighID);
            JsonHelper.GetJsonNumber(Json, "home_id_low", out this.LowID);
            JsonHelper.GetJsonObject(Json, "level", out this.LastSave);
        }

        /// <summary>
        /// Saves this instance to json.
        /// </summary>
        internal JObject Save()
        {
            JObject Json = new JObject();

            Json.Add("home_id_high", this.HighID);
            Json.Add("home_id_low", this.LowID);
            Json.Add("level", this.HomeJSON);
            
            return Json;
        }
    }

    internal class HomeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Home Home = (Home) value;

            if (Home != null)
            {
                Home.Save().WriteTo(writer);
            }
            else
                LevelFile.StartingHome.WriteTo(writer);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Home Home = (Home) existingValue;

            if (Home != null)
            {
                Home.Load(JToken.Load(reader));
            }

            return Home;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Home);
        }
    }
}