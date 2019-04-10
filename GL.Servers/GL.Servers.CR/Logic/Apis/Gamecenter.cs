namespace GL.Servers.CR.Logic.Apis
{
    using Newtonsoft.Json;

    internal class Gamecenter
    {
        internal Player Player;

        [JsonProperty] internal string Identifier;
        [JsonProperty] internal string Certificate;
        [JsonProperty] internal string AppBundle;

        /// <summary>
        /// Initializes a new instance of the <see cref="Gamecenter"/> class.
        /// </summary>
        internal Gamecenter()
        {
            // Gamecenter.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Gamecenter"/> class.
        /// </summary>
        /// <param name="_Player">The player.</param>
        internal Gamecenter(Player Player) : this()
        {
            this.Player = Player;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Gamecenter"/> is filled.
        /// </summary>
        internal bool Filled
        {
            get
            {
                return !string.IsNullOrEmpty(this.Identifier) && !string.IsNullOrEmpty(this.Certificate) && !string.IsNullOrEmpty(this.AppBundle);
            }
        }
    }
}