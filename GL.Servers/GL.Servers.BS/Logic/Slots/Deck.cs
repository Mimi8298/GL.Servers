namespace GL.Servers.BS.Logic.Slots
{
    using System.Collections.Generic;
    using System.Linq;

    using GL.Servers.BS.Core;
    using GL.Servers.BS.Logic.Slots.Items;
    using GL.Servers.Extensions.List;

    using Newtonsoft.Json;

    internal class Deck : Dictionary<int, Card>
    {
        [JsonProperty("player")] internal Player Player;

        /// <summary>
        /// Initializes a new instance of the <see cref="Deck"/> class.
        /// </summary>
        internal Deck()
        {
            // Deck.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Deck"/> class.
        /// </summary>
        /// <param name="Player">The player.</param>
        internal Deck(Player Player)
        {
            this.Player = Player;
        }

        /// <summary>
        /// Adds the specified card.
        /// </summary>
        /// <param name="Card">The card.</param>
        internal void Add(Card Card)
        {
            if (this.ContainsKey(Card.GlobalID))
            {
                Logging.Error(this.GetType(), "Error when adding the card n°" + Card.GlobalID + " to the deck, the card is already in the deck.");
            }
            else
            {
                base.Add(Card.GlobalID, Card);
            }

            Card.Deck = this;
        }

        /// <summary>
        /// Removes the specified card.
        /// </summary>
        /// <param name="Card">The card.</param>
        internal void Remove(Card Card)
        {
            if (this.ContainsKey(Card.GlobalID))
            {
                base.Remove(Card.GlobalID);
            }
        }

        internal byte[] ToBytes
        {
            get
            {
                List<byte> Packet = new List<byte>();

                foreach (Card Card in this.Values.ToArray())
                {
                    Packet.AddVInt(Card.Type);
                    Packet.AddVInt(Card.Identifier);
                    Packet.AddVInt(0); // 1D
                    // Packet.AddVInt(0);
                    Packet.AddVInt(Card.Trophies);
                    Packet.AddVInt(Card.Trophies);
                    Packet.AddVInt(Card.Level);
                }

                return Packet.ToArray();
            }
        }
    }
}