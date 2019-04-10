namespace GL.Editor.Logic.Slots
{
    using System.Collections.Generic;
    using System.Linq;

    using GL.Editor.Logic.Slots.Items;

    internal class Exports : Dictionary<uint, Export>
    {
        /// <summary>
        /// Adds the specified export.
        /// </summary>
        /// <param name="Export">The export.</param>
        internal void Add(Export Export)
        {
            if (this.ContainsKey(Export.Identifier))
            {
                this[Export.Identifier].Name += string.IsNullOrEmpty(Export.Name) ? "" : ", " + Export;
            }
            else
            {
                base.Add(Export.Identifier, Export);
            }
        }

        /// <summary>
        /// Obtient une collection contenant les valeurs dans
        /// <see cref="T:System.Collections.Generic.Dictionary`2" />.
        /// </summary>
        internal List<string> Names
        {
            get
            {
                return this.Values.Select(Export => Export.Name).ToList();
            }
        }

        /// <summary>
        /// Gets the specified export identifier.
        /// </summary>
        /// <param name="ExportID">The export identifier.</param>
        internal Export Get(uint ExportID)
        {
            return this.ContainsKey(ExportID) ? this[ExportID] : null;
        }
    }
}