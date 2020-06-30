namespace GL.Servers.CR.Core.Database
{
    using System;
    using System.Threading.Tasks;

    using GL.Servers.CR.Logic.Enums;

    using StackExchange.Redis;

    internal class RedisDatabase : IDatabase
    {
        internal ConnectionMultiplexer Multiplexer;
        internal TimeSpan CacheLifeTime;
        internal IServer Server;

        internal StackExchange.Redis.IDatabase Players;
        internal StackExchange.Redis.IDatabase Alliances;

        /// <summary>
        /// Gets the seed of players.
        /// </summary>
        public int PlayerSeed
        {
            get
            {
                return -1;
            }
        }

        /// <summary>
        /// Gets the seed of alliances.
        /// </summary>
        public int AllianceSeed
        {
            get
            {
                return -1;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RedisDatabase"/> class.
        /// </summary>
        internal RedisDatabase()
        {
            ConfigurationOptions Configuration  = new ConfigurationOptions();

            Configuration.EndPoints.Add("127.0 0.1", 6379);

            Configuration.Password              = "";
            Configuration.ClientName            = this.GetType().Assembly.FullName;

            Configuration.SyncTimeout           = 7000;
            Configuration.ConnectTimeout        = 7000;
            Configuration.ResponseTimeout       = 7000;

            this.Multiplexer                    = ConnectionMultiplexer.Connect(Configuration);
            this.Server                         = this.Multiplexer.GetServer(Configuration.EndPoints[0]);

            this.Players                        = this.Multiplexer.GetDatabase((int) Database.Players);
            this.Alliances                      = this.Multiplexer.GetDatabase((int) Database.Alliances);

            this.Multiplexer.ErrorMessage      += this.OnError;
            this.Multiplexer.ConnectionFailed  += this.OnConnectionFailed;

            this.CacheLifeTime                  = TimeSpan.FromHours(4);
        }

        /// <summary>
        /// Called when [connection failed].
        /// </summary>
        /// <param name="Sender">The sender.</param>
        /// <param name="Error">The <see cref="ConnectionFailedEventArgs"/> instance containing the event data.</param>
        private void OnConnectionFailed(object Sender, ConnectionFailedEventArgs Error)
        {
            Logging.Info(this.GetType(), Error.Exception.Message);
        }

        /// <summary>
        /// Called when [error].
        /// </summary>
        /// <param name="Sender">The sender.</param>
        /// <param name="Error">The <see cref="RedisErrorEventArgs"/> instance containing the event data.</param>
        private void OnError(object Sender, RedisErrorEventArgs Error)
        {
            Logging.Info(this.GetType(), Error.Message);
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        internal void Stop()
        {
            this.Multiplexer.Close();
        }

        /// <summary>
        /// Creates and saves a new player.
        /// </summary>
        public void CreatePlayer(int HighID, int LowID, string JSON)
        {
            this.Players.StringSet((HighID << 32 | (uint) LowID).ToString(), JSON, this.CacheLifeTime);
        }

        public void CreateAlliance(int HighID, int LowID, string JSON)
        {
            this.Alliances.StringSet((HighID << 32 | (uint) LowID).ToString(), JSON, this.CacheLifeTime);
        }

        /// <summary>
        /// Loads and returns the specified player.
        /// </summary>
        public string LoadPlayer(int HighID, int LowID)
        {
            RedisValue Save = this.Players.StringGet((HighID << 32 | (uint) LowID).ToString());

            if (!Save.IsNullOrEmpty)
            {
                return Save.ToString();
            }

            return null;
        }

        /// <summary>
        /// Loads and returns the specified player.
        /// </summary>
        public string LoadPlayer(int HighID, int LowID, string NotUsed)
        {
            RedisValue Save = this.Players.StringGet((HighID << 32 | (uint)LowID).ToString());

            if (!Save.IsNullOrEmpty)
            {
                return Save.ToString();
            }

            return null;
        }

        /// <summary>
        /// Loads and returns the specified player.
        /// </summary>
        public async Task<string> LoadPlayerAsync(int HighID, int LowID)
        {
            RedisValue Save = await this.Players.StringGetAsync((HighID << 32 | (uint)LowID).ToString());

            if (!Save.IsNullOrEmpty)
            {
                return Save.ToString();
            }

            return null;
        }

        /// <summary>
        /// Loads and returns the specified player.
        /// </summary>
        public async Task<string> LoadPlayerAsync(int HighID, int LowID, string NotUsed)
        {
            RedisValue Save = await this.Players.StringGetAsync((HighID << 32 | (uint)LowID).ToString());

            if (!Save.IsNullOrEmpty)
            {
                return Save.ToString();
            }

            return null;
        }

        /// <summary>
        /// Loads and returns the specified alliance.
        /// </summary>
        public string LoadAlliance(int HighID, int LowID)
        {
            RedisValue Save = this.Alliances.StringGet((HighID << 32 | (uint) LowID).ToString());

            if (!Save.IsNullOrEmpty)
            {
                return Save.ToString();
            }

            return null;
        }

        /// <summary>
        /// Loads and returns the specified alliance.
        /// </summary>
        public async Task<string> LoadAllianceAsync(int HighID, int LowID)
        {
            RedisValue Save = await this.Alliances.StringGetAsync((HighID << 32 | (uint) LowID).ToString());

            if (!Save.IsNullOrEmpty)
            {
                return Save.ToString();
            }

            return null;
        }

        /// <summary>
        /// Saves the specified player.
        /// </summary>
        public void SavePlayer(int HighID, int LowID, string JSON)
        {
            this.Players.StringSet((HighID << 32 | (uint) LowID).ToString(), JSON, this.CacheLifeTime);
        }

        /// <summary>
        /// Saves the specified alliance.
        /// </summary>
        public void SaveAlliance(int HighID, int LowID, string JSON)
        {
            this.Alliances.StringSet((HighID << 32 | (uint) LowID).ToString(), JSON);
        }
    }
}
