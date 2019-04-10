namespace GL.Servers.BS.Logic.Slots
{
    using System.Collections.Generic;
    using System.Linq;

    using GL.Servers.BS.Core;
    using GL.Servers.BS.Logic.Slots.Items;

    using Newtonsoft.Json;

    internal class Competences : Dictionary<int, Competence>
    {
        [JsonProperty("card")] internal Card Card;

        /// <summary>
        /// Initializes a new instance of the <see cref="Competences"/> class.
        /// </summary>
        internal Competences()
        {
            // Competences.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Competences"/> class.
        /// </summary>
        /// <param name="Player">The player.</param>
        internal Competences(Card Card)
        {
            this.Card = Card;
        }

        /// <summary>
        /// Adds the specified card.
        /// </summary>
        /// <param name="Card">The card.</param>
        internal void Add(Competence Competence)
        {
            if (this.ContainsKey(Competence.GlobalID))
            {
                Logging.Error(this.GetType(), "Error when adding the competence n°" + Competence.GlobalID + " to the list, the competence is already in.");
            }
            else
            {
                base.Add(Competence.GlobalID, Competence);
            }

            Competence.Competences = this;
        }

        /// <summary>
        /// Removes the specified card.
        /// </summary>
        /// <param name="Competence">The card.</param>
        internal void Remove(Competence Competence)
        {
            if (this.ContainsKey(Competence.GlobalID))
            {
                base.Remove(Competence.GlobalID);
            }
        }

        internal byte[] ToBytes
        {
            get
            {
                List<byte> Packet = new List<byte>();

                foreach (Competence Competence in this.Values.ToArray())
                {
                    Packet.AddRange(Competence.ToBytes);
                }

                return Packet.ToArray();
            }
        }
    }
}