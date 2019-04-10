namespace GL.Editor.Logic.Slots.Items
{
    using System.Collections.Generic;
    using System.Drawing;

    internal class Chunk
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        internal uint Identifier;

        /// <summary>
        /// Gets or sets the points xy.
        /// </summary>
        internal List<PointF> PointsXY;

        /// <summary>
        /// Gets or sets the points uv.
        /// </summary>
        internal List<PointF> PointsUV;

        /// <summary>
        /// Initializes a new instance of the <see cref="Chunk"/> class.
        /// </summary>
        /// <param name="Identifier">The identifier.</param>
        /// <param name="PointsXY">The points xy.</param>
        /// <param name="PointsUV">The points uv.</param>
        internal Chunk(uint Identifier = 0, List<PointF> PointsXY = null, List<PointF> PointsUV = null)
        {
            this.Identifier = Identifier;

            this.PointsXY   = PointsXY;
            this.PointsUV   = PointsUV;
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