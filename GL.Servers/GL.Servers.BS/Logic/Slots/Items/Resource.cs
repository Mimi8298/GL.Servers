namespace GL.Servers.BS.Logic.Slots.Items
{
    using GL.Servers.BS.Core;
    using GL.Servers.BS.Files;

    using Newtonsoft.Json;

    using Resources = GL.Servers.BS.Logic.Slots.Resources;

    internal class Resource
    {
        [JsonProperty("type")]          internal int Type;
        [JsonProperty("identifier")]    internal int Identifier;
        [JsonProperty("count")]         internal int Count;

        [JsonProperty("resources")]     internal Resources Resources;

        /// <summary>
        /// Gets or sets the global identifier.
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
        /// Initializes a new instance of the <see cref="Resource"/> class.
        /// </summary>
        internal Resource()
        {
            // Resource.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Resource"/> class.
        /// </summary>
        /// <param name="GlobalID">The global identifier.</param>
        /// <param name="Count">The count.</param>
        internal Resource(int GlobalID, int Count)
        {
            this.GlobalID   = GlobalID;
            this.Count      = Count;
        }

        /// <summary>
        /// Adds the specified count.
        /// </summary>
        /// <param name="Count">The count.</param>
        internal void Add(int Count)
        {
            Files.CSV_Logic.Resources Resource = CSV.Tables.GetWithGlobalID(this.GlobalID) as Files.CSV_Logic.Resources;

            if (Resource != null)
            {
                if (Resource.Cap > 0)
                {
                    if ((this.Count + Count) <= Resource.Cap)
                    {
                        this.Count += Count;
                    }
                    else
                    {
                        Logging.Error(this.GetType(), "Error when adding " + Count + " to a resource, the cap of " + Resource.Cap + " \"" + Resource.Name + "\" would be reached.");
                    }
                }
                else
                {
                    this.Count += Count;
                }
            }
            else
            {
                Logging.Error(this.GetType(), "Error when adding " + Count + " to a resource, the Resources instance was null.");
            }
        }

        /// <summary>
        /// Removes the specified count.
        /// </summary>
        /// <param name="Count">The count.</param>
        internal void Remove(int Count)
        {
            if (Count >= 0)
            {
                this.Count -= Count;
            }
            else
            {
                Logging.Error(this.GetType(), "Error when removing " + Count + " to a resource, the value is negative.");
            }
        }
    }
}