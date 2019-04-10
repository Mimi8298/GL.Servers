namespace GL.Editor.Logic.Slots
{
    using System;
    using System.Collections.Generic;

    using GL.Editor.Logic.Slots.Items;

    internal class Shapes : Dictionary<uint, Shape>
    {
        /// <summary>
        /// Adds the specified shape.
        /// </summary>
        /// <param name="Shape">The shape.</param>
        public void Add(Shape Shape)
        {
            if (this.ContainsKey(Shape.Identifier))
            {
                Console.WriteLine("Shape already in the list.");
            }
            else
            {
                base.Add(Shape.Identifier, Shape);
            }
        }

        /// <summary>
        /// Adds the specified shape identifier.
        /// </summary>
        /// <param name="ShapeID">The shape identifier.</param>
        public void Add(uint ShapeID)
        {
            if (this.ContainsKey(ShapeID))
            {
                this[ShapeID] = new Shape(ShapeID);
            }
            else
            {
                base.Add(ShapeID, new Shape(ShapeID));
            }
        }
    }
}