namespace GL.Servers.BB.Logic
{
    using GL.Servers.BB.Logic.Enums;
    using GL.Servers.BB.Logic.GameObject;
    using GL.Servers.BB.Logic.Manager;

    internal class Level
    {
        internal Player Player;
        internal Player VisitorPlayer;

        internal GameMode GameMode;

        internal AchievementManager AchievementManager;
        internal CommandManager CommandManager;
        internal EventManager EventManager;
        internal FootstepManager FootstepManager;
        internal HeroManager HeroManager;
        internal MissionManager MissionManager;
        internal SpellManager SpellManager;
        internal TraderManager TraderManager;

        /// <summary>
        /// Gets a value indicating the state of player.
        /// </summary>
        internal State State
        {
            get
            {
                return this.GameMode != null ? this.GameMode.State : State.None;
            }
        }

        /// <summary>
        /// Gets a value indicating whether tutorial is ongoing.
        /// </summary>
        internal bool IsTutorialOngoing
        {
            get
            {
                return !this.MissionManager.IsTutorialCompleted;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Level"/> class.
        /// </summary>
        public Level(GameMode GameMode)
        {
            this.GameMode           = GameMode;

            this.AchievementManager = new AchievementManager(this);
            this.CommandManager     = new CommandManager(this);
            this.EventManager       = new EventManager(this);
            this.FootstepManager    = new FootstepManager(this);
            this.HeroManager        = new HeroManager(this);
            this.MissionManager     = new MissionManager(this);
            this.SpellManager       = new SpellManager(this);
            this.TraderManager      = new TraderManager(this);
        }

        /// <summary>
        /// Starts the defense.
        /// </summary>
        /// <param name="Attacker">The attacker.</param>
        internal void DefenseStateStarted(Player Attacker)
        {
            this.VisitorPlayer = Attacker;

            foreach (GameObject.GameObject GameObject in this.Player.Home.GameObjectManager.GameObjects[0])
            {
                HitpointComponent HitpointComponent = GameObject.HitpointComponent;

                if (HitpointComponent != null)
                {
                    HitpointComponent.SetHitpoints(HitpointComponent.MaxHitpoints);
                }
            }

            // TODO Simule The Battle.

            this.GameMode.EndDefendState();
        }

        /// <summary>
        /// Stops the defense.
        /// </summary>
        internal void DefenseStateEnded()
        {
            this.VisitorPlayer = null;
        }

        /// <summary>
        /// Forwards the specified time.
        /// </summary>
        /// <param name="seconds">The time in seconds.</param>
        internal void FastForwardTime(int seconds)
        {
            this.Player.Home.FastForwardTime(seconds);
            this.Player.FastForwardTime(seconds);
        }

        /// <summary>
        /// To call after initialization or after deserialization.
        /// </summary>
        internal void LoadingFinished()
        {
            this.Player.Home.GameObjectManager.LoadingFinished();

            this.MissionManager.LoadingFinished();
            this.AchievementManager.RefreshStatus();
            this.FootstepManager.LoadingFinished();
        }

        /// <summary>
        /// Sets the player.
        /// </summary>
        /// <param name="Player">The player.</param>
        internal void SetPlayer(Player Player)
        {
            this.Player = Player;
            this.Player.Level = this;
        }

        /// <summary>
        /// Ticks this instance.
        /// </summary>
        internal void Tick()
        {
            this.Player.Home.GameObjectManager.Tick();

            this.MissionManager.Tick();
            this.AchievementManager.Tick();
            this.FootstepManager.Tick();

            if (this.GameMode.State == State.Home || this.GameMode.State == State.Visit)
            {
                this.Player.Tick();
            }
        }
    }
}