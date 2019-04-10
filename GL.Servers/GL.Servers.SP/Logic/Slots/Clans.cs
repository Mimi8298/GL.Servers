namespace GL.Servers.SP.Logic.Slots
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using GL.Servers.SP.Core;
    using GL.Servers.SP.Logic.Clan;
    using GL.Servers.SP.Core.Database;
    using GL.Servers.SP.Core.Database.Models.MySQL;

    using GL.Servers.Logic.Enums;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using SResources = GL.Servers.SP.Core.Resources;

    using Newtonsoft.Json;

    internal class Clans : ConcurrentDictionary<long, Alliance>
    {
        /// <summary>
        /// The settings for the <see cref="JsonConvert" /> class.
        /// </summary>
        private readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            TypeNameHandling            = TypeNameHandling.Auto,            MissingMemberHandling   = MissingMemberHandling.Ignore,
            DefaultValueHandling        = DefaultValueHandling.Include,     NullValueHandling       = NullValueHandling.Ignore,
            ReferenceLoopHandling       = ReferenceLoopHandling.Ignore,
            Formatting                  = Formatting.None
        };

        private int Seed;

        /// <summary>
        /// Initializes a new instance of the <see cref="Clans"/> class.
        /// </summary>
        internal Clans()
        {
            if (Constants.Database == DBMS.Mongo)
            {
                this.Seed = Mongo.ClanSeed;

                foreach (Core.Database.Models.Mongo.Clans Save in Mongo.Clans.Find(T => true).ToList())
                {
                    if (Save != null)
                    {
                        Alliance Alliance = new Alliance(Save.HighID, Save.LowID);

                        JsonConvert.PopulateObject(Save.Data.ToJson(), Alliance, this.Settings);

                        this.Add(Alliance);
                    }
                }

                Logging.Info(this.GetType(), this.Count + " alliances loaded in-memory.");
            }
            else
            {
                this.Seed = MySQL_Backup.GetSeed("Clans");
                this.GetRange();
            }
        }

        /// <summary>
        /// Adds the specified clan.
        /// </summary>
        /// <param name="Clan">The clan.</param>
        internal void Add(Alliance Clan)
        {
            if (this.ContainsKey(Clan.AllianceID))
            {
                if (!this.TryUpdate(Clan.AllianceID, Clan, Clan))
                {
                    Logging.Error(this.GetType(), "Unsuccessfuly updated the specified clan to the dictionnary.");
                }
            }
            else
            {
                if (!this.TryAdd(Clan.AllianceID, Clan))
                {
                    Logging.Error(this.GetType(), "Unsuccessfuly added the specified clan to the dictionnary.");
                }
            }
        }

        /// <summary>
        /// Removes the specified clan.
        /// </summary>
        /// <param name="Clan">The clan.</param>
        internal void Remove(Alliance Clan)
        {
            Alliance TmpClan;

            if (this.ContainsKey(Clan.AllianceID))
            {
                if (!this.TryRemove(Clan.AllianceID, out TmpClan))
                {
                    Logging.Error(this.GetType(), "Unsuccessfuly removed the specified clan from the dictionnary.");
                }
                else
                {
                    if (!TmpClan.Equals(Clan))
                    {
                        Logging.Error(this.GetType(), "We successfully removed a clan from the list but the returned clan was not equal to our clan.");
                    }
                }
            }

            this.Save(Clan);
        }

        /// <summary>
        /// Gets the clan using the specified identifier in the specified database.
        /// </summary>
        /// <param name="HighID">The high identifier.</param>
        /// <param name="LowID">The low identifier.</param>
        /// <param name="DBMS">The DBMS.</param>
        /// <param name="Store">if set to <c>true</c> [store].</param>
        internal async Task<Alliance> GetAsync(int HighID, int LowID, DBMS DBMS = Constants.Database, bool Store = true)
        {
            long Id = (long) HighID << 32 | (uint) LowID;

            if (!this.TryGetValue(Id, out Alliance Clan))
            {
                switch (DBMS)
                {
                    case DBMS.MySQL:
                    {
                        using (GBS_MySQL Database = new GBS_MySQL())
                        {
                            var Data = await Database.Clans.FindAsync(HighID, LowID);

                            if (Data != null)
                            {
                                if (!string.IsNullOrEmpty(Data.Data))
                                {
                                    Clan = new Alliance(HighID, LowID);

                                    JsonConvert.PopulateObject(Data.Data, Clan, this.Settings);

                                    if (Store)
                                    {
                                        this.Add(Clan);
                                    }
                                }
                                else
                                {
                                    Logging.Error(this.GetType(), "The data returned wasn't null but empty, at Get(" + HighID + ", " + LowID + ", MySQL, " + Store + ").");
                                }
                            }
                        }

                        break;
                    }

                    case DBMS.Redis:
                    {
                        var Value = await Redis.Clans.StringGetAsync(HighID + "-" + LowID);
                        string Data = Value.ToString();

                        if (!string.IsNullOrEmpty(Data))
                        {
                            Clan = new Alliance(HighID, LowID);

                            JsonConvert.PopulateObject(Data, Clan, this.Settings);

                            if (Store)
                            {
                                this.Add(Clan);
                            }
                        }

                        break;
                    }

                    case DBMS.Both:
                    {
                        Clan = await this.GetAsync(HighID, LowID, DBMS.Redis, Store);

                        if (Clan == null)
                        {
                            Clan = await this.GetAsync(HighID, LowID, DBMS.MySQL, Store);

                            if (Clan != null)
                            {
                                this.Save(Clan, DBMS.Redis);
                            }
                        }

                        break;
                    }
                    case DBMS.Mongo:
                    {
                        Core.Database.Models.Mongo.Clans Save = await Mongo.Clans.Find(T => T.HighID == HighID && T.LowID == LowID).Limit(1).SingleOrDefaultAsync();

                        if (Save != null)
                        {
                            Clan = new Alliance(HighID, LowID);

                            JsonConvert.PopulateObject(Save.Data.ToJson(), Clan, this.Settings);

                            if (Store)
                            {
                                this.Add(Clan);
                            }
                        }

                        break;
                    }
                }
            }

            return Clan;
        }


        /// <summary>
        /// Gets the clan using the specified identifier in the specified database.
        /// </summary>
        /// <param name="HighID">The high identifier.</param>
        /// <param name="LowID">The low identifier.</param>
        /// <param name="DBMS">The DBMS.</param>
        /// <param name="Store">if set to <c>true</c> [store].</param>
        internal Alliance Get(int HighID, int LowID, DBMS DBMS = Constants.Database, bool Store = true)
        {
            long Id = (long)HighID << 32 | (uint)LowID;

            if (!this.TryGetValue(Id, out Alliance Clan))
            {
                switch (DBMS)
                {
                    case DBMS.MySQL:
                    {
                        using (GBS_MySQL Database = new GBS_MySQL())
                        {
                            var Data = Database.Clans.Find(HighID, LowID);

                            if (Data != null)
                            {
                                if (!string.IsNullOrEmpty(Data.Data))
                                {
                                    Clan = new Alliance(HighID, LowID);

                                    JsonConvert.PopulateObject(Data.Data, Clan, this.Settings);

                                    if (Store)
                                    {
                                        this.Add(Clan);
                                    }
                                }
                                else
                                {
                                    Logging.Error(this.GetType(), "The data returned wasn't null but empty, at Get(" + HighID + ", " + LowID + ", MySQL, " + Store + ").");
                                }
                            }
                        }

                        break;
                    }

                    case DBMS.Redis:
                    {
                        string Data = Redis.Clans.StringGet(HighID + "-" + LowID).ToString();

                        if (!string.IsNullOrEmpty(Data))
                        {
                            Clan = new Alliance(HighID, LowID);

                            JsonConvert.PopulateObject(Data, Clan, this.Settings);

                            if (Store)
                            {
                                this.Add(Clan);
                            }
                        }

                        break;
                    }

                    case DBMS.Both:
                    {
                        Clan = this.Get(HighID, LowID, DBMS.Redis, Store);

                        if (Clan == null)
                        {
                            Clan = this.Get(HighID, LowID, DBMS.MySQL, Store);

                            if (Clan != null)
                            {
                                this.Save(Clan, DBMS.Redis);
                            }
                        }

                        break;
                    }
                    case DBMS.Mongo:
                    {
                        Core.Database.Models.Mongo.Clans Save = Mongo.Clans.Find(T => T.HighID == HighID && T.LowID == LowID).Limit(1).SingleOrDefault();

                        if (Save != null)
                        {
                            Clan = new Alliance(HighID, LowID);

                            JsonConvert.PopulateObject(Save.Data.ToJson(), Clan, this.Settings);

                            if (Store)
                            {
                                this.Add(Clan);
                            }
                        }

                        break;
                    }
                }
            }

            return Clan;
        }

        /// <summary>
        /// Gets the range.
        /// </summary>
        /// <param name="Offset">The offset.</param>
        /// <param name="Limit">The limit.</param>
        internal List<Alliance> GetRange(int Offset = 0, int Limit = 50, bool Store = true)
        {
            List<Alliance> Clans = new List<Alliance>(Limit - Offset);
            DbSqlQuery<Core.Database.Models.MySQL.Clans> SqlClans;

            using (GBS_MySQL MySQL = new GBS_MySQL())
            {
                SqlClans = MySQL.Clans.SqlQuery("SELECT * FROM Clans LIMIT " + Offset + ", " + Limit + ";");

                if (SqlClans != null)
                {
                    if (SqlClans.Any())
                    {
                        foreach (Core.Database.Models.MySQL.Clans Data in SqlClans)
                        {
                            if (!string.IsNullOrEmpty(Data.Data))
                            {
                                Alliance Clan = new Alliance(Data.HighID, Data.LowID);

                                JsonConvert.PopulateObject(Data.Data, Clan, this.Settings);
                                /*
                                if (Store && Clan.Members.Entries.Count > 0)
                                {
                                    this.Add(Clan);
                                }
                                */
                                Clans.Add(Clan);
                            }
                        }
                    }
                }
            }

            return Clans;
        }

        /// <summary>
        /// Creates a clan with the specified identifier in the specified database.
        /// </summary>
        /// <param name="HighID">The high identifier.</param>
        /// <param name="LowID">The low identifier.</param>
        /// <param name="DBMS">The DBMS.</param>
        /// <param name="Store">if set to <c>true</c> [store].</param>
        internal Alliance New(int HighID = Constants.ServerID, int LowID = 0, DBMS DBMS = Constants.Database, bool Store = true)
        {
            Alliance Clan = new Alliance(HighID, Interlocked.Increment(ref this.Seed));

            switch (DBMS)
            {
                case DBMS.MySQL:
                {
                    using (GBS_MySQL Database = new GBS_MySQL())
                    {
                        Database.Clans.Add(new Core.Database.Models.MySQL.Clans
                        {
                            HighID  = Clan.HighID,
                            LowID   = Clan.LowID,
                            Data    = JsonConvert.SerializeObject(Clan, this.Settings)
                        });

                        Database.SaveChangesAsync();
                    }

                    if (Store)
                    {
                        this.Add(Clan);
                    }

                    break;
                }

                case DBMS.Redis:
                {
                    Redis.Clans.StringSetAsync(Clan.ToString(), JsonConvert.SerializeObject(Clan, this.Settings), TimeSpan.FromMinutes(15));

                    if (Store)
                    {
                        this.Add(Clan);
                    }

                    break;
                }

                case DBMS.Both:
                {
                    this.New(Clan.HighID, Clan.LowID, DBMS.MySQL, Store);
                    this.New(Clan.HighID, Clan.LowID, DBMS.Redis, Store);
                    break;
                }

                case DBMS.Mongo:
                {
                    Mongo.Clans.InsertOneAsync(new Core.Database.Models.Mongo.Clans
                    {
                        HighID = Clan.HighID,
                        LowID = Clan.LowID,
                        Data = BsonDocument.Parse(JsonConvert.SerializeObject(Clan, this.Settings))
                    });

                    if (Store)
                    {
                        this.Add(Clan);
                    }

                    break;
                }
            }

            return Clan;
        }

        /// <summary>
        /// Creates a clan with the specified identifier in the specified database.
        /// </summary>
        /// <param name="Clan">The clan.</param>
        /// <param name="DBMS">The DBMS.</param>
        /// <param name="Store">if set to <c>true</c> [store].</param>
        internal Alliance New(Alliance Clan, DBMS DBMS = Constants.Database, bool Store = true)
        {
            if (Clan.LowID == 0)
            {
                Clan.LowID = Interlocked.Increment(ref this.Seed);
            }

            switch (DBMS)
            {
                case DBMS.MySQL:
                {
                    using (GBS_MySQL Database = new GBS_MySQL())
                    {
                        Database.Clans.Add(new Core.Database.Models.MySQL.Clans
                        {
                            HighID  = Clan.HighID,
                            LowID   = Clan.LowID,
                            Data    = JsonConvert.SerializeObject(Clan, this.Settings)
                        });

                        Database.SaveChangesAsync();
                    }

                    if (Store)
                    {
                        this.Add(Clan);
                    }

                    break;
                }

                case DBMS.Redis:
                {
                    Redis.Clans.StringSetAsync(Clan.ToString(), JsonConvert.SerializeObject(Clan, this.Settings), TimeSpan.FromMinutes(15));

                    if (Store)
                    {
                        this.Add(Clan);
                    }

                    break;
                }

                case DBMS.Both:
                {
                    this.New(Clan, DBMS.Redis, Store);
                    this.New(Clan, DBMS.MySQL, Store);
                    break;
                }

                case DBMS.Mongo:
                {
                    Mongo.Clans.InsertOneAsync(new Core.Database.Models.Mongo.Clans
                    {
                        HighID = Clan.HighID,
                        LowID = Clan.LowID,
                        Data = BsonDocument.Parse(JsonConvert.SerializeObject(Clan, this.Settings))
                    });

                    if (Store)
                    {
                        this.Add(Clan);
                    }

                    break;
                }
            }

            return Clan;
        }

        /// <summary>
        /// Saves the specified clan in the specified database.
        /// </summary>
        /// <param name="Clan">The clan.</param>
        /// <param name="DBMS">The DBMS.</param>
        internal void Save(Alliance Clan, DBMS DBMS = Constants.Database)
        {
            switch (DBMS)
            {
                case DBMS.MySQL:
                {
                    using (GBS_MySQL Database = new GBS_MySQL())
                    {
                        Core.Database.Models.MySQL.Clans Data = Database.Clans.Find(Clan.HighID, Clan.LowID);

                        if (Data != null)
                        {
                            Data.Data = JsonConvert.SerializeObject(Clan, this.Settings);
                        }

                        Database.SaveChangesAsync();
                    }

                    break;
                }

                case DBMS.Redis:
                {
                    Redis.Clans.StringSetAsync(Clan.ToString(), JsonConvert.SerializeObject(Clan, this.Settings), TimeSpan.FromMinutes(15));

                    break;
                }

                case DBMS.Both:
                {
                    this.Save(Clan, DBMS.MySQL);
                    this.Save(Clan, DBMS.Redis);
                    break;
                }

                case DBMS.Mongo:
                {
                    Mongo.Clans.UpdateOneAsync(T => T.HighID == Clan.HighID && T.LowID == Clan.LowID, Builders<Core.Database.Models.Mongo.Clans>.Update.Set(T => T.Data, BsonDocument.Parse(JsonConvert.SerializeObject(Clan, this.Settings))));

                    break;
                }
            }
        }

        /// <summary>
        /// Saves the specified DBMS.
        /// </summary>
        /// <param name="DBMS">The DBMS.</param>
        internal void Save(DBMS DBMS = Constants.Database)
        {
            Alliance[] Clans = this.Values.ToArray();

            Parallel.ForEach(Clans, Clan =>
            {
                try
                {
                    this.Save(Clan, DBMS);
                }
                catch (Exception Exception)
                {
                    SResources.Logger.Error(Exception, "Did not successed to save the clan " + Clan + " at shutdown.");
                }
            });

            Logging.Info(this.GetType(), "Saved " + Clans.Length + " clans.");
            Console.WriteLine("Saved " + Clans.Length + " clans.");
        }
    }
}