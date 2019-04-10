namespace GL.Servers.BB.Logic.GameObject
{
    using Newtonsoft.Json.Linq;

    internal class Component
    {
        internal GameObject Parent;

        internal bool Enabled;

        /// <summary>
        /// Gets a value indicating the component type.
        /// </summary>
        internal virtual int Type
        {
            get
            {
                return -1;
            }
        }

        /// <summary>
        /// Gets a value indicating the checksum.
        /// </summary>
        internal virtual int Checksum
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Component"/> class.
        /// </summary>
        public Component(GameObject GameObject)
        {
            this.Parent = GameObject;
        }

        /// <summary>
        /// Called after the loading.
        /// </summary>
        internal virtual void LoadingFinished()
        {

        }

        /// <summary>
        /// Loads the component from json.
        /// </summary>
        /// <param name="Token">The json.</param>
        internal virtual void Load(JToken Token)
        {
            
        }

        /// <summary>
        /// Saves the component to json.
        /// </summary>
        /// <param name="Json">The json.</param>
        internal virtual void Save(JObject Json)
        {
            
        }

        /// <summary>
        /// Ticks this instance.
        /// </summary>
        internal virtual void Tick()
        {
            
        }
    }
}