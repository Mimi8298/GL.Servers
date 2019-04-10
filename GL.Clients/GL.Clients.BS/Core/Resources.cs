namespace GL.Clients.BS.Core
{
    using GL.Clients.BS.Logic.Slots;

    internal class Resources
    {
        internal static bool Started;
        internal static Bots Bots;

        /// <summary>
        /// Initializes a new instance of the <see cref="Resources"/> class.
        /// </summary>
        internal static void Initialize()
        {
            if (!Resources.Started)
            {
                Resources.Bots      = new Bots();
            }

            Resources.Started       = true;
        }
    }
}