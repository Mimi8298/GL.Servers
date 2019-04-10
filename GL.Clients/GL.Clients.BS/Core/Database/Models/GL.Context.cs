namespace GL.Clients.BS.Core.Database.Models
{
    using System.Data.Entity;

    using MySql.Data.Entity;

    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class MySQL : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MySQL"/> class.
        /// </summary>
        public MySQL() : base("name=GL_MySQL")
        {
            // MySQL.
        }

        /// <summary>
        /// Gets or sets the clans.
        /// </summary>
        public virtual DbSet<clans> Clans
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the players.
        /// </summary>
        public virtual DbSet<players> Players
        {
            get;
            set;
        }
    }
}