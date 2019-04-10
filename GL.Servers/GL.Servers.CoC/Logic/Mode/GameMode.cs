namespace GL.Servers.CoC.Logic.Mode
{
    using GL.Servers.CoC.Core;
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Logic.Manager;
    using GL.Servers.CoC.Logic.Log.Manager;
    using GL.Servers.CoC.Logic.Mode.Enums;

    internal class GameMode
    {
        internal Device Device;
        internal Level Level;

        internal Time Time;
        internal State State;

        internal CommandManager CommandManager;
        internal GameLogManager GameLogManager;

        internal int ShieldTime;
        internal int GuardTime;

        internal int Timestamp;

        /// <summary>
        /// Gets a value indicating whether the device is connected.
        /// </summary>
        internal bool Connected
        {
            get
            {
                if (this.Device != null)
                {
                    return this.Device.Connected;
                }

                return false;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameMode"/> class.
        /// </summary>
        public GameMode(Device Device)
        {
            this.Device = Device;
            this.Time   = new Time();
            this.Level  = new Level(this);
            
            this.CommandManager = new CommandManager(this.Level);
            this.GameLogManager = new GameLogManager(this);
        }
        
        /// <summary>
        /// Loads home state.
        /// </summary>
        /// <param name="Timestamp">The server timestamp.</param>
        /// <param name="SecondsSinceLastSave">The time since last save for fast forward of time.</param>
        internal void LoadHomeState(Home Home, Player Player, int Timestamp, int SecondsSinceLastSave)
        {
            if (this.Level != null)
            {
                this.Level = null;
            }

            this.Time = new Time();
            this.State = State.Attack;
            this.Timestamp = Timestamp;

            this.Level = new Level(this);
            this.Level.SetPlayer(Player);
            this.Level.SetHome(Home);
            this.Level.FastForwardTime(SecondsSinceLastSave);
            this.Level.LoadingFinished();

            this.ShieldTime = Time.GetSecondsInTicks(0);
            this.GuardTime = Time.GetSecondsInTicks(0);
        }

        /// <summary>
        /// Loads the npc attack state.
        /// </summary>
        internal void LoadNpcAttackState(PlayerBase NpcPlayer, Home Home, PlayerBase VisitorPlayer, int Timestamp, int SecondsSinceLastSave)
        {
            if (this.State <= 0)
            {
                if (this.Level != null)
                {
                    this.Level = null;
                }

                this.Time = new Time();
                this.State = State.Attack;
                this.Timestamp = Timestamp;

                this.Level = new Level(this);
                this.Level.SetPlayer(NpcPlayer);
                this.Level.SetHome(Home);
                this.Level.SetVistorPlayer(VisitorPlayer);
                this.Level.FastForwardTime(SecondsSinceLastSave);
                this.Level.LoadingFinished();
            }
        }

        /// <summary>
        /// Puts at end the defend state.
        /// </summary>
        internal void EndDefendState()
        {
            if (this.State == State.Defend)
            {
                this.State = State.Home;
                this.Level.DefenseStateEnded();
            }
            else 
                Logging.Error(this.GetType(), "EndDefendState called from invalid state");
        }

        /// <summary>
        /// Starts the defend state.
        /// </summary>
        internal void StartDefendState(Player Attacker)
        {
            if (this.State == State.Home || this.State == State.Defend)
            {
                this.State = State.Defend;
                this.Level.DefenseStateStarted(Attacker);
            }
            else
                Logging.Error(this.GetType(), "StartDefendState called from invalid state");
        }
    }
}