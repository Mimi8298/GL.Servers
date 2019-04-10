namespace GL.Servers.GS.Core.Database.Models
{
    using System.Data.Entity;

    using MySql.Data.Entity;

    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class GRS_MySQL : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GRS_MySQL"/> class.
        /// </summary>
        public GRS_MySQL() : base("name=GGS_MySQL")
        {
            // base.Database.CommandTimeout = 20;
        }
        
        /// <summary>
        /// Gets or sets the players.
        /// </summary>
        public virtual DbSet<Players> Players
        {
            get;
            set;
        }
    }
}