namespace GL.Servers.BB.Logic.Slots
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using GL.Servers.BB.Core;
    using GL.Servers.BB.Core.Database;
    using GL.Servers.BB.Core.Database.Models;

    using GL.Servers.Logic.Enums;

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
            this.Seed = Constants.Database == DBMS.Mongo ? Mongo.ClanSeed : MySQL_Backup.GetSeed("Clans");
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
                    Logging.Error(this.GetType(), Exception.GetType().Name + " when saving the clan " + Clan + " at shutdown.");
                }
            });

            Logging.Info(this.GetType(), "Saved " + Clans.Length + " clans.");
            Console.WriteLine("Saved " + Clans.Length + " clans.");
        }
    }
}