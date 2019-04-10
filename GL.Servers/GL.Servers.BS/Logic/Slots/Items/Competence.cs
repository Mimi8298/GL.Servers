namespace GL.Servers.BS.Logic.Slots.Items
{
    using System.Collections.Generic;

    using GL.Servers.BS.Core;
    using GL.Servers.BS.Files;
    using GL.Servers.BS.Files.CSV_Logic;
    using GL.Servers.Extensions.List;

    using Newtonsoft.Json;

    internal class Competence
    {
        internal const int Reference    = 1000000;

        [JsonProperty("type")]          internal int Type;
        [JsonProperty("identifier")]    internal int Identifier;
        [JsonProperty("level")]         internal int Level;

        [JsonProperty("competences")]   internal Competences Competences;

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
        /// Initializes a new instance of the <see cref="Competence"/> class.
        /// </summary>
        internal Competence()
        {
            this.Level = 1;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Competence"/> class.
        /// </summary>
        /// <param name="GlobalID">The global identifier.</param>
        internal Competence(int GlobalID) : this()
        {
            this.GlobalID = GlobalID;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Competence"/> class.
        /// </summary>
        /// <param name="Type">The type.</param>
        /// <param name="Identifier">The identifier.</param>
        internal Competence(int Type, int Identifier) : this()
        {
            this.Type       = Type;
            this.Identifier = Identifier;
        }

        /// <summary>
        /// Upgrades this instance.
        /// </summary>
        public void Upgrade()
        {
            Cards Card = CSV.Tables.GetWithGlobalID(this.GlobalID) as Cards;

            if (Card != null)
            {
                if (this.Level < Card.NumCards)
                {
                    this.Level++;
                }
                else
                {
                    Logging.Error(this.GetType(), "Error when upgrading the card n°" + this.GlobalID + ", the level was equal or superior to the limit.");
                }
            }
            else
            {
                Logging.Error(this.GetType(), "Error when upgrading the card n°" + this.GlobalID + ", the Card instance was null.");
            }
        }

        internal byte[] ToBytes
        {
            get
            {
                List<byte> Packet = new List<byte>();

                Packet.AddVInt(this.Type);
                Packet.AddVInt(this.Identifier);
                Packet.AddVInt(this.Level);

                return Packet.ToArray();
            }
        }
    }
}