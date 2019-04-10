namespace GL.Servers.CR.Logic.Slots
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using GL.Servers.CR.Core;
    using GL.Servers.CR.Logic.Commands;

    using GL.Servers.Files;
    
    using Newtonsoft.Json;

    internal class Players : ConcurrentDictionary<long, Player>
    {
        /// <summary>
        /// The settings for the <see cref="JsonConvert" /> class.
        /// </summary>
        private readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            TypeNameHandling            = TypeNameHandling.None,            MissingMemberHandling   = MissingMemberHandling.Ignore,
            DefaultValueHandling        = DefaultValueHandling.Include,     NullValueHandling       = NullValueHandling.Ignore,
            Formatting                  = Formatting.None
        };

        private int Seed;

        /// <summary>
        /// Initializes a new instance of the <see cref="Players"/> class.
        /// </summary>
        internal Players()
        {
            this.Seed = Resources.Database.PlayerSeed;
        }

        /// <summary>
        /// Adds the specified player.
        /// </summary>
        /// <param name="Player">The player.</param>
        internal void Add(Player Player)
        {
            if (this.ContainsKey(Player.PlayerID))
            {
                if (!this.TryUpdate(Player.PlayerID, Player, Player))
                {
                    Logging.Error(this.GetType(), "Unsuccessfuly updated the specified player to the dictionnary.");
                }
            }
            else
            {
                if (!this.TryAdd(Player.PlayerID, Player))
                {
                    Logging.Error(this.GetType(), "Unsuccessfuly added the specified player to the dictionnary.");
                }
            }
        }

        /// <summary>
        /// Removes the specified player.
        /// </summary>
        /// <param name="Player">The player.</param>
        internal void Remove(Player Player)
        {
            Player TmpPlayer;

            if (this.ContainsKey(Player.PlayerID))
            {
                if (!this.TryRemove(Player.PlayerID, out TmpPlayer))
                {
                    Logging.Error(this.GetType(), "Unsuccessfuly removed the specified player from the dictionnary.");
                }
                else
                {
                    if (!TmpPlayer.Equals(Player))
                    {
                        Logging.Error(this.GetType(), "We successfully removed a player from the list but the returned player was not equal to our player.");
                    }
                }
            }
            
            if (Player.GameMode != null)
            {
                if (Player.GameMode.CommandManager.AvailableServerCommands.Count > 0)
                {
                    foreach (Command Command in Player.GameMode.CommandManager.AvailableServerCommands.Values.ToArray())
                    {
                        if (Command.IsServerCommand)
                        {
                            Command.Execute(Player.GameMode);
                        }
                    }
                }
            }

            this.Save(Player);
        }

        /// <summary>
        /// Gets the player using the specified identifier in the specified database.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="HighID">The high identifier.</param>
        /// <param name="LowID">The low identifier.</param>
        /// <param name="Store">if set to <c>true</c> [store].</param>
        internal Player Get(int HighID, int LowID, bool Store = true)
        {
            long ID = (long) HighID << 32 | (uint) LowID;

            if (!this.TryGetValue(ID, out Player Player))
            {
                string JSON;
                
                if (Resources.CacheSystem == null || (JSON = Resources.CacheSystem.LoadPlayer(HighID, LowID)) == null)
                {
                    JSON = Resources.Database.LoadPlayer(HighID, LowID);
                }

                if (!string.IsNullOrEmpty(JSON))
                {
                    Player = JsonConvert.DeserializeObject<Player>(JSON, this.Settings);

                    if (Store)
                    {
                        this.Add(Player);
                    }
                }
            }

            return Player;
        }

        /// <summary>
        /// Gets the player using the specified identifier in the specified database.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="HighID">The high identifier.</param>
        /// <param name="LowID">The low identifier.</param>
        /// <param name="Store">if set to <c>true</c> [store].</param>
        internal async Task<Player> GetAsync(int HighID, int LowID, bool Store = true)
        {
            long ID = (long) HighID << 32 | (uint)LowID;

            if (!this.TryGetValue(ID, out Player Player))
            {
                string JSON;

                if (Resources.CacheSystem == null || (JSON = await Resources.CacheSystem.LoadPlayerAsync(HighID, LowID)) == null)
                {
                    JSON = await Resources.Database.LoadPlayerAsync(HighID, LowID);
                }

                if (!string.IsNullOrEmpty(JSON))
                {
                    Player = JsonConvert.DeserializeObject<Player>(JSON, this.Settings);

                    if (Store)
                    {
                        this.Add(Player);
                    }
                }
            }

            return Player;
        }

        /// <summary>
        /// Creates a new player using the specified identifier in the specified database.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="HighID">The high identifier.</param>
        /// <param name="LowID">The low identifier.</param>
        /// <param name="DBMS">The DBMS.</param>
        /// <param name="Store">if set to <c>true</c> [store].</param>
        internal Player Create(int HighID = Constants.ServerID, int LowID = 0)
        {
            Player Player       = new Player(HighID, Interlocked.Increment(ref this.Seed));

            Player.Home.Load(Home.JObject);
            Player.Home.HighID = Player.HighID;
            Player.Home.LowID = Player.LowID;

            for (int i = 0; i < 40; i++)
            {
                char Letter     = (char) Resources.Random.Next('A', 'Z');
                Player.PassToken = Player.PassToken + Letter;
            }

            string JSON = JsonConvert.SerializeObject(Player, this.Settings);

            if (Resources.CacheSystem != null)
            {
                Resources.CacheSystem.CreatePlayer(HighID, LowID, JSON);
            }

            Resources.Database.CreatePlayer(Player.HighID, Player.LowID, JSON);

            this.Add(Player);

            return Player;
        }

        /// <summary>
        /// Saves the specified player in the specified database.
        /// </summary>
        /// <param name="Player">The player.</param>
        internal void Save(Player Player)
        {
            string JSON = JsonConvert.SerializeObject(Player, this.Settings);

            if (Resources.CacheSystem != null)
            {
                Resources.CacheSystem.SavePlayer(Player.HighID, Player.LowID, JSON);
            }

            Resources.Database.SavePlayer(Player.HighID, Player.LowID, JSON);
        }
        
        /// <summary>
        /// Saves the specified DBMS.
        /// </summary>
        /// <param name="DBMS">The DBMS.</param>
        internal void Save()
        {
            Player[] Players = this.Values.ToArray();

            Parallel.ForEach(Players, Player =>
            {
                try
                {
                    this.Save(Player);
                }
                catch (Exception Exception)
                {
                    Logging.Error(this.GetType(), Exception.GetType().Name + ", did not successed to save a player at shutdown.");
                }
            });

            Logging.Info(this.GetType(), "Saved " + Players.Length + " players.");
            Console.WriteLine("Saved " + Players.Length + " players.");
        }
    }
}