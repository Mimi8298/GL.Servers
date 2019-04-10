namespace GL.Servers.BB.Logic.GameObject
{
    using GL.Servers.BB.Extensions.Helper;
    using Newtonsoft.Json.Linq;

    internal class HitpointComponent : Component
    {
        internal int LastDamage;
        internal int AccurateHitpoints;
        internal int AccurateMaxHitpoints;

        internal bool InRegeneration;

        /// <summary>
        /// Gets a value indicating the hitpoints of the gameobject.
        /// </summary>
        internal int Hitpoints
        {
            get
            {
                return (this.AccurateHitpoints + 99) / 100;
            }
        }

        /// <summary>
        /// Gets a value indicating the max hitpoints of the gameobject.
        /// </summary>
        internal int MaxHitpoints
        {
            get
            {
                return (this.AccurateMaxHitpoints + 99) / 100;
            }
        }

        /// <inheritdoc />
        internal override int Type
        {
            get
            {
                return 2;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the gameobject has full hitpoints.
        /// </summary>
        internal bool HasFullHitpoints
        {
            get
            {
                return this.AccurateHitpoints >= this.AccurateMaxHitpoints;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the gameobject has received a damage for the last 30 seconds.
        /// </summary>
        internal bool IsDamagedRecently
        {
            get
            {
                return this.LastDamage < 30;
            }
        }

        /// <inheritdoc />
        public HitpointComponent(GameObject GameObject, int MaxHitpoints) : base(GameObject)
        {
            this.AccurateHitpoints = 100 * MaxHitpoints;
            this.AccurateMaxHitpoints = 100 * MaxHitpoints;
        }

        /// <summary>
        /// Sets the hitpoints.
        /// </summary>
        /// <param name="Hitpoints">The hitpoints.</param>
        internal void SetHitpoints(int Hitpoints)
        {
            Hitpoints = Math.Clamp(100 * Hitpoints, 0, this.MaxHitpoints);
            this.AccurateHitpoints = Hitpoints;
        }

        /// <summary>
        /// Sets the hitpoints.
        /// </summary>
        /// <param name="Hitpoints">The hitpoints.</param>
        internal void SetMaxHitpoints(int MaxHitpoints)
        {
            MaxHitpoints = Math.Clamp(100 * MaxHitpoints, 0, this.MaxHitpoints);
            this.AccurateMaxHitpoints = MaxHitpoints;
        }

        /// <inheritdoc />
        internal override void Load(JToken Token)
        {
            if (JsonHelper.GetInt(Token["hp"], out int Hp))
            {
                this.AccurateHitpoints = Hp;
            }
        }

        /// <inheritdoc />
        internal override void Save(JObject Json)
        {
            if (this.AccurateHitpoints < this.AccurateMaxHitpoints)
            {
                Json.Add("hp", this.AccurateHitpoints);
            }
        }

        /// <inheritdoc />
        internal override void Tick()
        {
            // TODO Implement Tick().
        }
    }
}