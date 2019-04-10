namespace GL.Servers.SP.Logic
{
    using GL.Servers.SP.Logic.Mode;
    using Newtonsoft.Json.Linq;

    internal class Puzzle
    {
        internal GameMode GameMode;
        internal Random Random;
        internal Level Level;

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
        /// Initializes a new instance of the <see cref="Puzzle"/> class.
        /// </summary>
        internal Puzzle(GameMode GameMode)
        {
            this.GameMode = GameMode;
            this.Random   = new Random();
            this.Level    = new Level(this);
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
            
        }

        /// <summary>
        /// Saves this instance to json.
        /// </summary>
        internal JObject Save()
        {
            JObject Json = new JObject();

            Json.Add("stimer", new Timer().Save(this.GameMode.Time));
            Json.Add("turntimer", new Timer().Save(this.GameMode.Time));
            Json.Add("rtt", new Timer().Save(this.GameMode.Time));

            Json.Add("pstate", 0);
            Json.Add("state", 0);
            Json.Add("prevstate", 0);
            Json.Add("mdelay", 0);
            Json.Add("wave", 0);
            Json.Add("combo", 0);
            Json.Add("bt", 0);
            Json.Add("score", 0);
            Json.Add("endReason", 0);
            Json.Add("turncount", 0);
            Json.Add("rx0", 0);
            Json.Add("ry0", 0);
            Json.Add("rx1", 0);
            Json.Add("ry1", 0);
            Json.Add("tciw", 0);
            Json.Add("failcnt", 0);
            Json.Add("omocnt", 0);

            Json.Add("lrnd", 1);

            Json.Add("skipfirstturn", 0);
            Json.Add("tde", 0);

            Json.Add("buc", new JArray());
            Json.Add("blocks", new JArray());
            Json.Add("level", this.Level.Save());

            return Json;
        }
    }
}