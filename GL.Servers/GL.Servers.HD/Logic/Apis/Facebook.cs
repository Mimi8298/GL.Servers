using FacebookClient = Facebook.FacebookClient;

namespace GL.Servers.HD.Logic.Apis
{
    using Newtonsoft.Json;

    internal class Facebook
    {
        internal const string ApplicationID         = "815255971920210";
        internal const string ApplicationSecret     = "7fcdd96a31663ca11408061cddd5f8de";
        internal const string ApplicationVersion    = "2.8";

        [JsonProperty("fb_id")]     internal string Identifier;
        [JsonProperty("fb_token")]  internal string Token;

        internal FacebookClient FBClient;
        internal Player Player;

        /// <summary>
        /// Initializes a new instance of the <see cref="Facebook"/> class.
        /// </summary>
        internal Facebook()
        {
            // Facebook.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Facebook"/> class.
        /// </summary>
        /// <param name="_Player">The player.</param>
        internal Facebook(Player Player)
        {
            this.Player = Player;

            if (this.Filled)
            {
                this.Connect();
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Facebook"/> is connected.
        /// </summary>
        internal bool Connected
        {
            get
            {
                return this.Filled && this.FBClient != null;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Facebook"/> is filled.
        /// </summary>
        internal bool Filled
        {
            get
            {
                return !string.IsNullOrEmpty(this.Identifier) && !string.IsNullOrEmpty(this.Token);
            }
        }

        /// <summary>
        /// Connects this instance.
        /// </summary>
        internal void Connect()
        {
            this.FBClient           = new FacebookClient(this.Token);

            this.FBClient.AppId     = Facebook.ApplicationID;
            this.FBClient.AppSecret = Facebook.ApplicationSecret;
            this.FBClient.Version   = Facebook.ApplicationVersion;
        }

        internal object Get(string Path, bool IncludeIdentifier = true)
        {
            if (this.Connected)
            {
                return this.FBClient.Get("https://graph.facebook.com/v" + Facebook.ApplicationVersion + "/" + (IncludeIdentifier ? this.Identifier + '/' + Path : Path));
            }
            else
            {
                return null;
            }
        }
    }
}