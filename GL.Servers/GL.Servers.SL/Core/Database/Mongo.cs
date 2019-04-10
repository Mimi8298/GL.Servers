namespace GL.Servers.SL.Core.Database
{
    // using GL.Servers.SL.Core.Database.Models.Mongo;

    using MongoDB.Driver;

    internal class Mongo
    {
        private static IMongoClient Client;
        private static IMongoDatabase Database;

        // internal static IMongoCollection<Clans> Clans;
        // internal static IMongoCollection<Players> Players;

        internal static int ClanSeed
        {
            get
            {
                /* Clans Last = Clans.Find(T => T.HighID == Constants.ServerID).Sort(Builders<Clans>.Sort.Descending(T => T.LowID)).Limit(1).SingleOrDefault();

                if (Last != null)
                {
                    return Last.LowID;
                } */

                return 0;
            }
        }

        internal static int PlayerSeed
        {
            get
            {
                /* Players Last = Players.Find(T => T.HighID == Constants.ServerID).Sort(Builders<Players>.Sort.Descending(T => T.LowID)).Limit(1).SingleOrDefault();

                if (Last != null)
                {
                    return Last.LowID;
                } */

                return 0;
            }
        }

        /// <summary>
        /// Initializes this instance of <see cref="Mongo"/>.
        /// </summary>
        internal static void Initialize()
        {
            Mongo.Client    = new MongoClient("mongodb://127.0.0.1:18800");
            Mongo.Database  = Mongo.Client.GetDatabase("SmashLand");

            // Mongo.Clans     = Mongo.Database.GetCollection<Clans>("Clans");
            // Mongo.Players   = Mongo.Database.GetCollection<Players>("Players");
        }
    }
}