namespace GL.Clients.BS.Logic.Slots
{
    using GL.Clients.BS.Core;
    using GL.Clients.BS.Core.Database.Models;

    using Newtonsoft.Json;

    internal static class Players
    {
        /// <summary>
        /// The settings for the <see cref="JsonConvert" /> class.
        /// </summary>
        internal static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            TypeNameHandling            = TypeNameHandling.None,            MissingMemberHandling   = MissingMemberHandling.Ignore,
            DefaultValueHandling        = DefaultValueHandling.Include,     NullValueHandling       = NullValueHandling.Include,
            Formatting                  = Formatting.None
        };

        /// <summary>
        /// Gets the player using the specified identifiers.
        /// </summary>
        /// <param name="HighID">The high identifier.</param>
        /// <param name="LowID">The low identifier.</param>
        internal static Profile Get(int HighID, int LowID)
        {
            Profile Profile = null;
            
            using (MySQL Database = new MySQL())
            {
                var Data = Database.Players.Find(HighID, LowID);

                if (Data != null)
                {
                    if (!string.IsNullOrEmpty(Data.Data))
                    {
                        Profile = JsonConvert.DeserializeObject<Profile>(Data.Data, Settings);
                    }
                    else
                    {
                        Logging.Error(typeof(Players), "The data returned wasn't null but empty, at Get(" + HighID + ", " + LowID + ").");
                    }
                }
            }

            return Profile;
        }

        /// <summary>
        /// Saves a new profile to the database.
        /// </summary>
        /// <param name="Profile">The player.</param>
        internal static void New(Profile Profile)
        {
            using (MySQL Database = new MySQL())
            {
                Database.Players.Add(new Core.Database.Models.players
                {
                    HighID  = Profile.HighID,
                    LowID   = Profile.LowID,
                    Data    = JsonConvert.SerializeObject(Profile, Settings)
                });

                Database.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Saves the specified profile in the database.
        /// </summary>
        /// <param name="Profile">The profile.</param>
        internal static void Save(Profile Profile)
        {
            using (MySQL Database = new MySQL())
            {
                var Data        = Database.Players.Find(Profile.HighID, Profile.LowID);

                if (Data != null)
                {
                    Data.HighID = Profile.HighID;
                    Data.LowID  = Profile.LowID;
                    Data.Data   = JsonConvert.SerializeObject(Profile, Settings);
                }
                else
                {
                    Logging.Error(typeof(Players), "The database returned a null value when we tried to get a player.");
                }

                Database.SaveChangesAsync();
            }
        }
    }
}