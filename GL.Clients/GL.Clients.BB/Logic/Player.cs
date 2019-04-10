namespace GL.Clients.BB.Logic
{
    internal class Player
    {
        internal int HighID;
        internal int LowID;

        internal string Username;

        /// <summary>
        /// Gets a value indicating whether this player's name is set.
        /// </summary>
        internal bool isNameSet
        {
            get
            {
                return !string.IsNullOrEmpty(this.Username);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        internal Player()
        {
            // Player.
        }
    }
}