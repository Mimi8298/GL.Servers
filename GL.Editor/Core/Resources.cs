namespace GL.Editor.Core
{
    using GL.Editor.Logic;

    internal class Resources
    {
        /// <summary>
        /// The list of all sc in memory <see cref="SC"/>s.
        /// </summary>
        public static Files Files;

        /// <summary>
        /// Initializes a new instance of the <see cref="Resources"/> class.
        /// </summary>
        public Resources()
        {
            Resources.Files = new Files();
        }
    }
}