namespace GL.Servers.BS.Logic.Slots
{
    using System;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using GL.Servers.BS.Core;
    using GL.Servers.BS.Core.Database;
    using GL.Servers.BS.Core.Database.Models;

    using GL.Servers.Logic.Enums;

    using SResources = GL.Servers.BS.Core.Resources;

    using Newtonsoft.Json;

    internal class Players : ConcurrentDictionary<long, Player>
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
        /// Initializes a new instance of the <see cref="Players"/> class.
        /// </summary>
        internal Players()
        {
            this.Seed = MySQL_Backup.GetSeed("Players") + 1;
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

            this.Save(Player);
        }

        /// <summary>
        /// Gets the player using the specified identifier in the specified database.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="HighID">The high identifier.</param>
        /// <param name="LowID">The low identifier.</param>
        /// <param name="DBMS">The DBMS.</param>
        /// <param name="Store">if set to <c>true</c> [store].</param>
        internal Player Get(Device Device, int HighID, int LowID, DBMS DBMS = Constants.Database, bool Store = true)
        {
            if (!this.ContainsKey((long) HighID << 32 | (uint) LowID))
            {
                Player Player = null;

                switch (DBMS)
                {
                    case DBMS.MySQL:
                    {
                        using (GBS_MySQL Database = new GBS_MySQL())
                        {
                            var Data = Database.Players.Find(HighID, LowID);

                            if (Data != null)
                            {
                                if (!string.IsNullOrEmpty(Data.Data))
                                {
                                    Player          = JsonConvert.DeserializeObject<Player>(Data.Data, this.Settings);
                                    Player.Device   = Device;
                                    Device.Player   = Player;

                                    if (Store)
                                    {
                                        this.Add(Player);
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
                        string Data = Redis.Players.StringGet(HighID + "-" + LowID).ToString();

                        if (!string.IsNullOrEmpty(Data))
                        {
                            Player          = JsonConvert.DeserializeObject<Player>(Data, this.Settings);
                            Player.Device   = Device;
                            Device.Player   = Player;

                            if (Store)
                            {
                                this.Add(Player);
                            }
                        }

                        break;
                    }

                    case DBMS.Both:
                    {
                        Player = this.Get(Device, HighID, LowID, DBMS.Redis, Store);

                        if (Player == null)
                        {
                            Player = this.Get(Device, HighID, LowID, DBMS.MySQL, Store);

                            if (Player != null)
                            {
                                this.Save(Player, DBMS.Redis);
                            }
                        }

                        break;
                    }

                    case DBMS.File:
                    {
                        if (File.Exists(Directory.GetCurrentDirectory() + "\\Saves\\Players\\" + HighID + "-" + LowID + ".json"))
                        {
                            string JSON = File.ReadAllText(Directory.GetCurrentDirectory() + "\\Saves\\Players\\" + HighID + "-" + LowID + ".json");

                            if (!string.IsNullOrWhiteSpace(JSON))
                            {
                                Player          = JsonConvert.DeserializeObject<Player>(JSON, this.Settings);
                                Player.Device   = Device;
                                Device.Player   = Player;
                            }
                            else
                            {
                                Logging.Error(this.GetType(), "The data returned wasn't null but empty, at Get(" + HighID + ", " + LowID + ", File, " + Store + ").");
                            }
                        }

                        break;
                    }
                }

                return Player;
            }

            return this[LowID];
        }

        /// <summary>
        /// Creates a new player using the specified identifier in the specified database.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="HighID">The high identifier.</param>
        /// <param name="LowID">The low identifier.</param>
        /// <param name="DBMS">The DBMS.</param>
        /// <param name="Store">if set to <c>true</c> [store].</param>
        internal Player New(Device Device, int HighID = Constants.ServerID, int LowID = 0, DBMS DBMS = Constants.Database, bool Store = true)
        {
            Player Player = new Player(Device, HighID, Interlocked.Increment(ref this.Seed));

            // JsonConvert.PopulateObject(Files.Home.Starting_Home, Player.Objects, this.Settings);
            
            for (int i = 0; i < 40; i++)
            {
                char Letter     = (char) SResources.Random.Next('A', 'Z');
                Player.Token    = Player.Token + Letter;
            }

            switch (DBMS)
            {
                case DBMS.MySQL:
                {
                    using (GBS_MySQL Database = new GBS_MySQL())
                    {
                        Database.Players.Add(new Core.Database.Models.Players
                        {
                            HighID  = Player.HighID,
                            LowID   = Player.LowID,
                            Data    = JsonConvert.SerializeObject(Player, this.Settings)
                        });

                        Database.SaveChangesAsync();
                    }

                    if (Store)
                    {
                        this.Add(Player);
                    }

                    break;
                }

                case DBMS.Redis:
                {
                    this.Save(Player, DBMS);

                    if (Store)
                    {
                        this.Add(Player);
                    }

                    break;
                }

                case DBMS.Both:
                {
                    this.Save(Player, DBMS);

                    using (GBS_MySQL Database = new GBS_MySQL())
                    {
                        Database.Players.Add(new Core.Database.Models.Players
                        {
                            HighID  = Player.HighID,
                            LowID   = Player.LowID,
                            Data    = JsonConvert.SerializeObject(Player, this.Settings)
                        });

                        Database.SaveChangesAsync();
                    }

                    if (Store)
                    {
                        this.Add(Player);
                    }

                    break;
                }

                case DBMS.File:
                {
                    if (!File.Exists(Directory.GetCurrentDirectory() + "\\Saves\\Players\\" + Player + ".json"))
                    {
                        File.WriteAllText(Directory.GetCurrentDirectory() + "\\Saves\\Players\\" + Player + ".json", JsonConvert.SerializeObject(Player, this.Settings));
                    }

                    break;
                }
            }

            return Player;
        }

        /// <summary>
        /// Saves the specified player in the specified database.
        /// </summary>
        /// <param name="Player">The player.</param>
        /// <param name="DBMS">The DBMS.</param>
        internal void Save(Player Player, DBMS DBMS = Constants.Database)
        {
            switch (DBMS)
            {
                case DBMS.MySQL:
                {
                    using (GBS_MySQL Database = new GBS_MySQL())
                    {
                        var Data        = Database.Players.Find(Player.HighID, Player.LowID);

                        if (Data != null)
                        {
                            Data.HighID = Player.HighID;
                            Data.LowID  = Player.LowID;
                            Data.Data   = JsonConvert.SerializeObject(Player, this.Settings);
                        }
                        else
                        {
                            Logging.Error(this.GetType(), "The database returned a null value when we tried to get a player.");
                        }

                        Database.SaveChangesAsync();
                    }

                    break;
                }

                case DBMS.Redis:
                {
                    Redis.Players.StringSetAsync(Player.ToString(), JsonConvert.SerializeObject(Player, this.Settings), TimeSpan.FromMinutes(30));
                    break;
                }

                case DBMS.Both:
                {
                    this.Save(Player, DBMS.MySQL);
                    this.Save(Player, DBMS.Redis);
                    break;
                }

                case DBMS.File:
                {
                    File.WriteAllText(Directory.GetCurrentDirectory() + "\\Saves\\Players\\" + Player + ".json", JsonConvert.SerializeObject(Player, this.Settings));
                    break;
                }
            }
        }

        /// <summary>
        /// Deletes the specified player in the specified database.
        /// </summary>
        /// <param name="Player">The player.</param>
        /// <param name="DBMS">The DBMS.</param>
        internal void Delete(Player Player, DBMS DBMS = Constants.Database)
        {
            switch (DBMS)
            {
                case DBMS.MySQL:
                {
                    using (GBS_MySQL Database = new GBS_MySQL())
                    {
                        Core.Database.Models.Players Data = Database.Players.Find(Player.HighID, Player.LowID);

                        if (Data != null)
                        {
                            Database.Players.Remove(Data);
                        }
                        else
                        {
                            Logging.Error(this.GetType(), "Data was null when deleting a player in the MySQL Database.");
                        }

                        Database.SaveChangesAsync();
                    }

                    break;
                }

                case DBMS.Redis:
                {
                    Redis.Players.KeyDeleteAsync(Player.ToString());
                    break;
                }

                case DBMS.Both:
                {
                    this.Delete(Player, DBMS.MySQL);
                    this.Delete(Player, DBMS.Redis);
                    break;
                }

                case DBMS.File:
                {
                    File.Delete(Directory.GetCurrentDirectory() + "\\Saves\\Players\\" + Player + ".json");
                    break;
                }
            }
        }

        /// <summary>
        /// Deletes the specified player in the specified database.
        /// </summary>
        /// <param name="HighID">The high identifier.</param>
        /// <param name="LowID">The low identifier.</param>
        /// <param name="DBMS">The DBMS.</param>
        internal void Delete(int HighID, int LowID, DBMS DBMS = Constants.Database)
        {
            switch (DBMS)
            {
                case DBMS.MySQL:
                {
                    using (GBS_MySQL Database = new GBS_MySQL())
                    {
                        Core.Database.Models.Players Data = Database.Players.Find(HighID, LowID);

                        if (Data != null)
                        {
                            Database.Players.Remove(Data);
                        }
                        else
                        {
                            Logging.Error(this.GetType(), "Data was null when deleting a player in the MySQL Database.");
                        }

                        Database.SaveChangesAsync();
                    }

                    break;
                }

                case DBMS.Redis:
                {
                    Redis.Players.KeyDeleteAsync(HighID + "-" + LowID);
                    break;
                }

                case DBMS.Both:
                {
                    this.Delete(HighID, LowID, DBMS.MySQL);
                    this.Delete(HighID, LowID, DBMS.Redis);
                    break;
                }

                case DBMS.File:
                {
                    File.Delete(Directory.GetCurrentDirectory() + "\\Saves\\Players\\" + HighID + "-" + LowID + ".json");
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
            if (DBMS == DBMS.Both)
            {
                DBMS = DBMS.MySQL;
            }

            Player[] Players = this.Values.ToArray();

            Parallel.ForEach(Players, Player =>
            {
                try
                {
                    this.Save(Player, DBMS);
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