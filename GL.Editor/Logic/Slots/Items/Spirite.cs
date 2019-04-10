namespace GL.Editor.Logic.Slots.Items
{
    internal class Spirite
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Spirite"/> class.
        /// </summary>
        public Spirite(uint _Identifier = 0)
        {
            this.Identifier = _Identifier;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public uint Identifier
        {
            get;
            set;
        }
    }
}