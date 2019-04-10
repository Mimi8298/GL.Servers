namespace GL.Servers.BB.Logic
{
    using System.IO;
    using GL.Servers.BB.Core;
    using GL.Servers.BB.Extensions.Converter;
    using GL.Servers.BB.Extensions.Helper;
    using GL.Servers.BB.Logic.Manager;

    using GL.Servers.Extensions.List;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    [JsonConverter(typeof(HomeConverter))]
    internal class Home
    {
        internal Time Time;
        internal Player Player;

        internal Random Random;

        internal int HighID;
        internal int LowID;

        internal string HomeBaseLevel;
        internal string LevelAuthorName;

        internal WorkerManager WorkerManager;
        internal GameObjectManager GameObjectManager;

        internal Level Level
        {
            get
            {
                return this.Player?.Level;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Home"/> class.
        /// </summary>
        internal Home()
        {
            this.HomeBaseLevel     = Resources.GameSettings.HomeBaseLevelFile;

            this.Time              = new Time();
            this.WorkerManager     = new WorkerManager();
            this.GameObjectManager = new GameObjectManager(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Home"/> class.
        /// </summary>
        /// <param name="Player">The player.</param>
        internal Home(Player Player, int HighID, int LowID) : this()
        {
            this.Player     = Player;
            this.HighID     = HighID;
            this.LowID      = LowID;

            this.Random     = new Random(Resources.Random.Next());
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        /// <param name="Packet">The byte stream.</param>
        internal void Encode(ByteWriter Packet)
        {
            Packet.AddLong(this.HighID, this.LowID);

            Packet.AddString(this.HomeBaseLevel);
            Packet.AddString(this.LevelAuthorName);

            Packet.AddCompressableString(this.Save().ToString(Formatting.None));
        }

        /// <summary>
        /// Skips specified seconds.
        /// </summary>
        /// <param name="seconds">The seconds.</param>
        internal void FastForwardTime(int seconds)
        {
            if (seconds >= 0)
            {
                this.GameObjectManager.FastForwardTime(seconds);
            }
        }

        /// <summary>
        /// Loads <see cref="Home"/> from JSON.
        /// </summary>
        /// <param name="Json">The Json string.</param>
        internal void Load(string json)
        {
            using (StringReader StringReader = new StringReader(json))
            {
                this.Load(JToken.Load(new JsonTextReader(StringReader)));
            }
        }

        /// <summary>
        /// Loads <see cref="Home"/> from JSON.
        /// </summary>
        /// <param name="Token">The Json object.</param>
        internal void Load(JToken Token, bool isSave = false)
        {
            if (isSave)
            {
                JsonHelper.GetInt(Token["HighID"], out this.HighID);
                JsonHelper.GetInt(Token["LowID"], out this.LowID);

                JToken HomeBaseLevelToken = Token["HomeBaseLevel"];

                if (HomeBaseLevelToken != null)
                {
                    this.HomeBaseLevel = (string) HomeBaseLevelToken;
                }

                JsonHelper.GetString(Token["LevelAuthorName"], out this.LevelAuthorName);
            }

            this.GameObjectManager.Load(Token);
        }

        /// <summary>
        /// Saves <see cref="Home"/> to JSON.
        /// </summary>
        /// <returns>The Json object.</returns>
        internal JObject Save(bool isSave = false)
        {
            JObject Json = new JObject();

            if (isSave)
            {
                Json.Add("HighID", this.HighID);
                Json.Add("LowID", this.LowID);
                Json.Add("HomeBaseLevel", this.HomeBaseLevel);
                Json.Add("LevelAuthorName", this.LevelAuthorName);
            }

            this.GameObjectManager.Save(Json);

            Json.Add("map_spawn_timer", 0);
            Json.Add("deepsea_spawn_timer", 0);
            Json.Add("map_unliberation_timer", 0);
            Json.Add("upgrade_outpost_defenses", 0);
            Json.Add("seed", this.Random.Seed);

            Json.Add("layout_names", new JArray());
            Json.Add("troop_preset_names", new JArray());

            return Json;
        }

        /// <summary>
        /// Ticks this instance.
        /// </summary>
        internal void Tick()
        {
            this.GameObjectManager.Tick();
        }
    }
}
