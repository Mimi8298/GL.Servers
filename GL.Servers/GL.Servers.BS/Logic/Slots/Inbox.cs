namespace GL.Servers.BS.Logic.Slots
{
    using System.Collections.Generic;

    using GL.Servers.BS.Logic.Slots.Items;

    using Newtonsoft.Json;

    internal class Inbox : List<Mail>
    {
        [JsonProperty("player")] internal Player Player;

        /// <summary>
        /// Initializes a new instance of the <see cref="Inbox"/> class.
        /// </summary>
        /// <param name="Player">The player.</param>
        internal Inbox(Player Player)
        {
            this.Player = Player;
        }
    }
}