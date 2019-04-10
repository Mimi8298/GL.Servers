namespace GL.Servers.SP.Logic.Mode
{
    using GL.Servers.SP.Core;
    using GL.Servers.SP.Logic;
    using GL.Servers.SP.Logic.Manager;
    using GL.Servers.SP.Logic.Mode.Enums;
    using Newtonsoft.Json.Linq;

    internal class GameMode
    {
        internal Device Device;
        internal Random Random;

        internal Player Player;
        internal Office Office;
        internal Puzzle Puzzle;
        internal CityMap CityMap;
        internal EnergyTimer EnergyTimer;

        internal Time Time;
        internal State State;

        internal CommandManager CommandManager;

        internal int Timestamp;

        /// <summary>
        /// Gets a value indicating whether the device is connected.
        /// </summary>
        internal bool Connected
        {
            get
            {
                if (this.Device != null)
                {
                    return this.Device.Connected;
                }

                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating the checksum of this instance.
        /// </summary>
        internal int Checksum
        {
            get
            {
                int Checksum = this.Time.ClientSubTick;

                if (this.Player != null)
                {
                    Checksum += this.Player.Checksum;
                }

                if (this.CityMap != null)
                {
                    Checksum += this.CityMap.Checksum;
                }

                return this.Office.Checksum + Checksum;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameMode"/> class.
        /// </summary>
        public GameMode(Device Device)
        {
            this.Device = Device;
            this.Time   = new Time();
            this.Random = new Random();
            this.Office = new Office();
            this.CityMap = new CityMap(this);
            this.Puzzle = new Puzzle(this);
            this.EnergyTimer = new EnergyTimer(this);
        }

        /// <summary>
        /// Creates a fast forward the time.
        /// </summary>
        internal void FastForwardTime(int Seconds)
        {
            this.Office.FastForwardTime(Seconds);
            this.EnergyTimer.FastForwardTime(Seconds);
        }

        /// <summary>
        /// Loads the game.
        /// </summary>
        internal void LoadGame(int Seed, int SecondsSinceLastSave)
        {
            if (this.State <= 0)
            {
                this.Random = new Random(Seed);
                this.LoadHomeJson(this.Player.Home.LastSave);
                this.FastForwardTime(SecondsSinceLastSave);
            }
        }

        /// <summary>
        /// Loads home json.
        /// </summary>
        internal void LoadHomeJson(JToken Json)
        {
            this.CityMap.Load(Json["map"]);
            this.Office.Load(Json["office"]);
            this.Puzzle.Load(Json["puzzle"]);
            this.EnergyTimer.Load(Json["lifeTimer"]);
        }

        /// <summary>
        /// Sets the player.
        /// </summary>
        internal void SetPlayer(Player Player)
        {
            this.Player = Player;
            this.Player.GameMode = this;
        }

        /// <summary>
        /// Ticks this instance.
        /// </summary>
        internal void Tick()
        {
            this.Office.Tick();
            this.EnergyTimer.Tick();
        }

        /// <summary>
        /// Saves this instance to json.
        /// </summary>
        internal JObject Save()
        {
            JObject Json = new JObject();

            Json.Add("map", this.CityMap.Save());
            Json.Add("office", this.Office.Save());
            Json.Add("puzzle", this.Puzzle.Save());
            Json.Add("lifeTimer", this.EnergyTimer.Save());

            return Json;
        }
    }
}