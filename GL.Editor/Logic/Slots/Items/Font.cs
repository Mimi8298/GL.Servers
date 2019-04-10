namespace GL.Editor.Logic.Slots.Items
{
    using System.Collections.Generic;

    internal class Font
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        internal uint Identifier;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        internal string Name;

        /// <summary>
        /// Initializes a new instance of the <see cref="Font"/> class.
        /// </summary>
        /// <param name="Identifier">The identifier.</param>
        /// <param name="Name">The name.</param>
        internal Font(uint Identifier = 0, string Name = "")
        {
            this.Identifier = Identifier;
            this.Name       = Name;
        }

        /// <summary>
        /// Gets this instance bytes.
        /// </summary>
        internal byte[] ToBytes
        {
            get
            {
                List<byte> Packet = new List<byte>();

                return Packet.ToArray();
            }
        }
    }
}