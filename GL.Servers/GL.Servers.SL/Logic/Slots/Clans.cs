namespace GL.Servers.SL.Logic.Slots
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using GL.Servers.SL.Core;
    using GL.Servers.SL.Core.Database;
    using GL.Servers.SL.Core.Database.Models.MySQL;

    using GL.Servers.Logic.Enums;

    using SResources = GL.Servers.SL.Core.Resources;

    using Newtonsoft.Json;

    internal class Clans : ConcurrentDictionary<long, Clan>
    {
        /// <summary>
        /// The settings for the <see cref="JsonConvert" /> class.
        /// </summary>
        private readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            TypeNameHandling            = TypeNameHandling.Auto,            MissingMemberHandling   = MissingMemberHandling.Ignore,
            DefaultValueHandling        = DefaultValueHandling.Include,     NullValueHandling       = NullValueHandling.Ignore,
            PreserveReferencesHandling  = PreserveReferencesHandling.All,   ReferenceLoopHandling   = ReferenceLoopHandling.Ignore,
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
        internal void Add(Clan Clan)
        {
            if (this.ContainsKey(Clan.ClanID))
            {
                if (!this.TryUpdate(Clan.ClanID, Clan, Clan))
                {
                    Logging.Error(this.GetType(), "Unsuccessfuly updated the specified clan to the dictionnary.");
                }
            }
            else
            {
                if (!this.TryAdd(Clan.ClanID, Clan))
                {
                    Logging.Error(this.GetType(), "Unsuccessfuly added the specified clan to the dictionnary.");
                }
            }
        }

        /// <summary>
        /// Removes the specified clan.
        /// </summary>
        /// <param name="Clan">The clan.</param>
        internal void Remove(Clan Clan)
        {
            Clan TmpClan;

            if (this.ContainsKey(Clan.ClanID))
            {
                if (!this.TryRemove(Clan.ClanID, out TmpClan))
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
        internal Clan Get(int HighID, int LowID, DBMS DBMS = Constants.Database, bool Store = true)
        {
            if (!this.ContainsKey((long) HighID << 32 | (uint) LowID))
            {
                Clan Clan = null;

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
                                    Clan = new Clan(HighID, LowID);

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
                            Clan = new Clan(HighID, LowID);

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
                }

                return Clan;
            }

            return this[LowID];
        }

        /// <summary>
        /// Gets the range.
        /// </summary>
        /// <param name="Offset">The offset.</param>
        /// <param name="Limit">The limit.</param>
        internal List<Clan> GetRange(int Offset = 0, int Limit = 50, bool Store = true)
        {
            List<Clan> Clans = new List<Clan>(Limit - Offset);
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
                                Clan Clan = new Clan(Data.HighID, Data.LowID);

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
        internal Clan New(int HighID = Constants.ServerID, int LowID = 0, DBMS DBMS = Constants.Database, bool Store = true)
        {
            Clan Clan = new Clan(HighID, Interlocked.Increment(ref this.Seed));

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
            }

            return Clan;
        }

        /// <summary>
        /// Creates a clan with the specified identifier in the specified database.
        /// </summary>
        /// <param name="Clan">The clan.</param>
        /// <param name="DBMS">The DBMS.</param>
        /// <param name="Store">if set to <c>true</c> [store].</param>
        internal Clan New(Clan Clan, DBMS DBMS = Constants.Database, bool Store = true)
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
            }

            return Clan;
        }

        /// <summary>
        /// Saves the specified clan in the specified database.
        /// </summary>
        /// <param name="Clan">The clan.</param>
        /// <param name="DBMS">The DBMS.</param>
        internal void Save(Clan Clan, DBMS DBMS = Constants.Database)
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
            }
        }

        /// <summary>
        /// Saves the specified DBMS.
        /// </summary>
        /// <param name="DBMS">The DBMS.</param>
        internal void Save(DBMS DBMS = Constants.Database)
        {
            Clan[] Clans = this.Values.ToArray();

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