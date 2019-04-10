namespace GL.Editor.Logic.Slots.Items
{
    using System.Drawing;

    internal class Shape
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Shape"/> class.
        /// </summary>
        public Shape(uint _ShapeID, uint _PolyCount = 0, uint _PointsCount = 0)
        {
            this.Identifier = _ShapeID;
            this.PolygonCount = _PolyCount;
            this.PointsCount = _PointsCount;

            this.Chunks = new Chunks();
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

        /// <summary>
        /// Gets or sets the polygon count.
        /// </summary>
        /// <value>
        /// The polygon count.
        /// </value>
        public uint PolygonCount
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the points count.
        /// </summary>
        /// <value>
        /// The points count.
        /// </value>
        public uint PointsCount
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>
        /// The image.
        /// </value>
        public Bitmap Image
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the chunks.
        /// </summary>
        /// <value>
        /// The chunks.
        /// </value>
        public Chunks Chunks
        {
            get;
            set;
        }
    }
}