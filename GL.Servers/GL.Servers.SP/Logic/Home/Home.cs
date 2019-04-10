namespace GL.Servers.SP.Logic
{
    using System;

    using GL.Servers.SP.Files;
    using GL.Servers.SP.Logic.Mode;
    using GL.Servers.SP.Extensions.Helper;

    using GL.Servers.Extensions.List;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    [JsonConverter(typeof(HomeConverter))]
    internal class Home
    {
        internal Player Player;
        
        internal int HighID;
        internal int LowID;

        internal JToken LastSave;

        internal GameMode GameMode
        {
            get
            {
                return this.Player.GameMode;
            }
        }

        /// <summary>
        /// Gets a value indicating the home json. Returns 'Home->GameMode::Save()' whether Home->GameMode is not null else returns 'Home->LastSave'.
        /// </summary>
        internal JToken HomeJSON
        {
            get
            {
                return this.GameMode != null ? this.GameMode.Save() : this.LastSave;
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
        public Home(Player Player) : this()
        {
            this.Player = Player;
        }
        
        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal void Encode(ByteWriter Packet)
        {
            Packet.AddLong(this.HighID, this.LowID);
            Packet.AddString(this.HomeJSON.ToString(Formatting.None));
        }
        
        /// <summary>
        /// Loads this instance from json.
        /// </summary>
        internal void Load(JToken Json)
        {
            JsonHelper.GetJsonNumber(Json, "home_id_high", out this.HighID);
            JsonHelper.GetJsonNumber(Json, "home_id_low", out this.LowID);
            JsonHelper.GetJsonObject(Json, "home", out this.LastSave);
        }

        /// <summary>
        /// Saves this instance to json.
        /// </summary>
        internal JObject Save()
        {
            JObject Json = new JObject();

            Json.Add("home_id_high", this.HighID);
            Json.Add("home_id_low", this.LowID);
            Json.Add("home", this.HomeJSON);
            
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