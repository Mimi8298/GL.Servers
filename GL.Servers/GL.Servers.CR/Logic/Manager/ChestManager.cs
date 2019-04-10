namespace GL.Servers.CR.Logic.Manager
{
    internal class ChestManager
    {
        internal Player Player;

        internal int ChestOpened;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChestManager"/> class.
        /// </summary>
        /// <param name="Player">The player.</param>
        internal ChestManager(Player Player)
        {
            this.Player = Player;
        }
    }
}