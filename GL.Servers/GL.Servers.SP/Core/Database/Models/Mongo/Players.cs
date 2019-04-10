namespace GL.Servers.SP.Core.Database.Models.Mongo
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

        /// <summary>
        /// Gets or sets the GameCenter id.
        /// </summary>
        public string GameCenterID
        {
            get;
            set;
        } = string.Empty;

        /// <summary>
        /// Gets or sets the google service id.
        /// </summary>
        public string GoogleID
        {
            get;
            set;
        } = string.Empty;

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