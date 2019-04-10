namespace GL.Servers.BB.Logic.Slots
{
    using GL.Servers.BB.Core;
    using GL.Servers.BB.Files.CSV_Logic;
    using GL.Servers.BB.Logic.Items;

    internal class ResourceSlots : DataSlots<Item>
    {
        internal ResourceSlots() : base()
        {
            // ResourceSlots.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceSlots"/> class.
        /// </summary>
        /// <param name="Player">The player.</param>
        internal ResourceSlots(Player Player) : base(Player)
        {
            // ResourceSlots.
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        internal override void Initialize()
        {
            ResourceBundle ResourceBundle = Resources.GameSettings.StartingResources;

            for (int i = 1; i < 5; i++)
            {
                this.Set(ResourceBundle.Resources[i].Data, ResourceBundle.Resources[i].Count);
            }
        }
    }
}