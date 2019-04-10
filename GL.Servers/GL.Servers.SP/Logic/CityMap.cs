namespace GL.Servers.SP.Logic
{
    using GL.Servers.SP.Extensions.Helper;
    using GL.Servers.SP.Files.CSV_Helpers;
    using GL.Servers.SP.Logic.Mode;
    using Newtonsoft.Json.Linq;

    internal class CityMap
    {
        internal GameMode GameMode;
        internal Timer Timer;

        /// <summary>
        /// Gets a value indicating the checksum of this instance.
        /// </summary>
        internal int Checksum
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CityMap"/> class.
        /// </summary>
        internal CityMap(GameMode GameMode)
        {
            this.GameMode = GameMode;
            this.Timer    = new Timer();
        }

        /// <summary>
        /// Creates a fast forward the time.
        /// </summary>
        internal void FastForwardTime(int Seconds)
        {

        }

        /// <summary>
        /// Ticks this instance.
        /// </summary>
        internal void Tick()
        {
            
        }

        /// <summary>
        /// Loads this instance from json.
        /// </summary>
        internal void Load(JToken Json)
        {
            if (Json != null)
            {
                if (JsonHelper.GetJsonObject(Json, "gtimer", out JToken GTimer))
                {
                    this.Timer.Load(GTimer);
                }

                if (JsonHelper.GetJsonData(Json, "agd", out Data Data))
                {

                }
            }
        }

        /// <summary>
        /// Saves this instance to json.
        /// </summary>
        internal JObject Save()
        {
            JObject Json = new JObject();

            Json.Add("gtimer", this.Timer.Save(this.GameMode.Time));
            Json.Add("agd", 0);

            return Json;
        }
    }
}