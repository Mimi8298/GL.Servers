namespace GL.Servers.BS.Logic.Slots.Items
{
    using System.Collections.Generic;
    using System.Linq;

    using GL.Servers.Extensions.List;

    using Newtonsoft.Json;

    internal class Card
    {
        internal const int Reference    = 1000000;

        [JsonProperty("type")]          internal int Type;
        [JsonProperty("identifier")]    internal int Identifier;
        [JsonProperty("trophies")]      internal int Trophies;

        [JsonProperty("competences")]   internal Competences Competences;
        [JsonProperty("deck")]          internal Deck Deck;

        internal int Level
        {
            get
            {
                return this.Competences.Values.Sum(Competence => Competence.Level) - 1; // Card_Unlock = -1
            }
        }

        /// <summary>
        /// Gets the global identifier.
        /// </summary>
        internal int GlobalID
        {
            get
            {
                return (this.Type * 1000000) + this.Identifier;
            }
            set
            {
                this.Type       = Files.CSV_Helpers.GlobalID.GetType(value);
                this.Identifier = Files.CSV_Helpers.GlobalID.GetID(value);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Card"/> class.
        /// </summary>
        internal Card()
        {
            this.Competences = new Competences(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Card"/> class.
        /// </summary>
        /// <param name="GlobalID">The global identifier.</param>
        internal Card(int GlobalID) : this()
        {
            this.GlobalID = GlobalID;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Card"/> class.
        /// </summary>
        /// <param name="Type">The type.</param>
        /// <param name="Identifier">The identifier.</param>
        internal Card(int Type, int Identifier) : this()
        {
            this.Type       = Type;
            this.Identifier = Identifier;
        }

        internal byte[] ToBytes
        {
            get
            {
                List<byte> Packet = new List<byte>();

                Packet.AddVInt(this.Type);
                Packet.AddVInt(this.Identifier);
                Packet.AddVInt(0); // 1D
                // Packet.AddVInt(0);
                Packet.AddVInt(this.Trophies);
                Packet.AddVInt(this.Trophies);
                Packet.AddVInt(this.Level);

                return Packet.ToArray();
            }
        }
    }
}