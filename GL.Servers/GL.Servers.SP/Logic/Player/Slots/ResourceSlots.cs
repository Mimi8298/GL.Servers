namespace GL.Servers.SP.Logic.Slots
{
    using GL.Servers.SP.Extensions.Game;

    internal class ResourceSlots : DataSlots
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceSlots"/> class.
        /// </summary>
        internal ResourceSlots(int Capacity = 10) : base(Capacity)
        {
            // ResourceSlots.
        }
        
        /// <summary>
        /// Initializes the list.
        /// </summary>
        internal void Initialize()
        {
            this.Set(9000001, Globals.StartingEnergy);
        }
    }
}