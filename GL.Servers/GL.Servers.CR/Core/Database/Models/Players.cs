namespace GL.Servers.CR.Core.Database.Models
{
    public class Players
    {
        /// <summary>
        /// Gets or sets the high identifier.
        /// </summary>
        internal int HighID
        {
            get;
            set;
        }
        

        /// <summary>
        /// Gets or sets the low identifier.
        /// </summary>
        internal int LowID
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the facebook identifier.
        /// </summary>
        internal string FacebookID
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the google identifier.
        /// </summary>
        internal string GoogleID
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the gamecenter identifier.
        /// </summary>
        internal string GamecenterID
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        internal string Data
        {
            get;
            set;
        }
    }
}