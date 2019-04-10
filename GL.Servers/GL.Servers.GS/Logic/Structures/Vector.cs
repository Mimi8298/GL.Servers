namespace GL.Servers.GS.Logic.Structures
{
    internal class Vector
    {
        internal int X;
        internal int Y;

		/// <summary>
		/// Initializes a new instance of the <see cref="Vector" /> class.
		/// </summary>
        internal Vector(int X = 0, int Y = 0)
        {
            this.X = X;
            this.Y = Y;
        }
    }
}