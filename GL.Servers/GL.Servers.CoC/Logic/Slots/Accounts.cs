namespace GL.Servers.CoC.Logic.Slots
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Collections.Concurrent;

    using GL.Servers.CoC.Core;
    using GL.Servers.CoC.Core.Database;

    using GL.Servers.Logic.Enums;
    using GL.Servers.CoC.Files;
    using GL.Servers.CoC.Files.CSV_Helpers;
    using GL.Servers.CoC.Logic;

    using MongoDB.Bson;
    using MongoDB.Driver;

    using Newtonsoft.Json;

    using SResources = GL.Servers.CoC.Core.Resources;

    internal class Accounts : ConcurrentDictionary<long, Accounts.Account>
    {
        internal ConcurrentDictionary<long, Player> Players;
        internal ConcurrentDictionary<long, Home> Homes;

        /// <summary>
        /// The settings for the <see cref="JsonConvert" /> class.
        /// </summary>
        private readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            TypeNameHandling            = TypeNameHandling.Auto,            MissingMemberHandling   = MissingMemberHandling.Ignore,
            DefaultValueHandling        = DefaultValueHandling.Include,     NullValueHandling       = NullValueHandling.Ignore,
            /*PreserveReferencesHandling  = PreserveReferencesHandling.All,*/   ReferenceLoopHandling   = ReferenceLoopHandling.Ignore,
            Converters                  = new List<JsonConverter> { new DataConverter() },
            Formatting                  = Formatting.None
        };

        private int Seed;

        /// <summary>
        /// Initializes a new instance of the <see cref="Slots.Accounts"/> class.
        /// </summary>
        internal Accounts()
        {
            this.Seed = Constants.Database == DBMS.Mongo ? Mongo.PlayerSeed : MySQL_Backup.GetSeed("Players");

            this.Homes = new ConcurrentDictionary<long, Home>();
            this.Players = new ConcurrentDictionary<long, Player>();
        }

        /// <summary>
        /// Adds the specified player to dictionary.
        /// </summary>
        internal void Add(Player Player)
        {
            if (!this.Players.TryAdd(Player.PlayerID, Player))
            {
                Logging.Error(this.GetType(), "Unable to add the player " + Player.HighID + "-" + Player.LowID + " in list.");
            }
        }

        /// <summary>
        /// Adds the specified home to dictionary.
        /// </summary>
        internal void Add(Home Home)
        {
            if (!this.Homes.TryAdd((long) Home.HighID << 32 | (uint) Home.LowID, Home))
            {
                Logging.Error(this.GetType(), "Unable to add the player " + Home.HighID + "-" + Home.LowID + " in list.");
            }
        }

        /// <summary>
        /// Creates a new account.
        /// </summary>
        internal Account CreateAccount(DBMS Database = Constants.Database)
        {
            int LowID = Interlocked.Increment(ref this.Seed);
            string PassToken = string.Empty;

            for (int i = 0; i < 40; i++)
            {
                PassToken = PassToken + Resources.Random.Next('A', 'Z');
            }

            Player Player = new Player(null, Constants.ServerID, LowID);
            Home Home = new Home(Constants.ServerID, LowID);

            Home.LastSave = LevelFile.StartingHome;

            switch (Database)
            {
                case DBMS.Mongo:
                {
                    Mongo.Players.InsertOneAsync(new Core.Database.Models.Mongo.Players
                    {
                        HighID = Constants.ServerID,
                        LowID = LowID,

                        Player = BsonDocument.Parse(JsonConvert.SerializeObject(Player, this.Settings)),
                        Home = BsonDocument.Parse(JsonConvert.SerializeObject(Home, this.Settings))
                    });

                    break;
                }
            }
            
            this.Add(Player);
            this.Add(Home);

            return new Account(Constants.ServerID, LowID, PassToken, Player, Home);
        }

        /// <summary>
        /// Loads the specified account.
        /// </summary>
        internal Account LoadAccount(int HighID, int LowID, string PassToken, DBMS Database = Constants.Database)
        {
            if (!this.TryGetValue((long) HighID << 32 | (uint) LowID, out Account Account))
            {
                switch (Database)
                {
                    case DBMS.Mongo:
                    {
                        Core.Database.Models.Mongo.Players Save = Mongo.Players.Find(T => T.HighID == HighID && T.LowID == LowID && T.PassToken == PassToken).SingleOrDefault();

                        if (Save != null)
                        {
                            Player Player = this.LoadPlayerFromSave(Save.Player.ToJson());
                            Home Home = this.LoadHomeFromSave(Save.Home.ToJson());

                            if (Player == null || Home == null)
                            {
                                Logging.Error(this.GetType(), "Unable to load account id:" + HighID + "-" + LowID + ".");
                                return new Account(-1, -1, string.Empty, null, null);
                            }

                            Account = new Account(HighID, LowID, PassToken, Player, Home);
                        }

                        break;
                    }
                }
            }

            return Account;
        }

        /// <summary>
        /// Loads the specified account.
        /// </summary>
        internal async Task<Account> LoadAccountAsync(int HighID, int LowID, string PassToken, DBMS Database = Constants.Database, bool Store = true)
        {
            long ID = (long) HighID << 32 | (uint) LowID;

            if (!this.TryGetValue((long) HighID << 32 | (uint)LowID, out Account Account))
            {
                switch (Database)
                {
                    case DBMS.Mongo:
                    {
                        Core.Database.Models.Mongo.Players Save = await Mongo.Players.Find(T => T.HighID == HighID && T.LowID == LowID && T.PassToken == PassToken).SingleOrDefaultAsync();

                        if (Save != null)
                        {
                            if (!this.Players.TryGetValue(ID, out Player Player))
                            {
                                Player = this.LoadPlayerFromSave(Save.Player.ToJson());

                                if (Store)
                                {
                                    this.Add(Player);
                                }
                            }

                            if (!this.Homes.TryGetValue(ID, out Home Home))
                            {
                                Home = this.LoadHomeFromSave(Save.Home.ToJson());

                                if (Store)
                                {
                                    this.Add(Home);
                                }
                            }

                            Account.HighID = Save.HighID;
                            Account.LowID = Save.LowID;
                            Account.PassToken = Save.PassToken;
                            Account.Player = Player;
                            Account.Home = Home;

                            if (Player == null || Home == null)
                            {
                                Logging.Error(this.GetType(), "Unable to load account id:" + HighID + "-" + LowID + ".");
                                return Account;
                            }
                            
                            this.TryAdd(ID, Account);
                        }

                        break;
                    }
                }
            }

            return Account;
        }

        internal async Task<Player> LoadPlayerAsync(int HighID, int LowID, DBMS Database = Constants.Database, bool Store = true)
        {
            long ID = (long) HighID << 32 | (uint) LowID;

            if (!this.Players.TryGetValue(ID, out Player Player))
            {
                switch (Database)
                {
                    case DBMS.Mongo:
                    {
                        Core.Database.Models.Mongo.Players Save = await Mongo.Players.Find(T => T.HighID == HighID && T.LowID == LowID).SingleOrDefaultAsync();

                        if (Save != null)
                        {
                            Player = this.LoadPlayerFromSave(Save.Player.ToJson());

                            if (Store)
                            {
                                this.Add(Player);
                            }

                            if (!this.Homes.TryGetValue(ID, out Home Home))
                            {
                                Home = this.LoadHomeFromSave(Save.Home.ToJson());

                                if (Store)
                                {
                                    this.Add(Home);
                                }
                            }

                            if (Player == null)
                            {
                                Logging.Error(this.GetType(), "Unable to load player id:" + HighID + "-" + LowID + ".");
                                return null;
                            }
                        }

                        break;
                    }
                }
            }

            return Player;
        }

        internal async Task<Home> LoadHomeAsync(int HighID, int LowID, DBMS Database = Constants.Database, bool Store = true)
        {
            long ID = (long) HighID << 32 | (uint)LowID;

            if (!this.Homes.TryGetValue(ID, out Home Home))
            {
                switch (Database)
                {
                    case DBMS.Mongo:
                    {
                        Core.Database.Models.Mongo.Players Save = await Mongo.Players.Find(T => T.HighID == HighID && T.LowID == LowID).SingleOrDefaultAsync();

                        if (Save != null)
                        {
                            Home = this.LoadHomeFromSave(Save.Home.ToJson());

                            if (Store)
                            {
                                this.Add(Home);
                            }

                            if (!this.Players.TryGetValue(ID, out Player Player))
                            {
                                Player = this.LoadPlayerFromSave(Save.Player.ToJson());

                                if (Store)
                                {
                                    this.Add(Player);
                                }
                            }

                            if (Home == null)
                            {
                                Logging.Error(this.GetType(), "Unable to load home id:" + HighID + "-" + LowID + ".");
                                return null;
                            }
                        }

                        break;
                    }
                }
            }

            return Home;
        }

        /// <summary>
        /// Saves the specified player.
        /// </summary>
        internal void SavePlayer(Player Player, DBMS Database = Constants.Database)
        {
            switch (Database)
            {
                case DBMS.Mongo:
                {
                    Mongo.Players.UpdateOneAsync(Save => Save.HighID == Player.HighID && Save.LowID == Player.LowID, Builders<Core.Database.Models.Mongo.Players>.Update.Set(Save => Save.Player, BsonDocument.Parse(JsonConvert.SerializeObject(Player, this.Settings))));

                    break;
                }
            }
        }

        /// <summary>
        /// Saves the specified player.
        /// </summary>
        internal void SaveHome(Home Home, DBMS Database = Constants.Database)
        {
            switch (Database)
            {
                case DBMS.Mongo:
                {
                    Mongo.Players.UpdateOneAsync(Save => Save.HighID == Home.HighID && Save.LowID == Home.LowID, Builders<Core.Database.Models.Mongo.Players>.Update.Set(Save => Save.Player, BsonDocument.Parse(JsonConvert.SerializeObject(Home, this.Settings))));

                    break;
                }
            }
        }

        /// <summary>
        /// Saves homes and players.
        /// </summary>
        internal void Saves(DBMS Database = Constants.Database)
        {
            Player[] Players = this.Players.Values.ToArray();
            Home[] Homes = this.Homes.Values.ToArray();

            Parallel.ForEach(Players, Player =>
            {
                try
                {
                    this.SavePlayer(Player, Database);
                }
                catch (Exception Exception)
                {
                    Logging.Error(this.GetType(), "An error has been throwed when the save of the player id " + Player.HighID + "-" + Player.LowID + ".");
                }
            });

            Parallel.ForEach(Homes, Home =>
            {
                try
                {
                    this.SaveHome(Home, Database);
                }
                catch (Exception Exception)
                {
                    Logging.Error(this.GetType(), "An error has been throwed when the save of the home id " + Home.HighID + "-" + Home.LowID + ".");
                }
            });
        }

        /// <summary>
        /// Loads player from json.
        /// </summary>
        internal Player LoadPlayerFromSave(string JSON)
        {
            Player Player = new Player();
            JsonConvert.PopulateObject(JSON, Player, this.Settings);
            return Player;
        }

        /// <summary>
        /// Loads home from json.
        /// </summary>
        internal Home LoadHomeFromSave(string JSON)
        {
            Home Home = new Home();
            JsonConvert.PopulateObject(JSON, Home, this.Settings);
            return Home;
        }

        internal struct Account
        {
            internal int HighID;
            internal int LowID;
            internal string PassToken;

            internal Player Player;
            internal Home Home;

            /// <summary>
            /// Initializes a new instance of the <see cref="Account"/> structure.
            /// </summary>
            public Account(int HighID, int LowID, string PassToken, Player Player, Home Home)
            {
                this.HighID = HighID;
                this.LowID = LowID;
                this.PassToken = PassToken;
                this.Player = Player;
                this.Home = Home;
            }
        }
    }
}