namespace GL.Servers.HD.Core.Database.Models.Mongo
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Players
    {
        [BsonId]
        internal ObjectId _id;

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

        public string GameCenterID
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        public BsonDocument Player
        {
            get;
            set;
        }
    }
}