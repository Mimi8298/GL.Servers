namespace GL.Proxy.CoC.Core
{
    using GL.Proxy.CoC.Core.Network;
    using GL.Proxy.CoC.Logic.Slots;

    /// <summary>
    /// This class load and initialize all required classes.
    /// </summary>
    public class Resources
    {
        internal static Devices Devices;
        internal static Gateway Gateway;

        /// <summary>
        /// Initializes a new instance of the <see cref="Resources"/> class.
        /// </summary>
        public static void Initialize()
        {
            Resources.Devices        = new Devices();
            Resources.Gateway        = new Gateway();
        }
    }
}