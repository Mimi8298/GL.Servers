namespace GL.Servers.CR.Core.Database
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Threading.Tasks;
    using GL.Servers.CR.Core.Database.Models;
    using MySql.Data.MySqlClient;

    internal class MySqlDatabase : IDatabase
    {
        internal string Credentials;

        /// <summary>
        /// Gets the seed of players.
        /// </summary>
        public int PlayerSeed
        {
            get
            {
                string SQL = "SELECT coalesce(MAX(LowID), 0) FROM Players WHERE `HighID` = " + Constants.ServerID;
                int Seed = -1;

                using (MySqlConnection Conn = new MySqlConnection(this.Credentials))
                {
                    try
                    {
                        Conn.Open();

                        using (MySqlCommand CMD = new MySqlCommand(SQL, Conn))
                        {
                            CMD.Prepare();
                            Seed = Convert.ToInt32(CMD.ExecuteScalar());
                        }
                    }
                    catch (Exception Exception)
                    {
                    }
                }

                return Seed;
            }
        }

        /// <summary>
        /// Gets the seed of alliances.
        /// </summary>
        public int AllianceSeed
        {
            get
            {
                string SQL = "SELECT coalesce(MAX(LowID), 0) FROM Clans WHERE `HighID` = " + Constants.ServerID;
                int Seed = -1;

                using (MySqlConnection Conn = new MySqlConnection(this.Credentials))
                {
                    try
                    {
                        Conn.Open();

                        using (MySqlCommand CMD = new MySqlCommand(SQL, Conn))
                        {
                            CMD.Prepare();
                            Seed = Convert.ToInt32(CMD.ExecuteScalar());
                        }
                    }
                    catch (Exception Exception)
                    {

                    }
                }

                return Seed;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MySqlDatabase"/> class.
        /// </summary>
        public MySqlDatabase()
        {
            foreach (var ConnectionString in ConfigurationManager.ConnectionStrings)
            {
                string Hostname = string.Empty;
                string Username = string.Empty;
                string Password = string.Empty;
                string Database = string.Empty;

                if (ConnectionString.ToString().ToLower().Contains("mysql"))
                {
                    string[] Parameters = ConnectionString.ToString().Split(';');

                    if (Parameters.Length > 0)
                    {
                        foreach (string Parameter in Parameters)
                        {
                            int IndexServer = Parameter.IndexOf("server=");
                            int IndexUser = Parameter.IndexOf("user id=");
                            int IndexPass = Parameter.IndexOf("password=");
                            int IndexBase = Parameter.IndexOf("database=");

                            if (IndexServer > -1)
                            {
                                Hostname = Parameter.Substring(IndexServer, Parameter.Length - IndexServer);
                                Hostname = Hostname.Split('=').Last();
                                Hostname = Hostname.Replace("\"", string.Empty);
                            }

                            if (IndexUser > -1)
                            {
                                Username = Parameter.Substring(IndexUser, Parameter.Length - IndexUser);
                                Username = Username.Split('=').Last();
                                Username = Username.Replace("\"", string.Empty);
                            }

                            if (IndexPass > -1)
                            {
                                Password = Parameter.Substring(IndexPass, Parameter.Length - IndexPass);
                                Password = Password.Split('=').Last();
                                Password = Password.Replace("\"", string.Empty);
                            }

                            if (IndexBase > -1)
                            {
                                Database = Parameter.Substring(IndexBase, Parameter.Length - IndexBase);
                                Database = Database.Split('=').Last();
                                Database = Database.Replace("\"", string.Empty);
                            }
                        }
                    }

                    this.Credentials = "server=" + Hostname + ";user=" + Username + ";password=" + Password + ";database=" + Database + ";";
                }
            }
        }

        /// <summary>
        /// Creates a player document in database.
        /// </summary>
        public void CreatePlayer(int HighID, int LowID, string JSON)
        {
            if (!string.IsNullOrEmpty(JSON))
            {
                using (GRS_MySQL MySQL = new GRS_MySQL())
                {
                    MySQL.Players.Add(new Players
                    {
                        HighID = HighID,
                        LowID = LowID,
                        Data = JSON
                    });

                    MySQL.SaveChanges();
                }
            }
        }

        public void CreateAlliance(int HighID, int LowID, string JSON)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Loads and returns the specified player.
        /// </summary>
        public string LoadPlayer(int HighID, int LowID)
        {
            using (GRS_MySQL MySQL = new GRS_MySQL())
            {
                var Save = MySQL.Players.Find(HighID, LowID);

                if (Save != null)
                {
                    return Save.Data;
                }
            }

            return null;
        }

        /// <summary>
        /// Loads and returns the specified player.
        /// </summary>
        public string LoadPlayer(int HighID, int LowID, string NotUsed)
        {
            using (GRS_MySQL MySQL = new GRS_MySQL())
            {
                var Save = MySQL.Players.Find(HighID, LowID);

                if (Save != null)
                {
                    return Save.Data;
                }
            }

            return null;
        }

        /// <summary>
        /// Loads and returns the specified player.
        /// </summary>
        public async Task<string> LoadPlayerAsync(int HighID, int LowID)
        {
            using (GRS_MySQL MySQL = new GRS_MySQL())
            {
                var Save = await MySQL.Players.FindAsync(HighID, LowID);

                if (Save != null)
                {
                    return Save.Data;
                }
            }

            return null;
        }

        /// <summary>
        /// Loads and returns the specified player.
        /// </summary>
        public async Task<string> LoadPlayerAsync(int HighID, int LowID, string NotUsed)
        {
            using (GRS_MySQL MySQL = new GRS_MySQL())
            {
                var Save = await MySQL.Players.FindAsync(HighID, LowID);

                if (Save != null)
                {
                    return Save.Data;
                }
            }

            return null;
        }

        /// <summary>
        /// Loads and returns the specified alliance.
        /// </summary>
        public string LoadAlliance(int HighID, int LowID)
        {
            using (GRS_MySQL MySQL = new GRS_MySQL())
            {
                var Save = MySQL.Alliances.Find(HighID, LowID);

                if (Save != null)
                {
                    return Save.Data;
                }
            }

            return null;
        }

        /// <summary>
        /// Loads and returns the specified alliance.
        /// </summary>
        public async Task<string> LoadAllianceAsync(int HighID, int LowID)
        {
            using (GRS_MySQL MySQL = new GRS_MySQL())
            {
                var Save = await MySQL.Alliances.FindAsync(HighID, LowID);

                if (Save != null)
                {
                    return Save.Data;
                }
            }

            return null;
        }

        /// <summary>
        /// Saves the specified player.
        /// </summary>
        public async void SavePlayer(int HighID, int LowID, string JSON)
        {
            using (GRS_MySQL MySQL = new GRS_MySQL())
            {
                var Save = await MySQL.Players.FindAsync(HighID, LowID);

                if (Save != null)
                {
                    Save.Data = JSON;
                }

                MySQL.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Saves the specified alliance.
        /// </summary>
        public async void SaveAlliance(int HighID, int LowID, string JSON)
        {
            using (GRS_MySQL MySQL = new GRS_MySQL())
            {
                var Save = await MySQL.Alliances.FindAsync(HighID, LowID);

                if (Save != null)
                {
                    Save.Data = JSON;
                }

                MySQL.SaveChangesAsync();
            }
        }
    }
}