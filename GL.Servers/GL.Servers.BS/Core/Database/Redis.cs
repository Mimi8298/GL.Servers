namespace GL.Servers.BS.Core.Database
{
    using GL.Servers.Core.Consoles;
    using GL.Servers.Logic.Enums;
    using GL.Servers.BS.Logic.Enums;

    using Newtonsoft.Json.Linq;

    using StackExchange.Redis;

    internal class Redis
    {
        internal ConnectionMultiplexer Multiplexer;

        internal IServer Server;

        internal static IDatabase Players;
        internal static IDatabase Clans;
        internal static IDatabase Battles;
        internal static IDatabase Leaderboard;

        /// <summary>
        /// Initializes a new instance of the <see cref="Redis"/> class.
        /// </summary>
        internal Redis()
        {
            if (Constants.Database == DBMS.Redis || Constants.Database == DBMS.Both)
            {
                ConfigurationOptions Configuration = new ConfigurationOptions();

                Configuration.EndPoints.Add("163.172.110.110", 6379);

                Configuration.Password              = "Gobelinlal0i1gigaonl0la1llnbLand";
                Configuration.ClientName            = this.GetType().Assembly.FullName;
                Configuration.AbortOnConnectFail    = false;

                Configuration.SyncTimeout           = 7000;
                Configuration.ConnectTimeout        = 7000;
                Configuration.ResponseTimeout       = 7000;

                this.Multiplexer                    = ConnectionMultiplexer.Connect(Configuration, new RedisTextWriter());
                this.Server                         = this.Multiplexer.GetServer(Configuration.EndPoints[0]);

                Redis.Players                       = this.Multiplexer.GetDatabase((int) Database.Players);
                Redis.Clans                         = this.Multiplexer.GetDatabase((int) Database.Clans);
                Redis.Battles                       = this.Multiplexer.GetDatabase((int) Database.Battles);
                Redis.Leaderboard                   = this.Multiplexer.GetDatabase((int) Database.Leaderboard);

                this.Multiplexer.ErrorMessage      += (sender, args) => Logging.Error(this.GetType(), args.Message);

                Redis.Leaderboard.SortedSetAdd("global", null);

                this.Multiplexer.GetSubscriber().Subscribe("clans", OnClanMessage);
                this.Multiplexer.GetSubscriber().Subscribe("players", OnPlayerMessage);
            }
        }

        /// <summary>
        /// Called when a message about a clan is received.
        /// </summary>
        /// <param name="Channel">The redis channel.</param>
        /// <param name="Message">The redis message.</param>
        private void OnClanMessage(RedisChannel Channel, RedisValue Message)
        {
            if (Message.IsNullOrEmpty == false)
            {
                JObject JSON = JObject.Parse(Message.ToString());

                if (JSON.HasValues)
                {
                    if (JSON["action"].ToObject<string>() == "message")
                    {

                    }
                    else if (JSON["action"].ToObject<string>() == "load")
                    {
                        
                    }
                }
                else
                {
                    Logging.Error(this.GetType(), "Error, the received message about a clan in the channel we subscribed is valid but has no values.");
                }
            }
            else
            {
                Logging.Error(this.GetType(), "Error, the received message about a clan in the channel we subscribed is either null or empty.");
            }
        }

        /// <summary>
        /// Called when a message about a player is received.
        /// </summary>
        /// <param name="Channel">The redis channel.</param>
        /// <param name="Message">The redis message.</param>
        private void OnPlayerMessage(RedisChannel Channel, RedisValue Message)
        {
            if (Message.IsNullOrEmpty == false)
            {
                JObject JSON = JObject.Parse(Message.ToString());

                if (JSON.HasValues)
                {
                    if (JSON["action"].ToObject<string>() == "login")
                    {

                    }
                    else if (JSON["action"].ToObject<string>() == "logout")
                    {
                        
                    }
                }
                else
                {
                    Logging.Error(this.GetType(), "Error, the received message about a player in the channel we subscribed is valid but has no values.");
                }
            }
            else
            {
                Logging.Error(this.GetType(), "Error, the received message about a player in the channel we subscribed is either null or empty.");
            }
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        internal void Stop()
        {
            if (Constants.Database == DBMS.Redis || Constants.Database == DBMS.Both)
            {
                this.Multiplexer.Close();
            }
        }
    }
}