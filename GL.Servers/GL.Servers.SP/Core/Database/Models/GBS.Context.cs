namespace GL.Servers.SP.Core.Database.Models.MySQL
{
    using System.Data.Entity;

    using MySql.Data.Entity;

    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class GBS_MySQL : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GBS_MySQL"/> class.
        /// </summary>
        public GBS_MySQL() : base("name=GBS_MySQL")
        {
            // base.Database.CommandTimeout = 20;
        }

        /// <summary>
        /// Gets or sets the clans.
        /// </summary>
        public virtual DbSet<Clans> Clans
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the players.
        /// </summary>
        public virtual DbSet<Players> Players
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the tournaments.
        /// </summary>
        public virtual DbSet<Tournaments> Tournaments
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the battles.
        /// </summary>
        public virtual DbSet<Battles> Battles
        {
            get;
            set;
        }
    }
}