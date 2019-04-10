namespace GL.Servers.CoC.Logic
{
    using GL.Servers.CoC.Core;

    internal class ResourceStorageComponent : Component
    {
        internal int[] MaxArray;

        /// <summary>
        /// Gets a value indicating the component type.
        /// </summary>
        internal override int Type
        {
            get
            {
                return 6;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceStorageComponent"/> class.
        /// </summary>
        public ResourceStorageComponent(GameObject GameObject) : base(GameObject)
        {
            // ResourceStorageComponent.
        }

        /// <summary>
        /// Gets the max stockage resources.
        /// </summary>
        internal int GetMax(int ResourceID)
        {
            if (ResourceID > this.MaxArray.Length)
            {
                Logging.Info(this.GetType(), "Index is out of range : " + ResourceID);
                return 0;
            }

            return this.MaxArray[ResourceID];
        }

        /// <summary>
        /// Sets the max array.
        /// </summary>
        internal void SetMaxArray(int[] MaxArray)
        {
            this.MaxArray = MaxArray;
        }
    }
}