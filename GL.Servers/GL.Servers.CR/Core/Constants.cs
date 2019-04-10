namespace GL.Servers.CR.Core
{
    using System;

    using GL.Servers.Logic.Enums;
    using GL.Servers.CR.Core.Database;

    internal class Constants
    {
        /// <summary>
        /// The unique server identifier, used to partition the database.
        /// </summary>
        internal const int ServerID         = 0;
        
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
        internal const int MaxPlayers       = 0;

        /// <summary>
        /// The maximum of send operation the program can process.
        /// </summary>
        internal const int MaxSends         = 500;

        /// <summary>
        /// Whether we should save/find player in <see cref="RedisDatabase"/>, <see cref="MySQL"/>, or both.
        /// </summary>
        internal const DBMS Database        = DBMS.MySQL;

        /// <summary>
        /// Gets the maintenance end <see cref="DateTime"/> instance.
        /// </summary>
        internal static DateTime Maintenance
        {
            get
            {
                return new DateTime(2017, 8, 14, 22, 0, 0); 
            }
        }
    }
}