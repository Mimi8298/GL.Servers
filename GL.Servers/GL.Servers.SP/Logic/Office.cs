namespace GL.Servers.SP.Logic
{
    using GL.Servers.SP.Extensions.Helper;
    using Newtonsoft.Json.Linq;

    internal class Office
    {
        /// <summary>
        /// Gets a value indicating the checksum of this instance.
        /// </summary>
        internal int Checksum
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Office"/> class.
        /// </summary>
        internal Office()
        {
            // Office.
        }

        /// <summary>
        /// Creates a fast forward the time.
        /// </summary>
        internal void FastForwardTime(int Seconds)
        {
            
        }

        /// <summary>
        /// Ticks this instance.
        /// </summary>
        internal void Tick()
        {
            
        }

        /// <summary>
        /// Loads this instance from json.
        /// </summary>
        internal void Load(JToken Json)
        {
            
        }

        /// <summary>
        /// Saves this instance to json.
        /// </summary>
        internal JObject Save()
        {
            JObject Json = new JObject();

            JArray Heroes = new JArray();
            JArray NewItems = new JArray();

            Json.Add("heroes", Heroes);
            Json.Add("newitemcount", NewItems);

            return Json;
        }
    }
}