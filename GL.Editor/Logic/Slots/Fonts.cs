namespace GL.Editor.Logic.Slots
{
    using System;
    using System.Collections.Generic;

    using GL.Editor.Logic.Slots.Items;

    internal class Fonts : Dictionary<uint, Font>
    {
        /// <summary>
        /// Adds the specified font.
        /// </summary>
        /// <param name="Font">The font.</param>
        internal void Add(Font Font)
        {
            if (this.ContainsKey(Font.Identifier))
            {
                Console.WriteLine("Font already in list.");
            }
            else
            {
                this.Add(Font.Identifier, Font);
            }
        }
    }
}