namespace GL.Servers.SP.Core
{
    using System;

    using GL.Servers.Logic.Enums;

    internal class Constants
    {
        /// <summary>
        /// The unique server identifier, used to partition the database.
        /// </summary>
        internal const int ServerID         = 0;

        /// <summary>
        /// If set to true, the server will only accept authorized ip's.
        /// </summary>
        internal const bool Local           = false;

        /// <summary>
        /// The length of the buffer used to send packets.
        /// </summary>
        internal const int SendBuffer       = 2048 * 1;

        /// <summary>
        /// The length of the buffer used to receive packets.
        /// </summary>
        internal const int ReceiveBuffer    = 2048 * 1;

        /// <summary>
        /// The maximum of players we can handle at same time.
        /// </summary>
        internal const int MaxPlayers       = 1000 * 0;

        /// <summary>
        /// The maximum of send operation the program can process.
        /// </summary>
        internal const int MaxSends         = 1000 * 0;

        /// <summary>
        /// Whether we should save/find player in <see cref="Redis"/>, <see cref="MySQL"/>, or both.
        /// </summary>
        internal const DBMS Database        = DBMS.Mongo;

        internal static DateTime MaintenanceTime
        {
            get
            {
                return DateTime.UtcNow.AddMinutes(-1);
            }
        }

        /// <summary>
        /// Array of IP Address authorized to log in the server even if it's in maintenance / updating / local.
        /// </summary>
        internal static string[] AuthorizedIP =
        {
            "88.189.197.11",    // Berkan
            "176.184.160.63",   // Alexandre de J&M
            "78.241.228.133",   // Guillaume l'intello
        };
    }
}