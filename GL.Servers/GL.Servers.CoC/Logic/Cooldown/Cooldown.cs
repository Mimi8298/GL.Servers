namespace GL.Servers.CoC.Logic
{
    using GL.Servers.CoC.Core;
    using GL.Servers.CoC.Extensions.Helper;
    using Newtonsoft.Json.Linq;

    public class Cooldown
    {
        private int PrecSubTick;
        private int CooldownTime;

        internal int Target;

        /// <summary>
        /// Gets a value indicating the cooldown seconds.
        /// </summary>
        internal int CooldownSeconds
        {
            get
            {
                long var = ((-2004318071L * this.CooldownTime) >> 32) + this.CooldownTime;
                return (int) ((var >> 3) + (var >> 31));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cooldown"/> class.
        /// </summary>
        public Cooldown()
        {
            // Cooldown.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cooldown"/> class.
        /// </summary>
        public Cooldown(int Cooldown, int Target)
        {
            this.CooldownTime = 15 * Cooldown;
            this.Target = Target;
        }

        /// <summary>
        /// Creates a fast forward of time.
        /// </summary>
        internal void FastForwardTime(int Seconds)
        {
            this.CooldownTime -= 15 * Seconds;

            if (this.CooldownTime <= 0)
            {
                this.CooldownTime = 0;
            }
        }

        /// <summary>
        /// Loads this instance from json.
        /// </summary>
        internal void Load(JToken Token)
        {
            if (!JsonHelper.GetJsonNumber(Token, "cooldown", out this.CooldownTime))
            {
                Logging.Error(this.GetType(), "Load() - Cooldown was not found!");
            }

            if (!JsonHelper.GetJsonNumber(Token, "target", out this.Target))
            {
                Logging.Error(this.GetType(), "Load() - Target was not found!");
            }
        }

        /// <summary>
        /// Saves this instance to json.
        /// </summary>
        internal JObject Save()
        {
            JObject Json = new JObject();

            Json.Add("cooldown", this.CooldownSeconds);
            Json.Add("target", this.Target);

            return Json;
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        internal void Update(Time Time)
        {
            this.FastForwardTime(Time.SubTick - this.PrecSubTick);
            this.PrecSubTick = Time.SubTick;
        }
    }
}