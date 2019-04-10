namespace GL.Servers.GS.Core.Database
{
    using System;
    using System.Configuration;
    using System.Linq;

    using MySql.Data.MySqlClient;

    internal class MySQL_Backup
    {
        /// <summary>
        /// Gets the credentials used to log into the MySQL Server.
        /// </summary>
        internal static string Credentials
        {
            get
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
                                int IndexUser   = Parameter.IndexOf("user id=");
                                int IndexPass   = Parameter.IndexOf("password=");
                                int IndexBase   = Parameter.IndexOf("database=");

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

                        return "server=" + Hostname + ";user=" + Username + ";password=" + Password + ";database=" + Database + ";";
                    }
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the latest player identifier.
        /// </summary>
        internal static int GetPlayerSeed()
        {
            string SQL = "SELECT coalesce(MAX(LowID), 0) FROM Players WHERE `HighID` = " + Constants.ServerID;
            int Seed = -1;

            using (MySqlConnection Conn = new MySqlConnection(MySQL_Backup.Credentials))
            {
                try
                {
                    Conn.Open();
                }
                catch (Exception Exception)
                {
                    Handle(Exception);
                }

                using (MySqlCommand CMD = new MySqlCommand(SQL, Conn))
                {
                    CMD.Prepare();
                    Seed = Convert.ToInt32(CMD.ExecuteScalar());
                }
            }

            return Seed;
        }

        /// <summary>
        /// Gets the latest clan identifier.
        /// </summary>
        internal static int GetClanSeed()
        {
            string SQL = "SELECT coalesce(MAX(LowID), 0) FROM Clans WHERE `HighID` = " + Constants.ServerID;
            int Seed = -1;

            using (MySqlConnection Conn = new MySqlConnection(MySQL_Backup.Credentials))
            {
                try
                {
                    Conn.Open();
                }
                catch (MySqlException Exception)
                {
                    Handle(Exception);
                }

                using (MySqlCommand CMD = new MySqlCommand(SQL, Conn))
                {
                    CMD.Prepare();
                    Seed = Convert.ToInt32(CMD.ExecuteScalar());
                }
            }

            return Seed;
        }
        

        /// <summary>
        /// Gets the latest battle identifier.
        /// </summary>
        internal static int GetBattleSeed()
        {
            string SQL = "SELECT coalesce(MAX(LowID), 0) FROM Battles WHERE `HighID` = " + Constants.ServerID;
            int Seed = -1;

            using (MySqlConnection Conn = new MySqlConnection(MySQL_Backup.Credentials))
            {
                try
                {
                    Conn.Open();
                }
                catch (MySqlException Exception)
                {
                    Handle(Exception);
                }

                using (MySqlCommand CMD = new MySqlCommand(SQL, Conn))
                {
                    CMD.Prepare();
                    Seed = Convert.ToInt32(CMD.ExecuteScalar());
                }
            }

            return Seed;
        }
        

        /// <summary>
        /// Gets the latest tournament identifier.
        /// </summary>
        internal static int GetTournamentSeed()
        {
            string SQL = "SELECT coalesce(MAX(LowID), 0) FROM Tournaments WHERE `HighID` = " + Constants.ServerID;
            int Seed = -1;

            using (MySqlConnection Conn = new MySqlConnection(MySQL_Backup.Credentials))
            {
                try
                {
                    Conn.Open();
                }
                catch (MySqlException Exception)
                {
                    Handle(Exception);
                }

                using (MySqlCommand CMD = new MySqlCommand(SQL, Conn))
                {
                    CMD.Prepare();
                    Seed = Convert.ToInt32(CMD.ExecuteScalar());
                }
            }

            return Seed;
        }

        /// <summary>
        /// Handles the specified exception.
        /// </summary>
        /// <param name="Exception">The exception.</param>
        private static void Handle(Exception Exception)
        {
            if (Exception.Message.Contains("not allowed"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("This server is not allowed to connect to the MySQL Server !");
                Console.WriteLine("Server is closing..");
                Console.ResetColor();
            }

            Console.ReadKey(false);
            Environment.Exit(0);
        }
    }
}