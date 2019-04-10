namespace GL.Clients.BB.Core
{
    using GL.Clients.BB.Logic;

    internal class Resources
    {
        internal static bool Started;

        /// <summary>
        /// Initializes a new instance of the <see cref="Resources"/> class.
        /// </summary>
        internal static void Initialize()
        {
            if (!Resources.Started)
            {
                Client Client       = new Client();
            }

            Resources.Started       = true;
        }
    }
}