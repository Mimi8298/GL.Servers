namespace GL.Servers.SP.Logic
{
    using Newtonsoft.Json.Linq;

    internal class Level
    {
        internal Puzzle Puzzle;

        /// <summary>
        /// Initializes a new instance of the <see cref="Level"/> class.
        /// </summary>
        public Level(Puzzle Puzzle)
        {
            this.Puzzle = Puzzle;
        }

        /// <summary>
        /// Loads this instance from json.
        /// </summary>
        internal void Load(JToken Json)
        {
            
        }

        internal JObject Save()
        {
            JObject Json = new JObject();

            Json.Add("monsters", new JArray());
            Json.Add("heroes", new JArray());

            Json.Add("targetiteration", 0);
            Json.Add("enemyturnstate", 0);
            Json.Add("waveturnno", 0);
            Json.Add("hswaps", 0);

            return Json;
        }
    }
}