namespace GL.Servers.SP.Core.Database.Models.Mongo
{
    using MongoDB.Bson;

    public class Clans
    {
        public ObjectId _id;

        /// <summary>
        /// Gets or sets the high identifier.
        /// </summary>
        public int HighID
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the low identifier.
        /// </summary>
        public int LowID
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        public BsonDocument Data
        {
            get;
            set;
        }
    }
}