namespace GL.Servers.CoC.Files.CSV_Helpers
{
    internal static class GlobalID
    {
        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <param name="GlobalID">The global identifier.</param>
        /// <returns></returns>
        internal static int GetType(int GlobalID)
        {
            return GlobalID / 1000000;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <param name="GlobalID">The global identifier.</param>
        /// <returns></returns>
        internal static int GetID(int GlobalID)
        {
            return GlobalID % 1000000;
        }

        /// <summary>
        /// Creates the specified type.
        /// </summary>
        /// <param name="Type">The type.</param>
        /// <param name="ID">The identifier.</param>
        /// <returns></returns>
        internal static int Create(int Type, int ID)
        {
            return Type * 1000000 + ID;
        }
    }
}