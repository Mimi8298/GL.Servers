namespace GL.Servers.CR.Logic.Slots
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using GL.Servers.CR.Core;

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
            Formatting                  = Formatting.None
        };

        private int Seed;

        /// <summary>
        /// Initializes a new instance of the <see cref="Clans"/> class.
        /// </summary>
        internal Clans()
        {
            this.Seed = Resources.Database.AllianceSeed;
        }

        /// <summary>
        /// Adds the specified clan.
        /// </summary>
        /// <param name="Clan">The clan.</param>
        internal void Add(Alliance Alliance)
        {
            if (this.ContainsKey(Alliance.AllianceID))
            {
                if (!this.TryUpdate(Alliance.AllianceID, Alliance, Alliance))
                {
                    Logging.Error(this.GetType(), "Unsuccessfuly updated the specified clan to the dictionnary.");
                }
            }
            else
            {
                if (!this.TryAdd(Alliance.AllianceID, Alliance))
                {
                    Logging.Error(this.GetType(), "Unsuccessfuly added the specified clan to the dictionnary.");
                }
            }
        }

        /// <summary>
        /// Removes the specified clan.
        /// </summary>
        /// <param name="Alliance">The clan.</param>
        internal void Remove(Alliance Alliance)
        {
            Alliance TmpClan;

            if (this.ContainsKey(Alliance.AllianceID))
            {
                if (!this.TryRemove(Alliance.AllianceID, out TmpClan))
                {
                    Logging.Error(this.GetType(), "Unsuccessfuly removed the specified clan from the dictionnary.");
                }
                else
                {
                    if (!TmpClan.Equals(Alliance))
                    {
                        Logging.Error(this.GetType(), "We successfully removed a clan from the list but the returned clan was not equal to our clan.");
                    }
                }
            }

            this.Save(Alliance);
        }

        /// <summary>
        /// Gets the clan using the specified identifier in the specified database.
        /// </summary>
        /// <param name="HighID">The high identifier.</param>
        /// <param name="LowID">The low identifier.</param>
        /// <param name="Store">if set to <c>true</c> [store].</param>
        internal Alliance GetAlliance(int HighID, int LowID, bool Store = true)
        {
            long ID = HighID << 32 | (uint) LowID;

            if (!this.TryGetValue(ID, out Alliance Alliance))
            {
                string JSON;

                if (Resources.CacheSystem == null || (JSON = Resources.Database.LoadAlliance(HighID, LowID)) == null)
                {
                    JSON = Resources.Database.LoadPlayer(HighID, LowID);
                }

                if (!string.IsNullOrEmpty(JSON))
                {
                    Alliance = JsonConvert.DeserializeObject<Alliance>(JSON, this.Settings);

                    if (Store)
                    {
                        this.Add(Alliance);
                    }
                }
            }

            return Alliance;
        }
        
        /// <summary>
        /// Creates a clan with the specified identifier in the specified database.
        /// </summary>
        /// <param name="HighID">The high identifier.</param>
        /// <param name="LowID">The low identifier.</param>
        /// <param name="Store">if set to <c>true</c> [store].</param>
        internal Alliance Create()
        {
            Alliance Alliance = new Alliance(Constants.ServerID, Interlocked.Increment(ref this.Seed));

            string JSON = JsonConvert.SerializeObject(Alliance, this.Settings);

            if (Resources.CacheSystem != null)
            {
                Resources.CacheSystem.CreateAlliance(Alliance.HighID, Alliance.LowID, JSON);
            }

            Resources.Database.SaveAlliance(Alliance.HighID, Alliance.LowID, JSON);

            return Alliance;
        }

        /// <summary>
        /// Saves the specified clan in the specified database.
        /// </summary>
        /// <param name="Alliance">The clan.</param>
        internal void Save(Alliance Alliance)
        {
            string JSON = JsonConvert.SerializeObject(Alliance, this.Settings);

            if (Resources.CacheSystem != null)
            {
                Resources.CacheSystem.SaveAlliance(Alliance.HighID, Alliance.LowID, JSON);
            }

            Resources.Database.SaveAlliance(Alliance.HighID, Alliance.LowID, JSON);
        }

        /// <summary>
        /// Saves the specified DBMS.
        /// </summary>
        internal void Save()
        {
            Alliance[] Clans = this.Values.ToArray();

            Parallel.ForEach(Clans, Clan =>
            {
                try
                {
                    this.Save(Clan);
                }
                catch (Exception Exception)
                {
                    Resources.Logger.Error(Exception, "Did not successed to save the clan " + Clan + " at shutdown.");
                }
            });

            Logging.Info(this.GetType(), "Saved " + Clans.Length + " clans.");
        }
    }
}