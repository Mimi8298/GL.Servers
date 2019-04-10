namespace GL.Clients.BS.Logic.Slots.Items
{
    using Newtonsoft.Json;

    internal class Brawler
    {
        /// <summary>
        /// Gets the global identifier.
        /// </summary>
        [JsonProperty("card_global")]   internal int GlobalID
        {
            get
            {
                return this.CardType * 1000000 + this.CardID;
            }
        }

        [JsonProperty("card_type")]     internal int CardType;
        [JsonProperty("card_id")]       internal int CardID;

        [JsonProperty("trophies")]      internal int Trophies;
        [JsonProperty("trophies2")]     internal int Trophies2;
        [JsonProperty("level")]         internal int Level;

        /// <summary>
        /// Initializes a new instance of the <see cref="Brawler"/> class.
        /// </summary>
        internal Brawler()
        {
            // Brawler.
        }
    }
}