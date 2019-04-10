namespace GL.Servers.CR.Logic.Apis
{
    using System.IO;
    using System.Threading;
    using Newtonsoft.Json;

    using global::Google.Apis.Auth.OAuth2;
    using global::Google.Apis.Games.v1;
    using global::Google.Apis.Services;
    using global::Google.Apis.Util.Store;

    internal class Google
    {
        internal const string GlobalPlayersID   = "CgkIgJyry5ITEAIQAQ";
        internal const string GlobalClansID     = "CgkIgJyry5ITEAIQBw";
        internal const string LocalPlayersID    = "CgkIgJyry5ITEAIQCA";
        internal const string LocalClansID      = "CgkIgJyry5ITEAIQCQ";

        internal Player Player;

        [JsonProperty] internal string Identifier;
        [JsonProperty] internal string Token;

        private UserCredential OCredentials;
        private GamesService OClient;

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
        internal Google(Player Player) : this()
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
                this.OCredentials = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(Stream).Secrets,
                    new[] { GamesService.Scope.Games, GamesService.Scope.PlusLogin },
                    "GL.Servers.CR",
                    CancellationToken.None,
                    new FileDataStore("GL.Servers.CR")
                ).Result;
            }
        }

        /// <summary>
        /// Logs into the YouTube API's Servers.
        /// </summary>
        internal void Login()
        {
            this.OClient = new GamesService(new BaseClientService.Initializer
            {
                HttpClientInitializer   = this.OCredentials,
                ApplicationName         = "GL.Servers.CR",
                ApiKey                  = "AIzaSyCT29VE7GHq-Asw2nku4XFvTfZnH8uopHs"
            });
        }

        /// <summary>
        /// Connects this instance.
        /// </summary>
        internal void Connect()
        {
            // this.GetCredentials();
            // this.Login();
        }
    }
}