namespace GL.Servers.CoC.Logic.Apis
{
    using System.IO;
    using System.Threading;

    using global::Google.Apis.Auth.OAuth2;
    using global::Google.Apis.Games.v1;
    using global::Google.Apis.Services;
    using global::Google.Apis.Util.Store;
    using GL.Servers.CoC.Logic;
    using Newtonsoft.Json;

    internal class Google
    {
        internal const string GlobalPlayersID = "CgkIgJyry5ITEAIQAQ";
        internal const string GlobalClansID = "CgkIgJyry5ITEAIQBw";
        internal const string LocalPlayersID = "CgkIgJyry5ITEAIQCA";
        internal const string LocalClansID = "CgkIgJyry5ITEAIQCQ";

        [JsonProperty("gg_id")] internal string Identifier;
        [JsonProperty("gg_token")] internal string Token;

        internal UserCredential OCredentials;
        internal GamesService OClient;
        internal Player Player;

        /// <summary>
        /// Initializes a new instance of the <see cref="Google"/> class.
        /// </summary>
        internal Google()
        {
            // Google.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Google"/> class.
        /// </summary>
        /// <param name="_Player">The player.</param>
        internal Google(Player Player)
        {
            this.Player = Player;

            if (this.Filled)
            {
                this.Connect();
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Google"/> is filled.
        /// </summary>
        internal bool Filled
        {
            get
            {
                return !string.IsNullOrEmpty(this.Identifier) && !string.IsNullOrEmpty(this.Token);
            }
        }

        /// <summary>
        /// Gets the credentials.
        /// </summary>
        internal void GetCredentials()
        {
            using (var Stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                this.OCredentials = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(Stream).Secrets, new[]
                {
                    GamesService.Scope.Games, GamesService.Scope.PlusLogin
                }, "GL.Servers.CoC", CancellationToken.None, new FileDataStore("GL.Servers.CoC")).Result;
            }
        }

        /// <summary>
        /// Logs into the YouTube API's Servers.
        /// </summary>
        internal void Login()
        {
            this.OClient = new GamesService(new BaseClientService.Initializer
            {
                HttpClientInitializer = this.OCredentials,
                ApplicationName = "GL.Servers.CoC",
                ApiKey = "AIzaSyAr6oXL0O52iM2y3qx_1edjLls4LAAows8"
            });
        }

        /// <summary>
        /// Connects this instance.
        /// </summary>
        internal void Connect()
        {
            this.GetCredentials();
            this.Login();

            /* var ScoreSubmit = this.OClient.Scores.Submit(Google.GlobalPlayersID, this.Player.Trophies);
            var ScoreResult = ScoreSubmit.Execute(); */
        }
    }
}