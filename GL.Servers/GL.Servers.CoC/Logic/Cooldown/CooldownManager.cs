namespace GL.Servers.CoC.Logic
{
    using System.Collections.Generic;
    using GL.Servers.CoC.Extensions.Helper;
    using Newtonsoft.Json.Linq;

    public class CooldownManager
    {
        private List<Cooldown> Cooldowns;

        /// <summary>
        /// Initializes a new instance of the <see cref="CooldownManager"/> class.
        /// </summary>
        public CooldownManager()
        {
            this.Cooldowns = new List<Cooldown>(8);
        }

        /// <summary>
        /// Adds a cooldown.
        /// </summary>
        internal void AddCooldown(int Cooldown, int Target)
        {
            this.Cooldowns.Add(new Cooldown(Cooldown, Target));
        }

        /// <summary>
        /// Deletes all cooldowns.
        /// </summary>
        internal void DeleteCooldowns()
        {
            this.Cooldowns.Clear();
        }

        /// <summary>
        /// Create a fast forward of the time.
        /// </summary>
        internal void FastForwardTime(int Seconds)
        {
            this.Cooldowns.ForEach(Cooldown => Cooldown.FastForwardTime(Seconds));
        }

        /// <summary>
        /// Gets the cooldown time in seconds.
        /// </summary>
        internal int GetCooldownSeconds(int TargetID)
        {
            Cooldown Cooldown = this.Cooldowns.Find(cooldown => cooldown.Target == TargetID);

            if (Cooldown != null)
            {
                return Cooldown.CooldownSeconds;
            }

            return 0;
        }
        
        /// <summary>
        /// Loads this instance from json.
        /// </summary>
        internal void Load(JToken Token)
        {
            if (JsonHelper.GetJsonArray(Token, "cooldowns", out JArray Cooldowns))
            {
                foreach (JToken Object in Cooldowns)
                {
                    Cooldown Cooldown = new Cooldown();
                    Cooldown.Load(Object);
                    this.Cooldowns.Add(Cooldown);
                }
            }
        }

        /// <summary>
        /// Saves this instance to jsoon.
        /// </summary>
        /// <returns></returns>
        internal void Save(JObject Json)
        {
            JArray Cooldowns = new JArray();

            this.Cooldowns.ForEach(Cooldown =>
            {
                Cooldowns.Add(Cooldown.Save());
            });

            Json.Add("cooldowns", Cooldowns);
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        internal void Update(Time Time)
        {
            for (int i = 0; i < this.Cooldowns.Count; i++)
            {
                this.Cooldowns[i].Update(Time);

                if (this.Cooldowns[i].CooldownSeconds <= 0)
                {
                    this.Cooldowns.RemoveAt(i--);
                }
            }
        }

        /// <summary>
        /// Destructes this instance.
        /// </summary>
        ~CooldownManager()
        {
            this.DeleteCooldowns();
        }
    }
}