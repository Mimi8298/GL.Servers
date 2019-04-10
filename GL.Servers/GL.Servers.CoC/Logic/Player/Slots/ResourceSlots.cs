namespace GL.Servers.CoC.Logic.Slots
{
    using GL.Servers.CoC.Extensions.Game;

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
        /// Initializes the instance.
        /// </summary>
        internal void Initialize()
        {
            this.Set(3000001, Globals.StartingGold);
            this.Set(3000002, Globals.StartingElixir);

            this.Set(3000007, Globals.StartingGold2);
            this.Set(3000008, Globals.StartingElixir2);
        }
    }
}