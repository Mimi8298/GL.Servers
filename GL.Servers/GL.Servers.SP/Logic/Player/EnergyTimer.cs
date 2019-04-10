namespace GL.Servers.SP.Logic
{
    using GL.Servers.SP.Extensions.Helper;
    using GL.Servers.SP.Logic.Mode;
    using Newtonsoft.Json.Linq;

    internal class EnergyTimer
    {
        internal GameMode GameMode;
        internal Timer Timer;

        /// <summary>
        /// Gets a value indicating the remaining seconds before the new energy.
        /// </summary>
        internal int RemainingSeconds
        {
            get
            {
                return this.Timer.GetRemainingSeconds(this.GameMode.Time);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnergyTimer"/> class.
        /// </summary>
        /// <param name="GameMode"></param>
        public EnergyTimer(GameMode GameMode)
        {
            this.GameMode = GameMode;
            this.Timer = new Timer();
        }

        /// <summary>
        /// Creates a fast forward the time.
        /// </summary>
        internal void FastForwardTime(int Seconds)
        {
            Player Player = this.GameMode.Player;

            if (Player != null)
            {
                while (!Player.HasMaxEnergy)
                {
                    if (this.Timer.GetRemainingSeconds(this.GameMode.Time) <= 0)
                    {
                        ++Player.Energy;
                        this.Timer.IncreaseTimer(1);
                    }
                    else
                        break;
                }

                if (Player.HasMaxEnergy)
                {
                    this.Timer.StopTimer();
                }
            }
        }

        /// <summary>
        /// Ticks this instance.
        /// </summary>
        internal void Tick()
        {
            Player Player = this.GameMode.Player;

            if (Player != null)
            {
                while (!Player.HasMaxEnergy)
                {
                    if (this.Timer.GetRemainingSeconds(this.GameMode.Time) <= 0)
                    {
                        ++Player.Energy;
                        this.Timer.IncreaseTimer(1);
                    }
                }

                if (Player.HasMaxEnergy)
                {
                    this.Timer.StopTimer();
                }
            }
        }

        /// <summary>
        /// Loads this instance from json.
        /// </summary>
        internal void Load(JToken Json)
        {
            if (Json != null)
            {
                if (JsonHelper.GetJsonObject(Json, "timer", out JToken JTimer))
                {
                    this.Timer.Load(JTimer);
                }
            }
        }

        /// <summary>
        /// Saves this instance to json.
        /// </summary>
        /// <param name="Json"></param>
        internal JObject Save()
        {
            return new JObject
            {
                {
                    "timer",
                    this.Timer.Save(this.GameMode.Time)
                }
            };
        }
    }
}