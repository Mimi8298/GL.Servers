namespace GL.Servers.BB.Logic
{
    using GL.Servers.BB.Core;
    using GL.Servers.BB.Logic.Enums;
    using GL.Servers.BB.Logic.Manager;

    internal class GameMode
    {
        internal Device Device;
        internal Level Level;
        
        internal Time Time;
        internal Timer AttackTimer;
        internal Timer ReplayTimer;

        internal State State;

        internal CommandManager CommandManager;
        
        /// <summary>
        /// Returns the remaining attack seconds.
        /// </summary>
        internal int RemainingAttackSeconds
        {
            get
            {
                if (this.State == State.Attack || this.State == State.Replay)
                {
                    if (this.AttackTimer != null)
                    {
                        return this.AttackTimer.GetRemainingSeconds(this.Time);
                    }

                    return 1;
                }

                return 0;
            }
        }

        /// <summary>
        /// Returns the remaining replay seconds.
        /// </summary>
        internal int RemainingReplaySeconds
        {
            get
            {
                if (this.State == State.Replay)
                {
                    // TODO Implement RemainingReplaySeconds.
                    return 1;
                }

                return 0;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameMode"/> class.
        /// </summary>
        /// <param name="Player"></param>
        public GameMode(Device Device)
        {
            this.Device         = Device;

            this.Time           = new Time();
            this.Level          = new Level(this);
            this.CommandManager = new CommandManager(this.Level);
        }
        
        /// <summary>
        /// Ends the defend state.
        /// </summary>
        internal void EndDefendState()
        {
            if (this.State == State.Defend)
            {
                this.State = State.Home;
                this.Level.DefenseStateEnded();
            }
            else
                Logging.Info(this.GetType(), "EndDefendState() called from invalid state.");
        }

        /// <summary>
        /// Loads direct attack state.
        /// </summary>
        internal void LoadDirectAttackState()
        {
            if (this.State <= 0)
            {
                this.State = State.Attack;
            }
            else
                Logging.Info(this.GetType(), "LoadAttackState() called from invalid state.");
        }

        /// <summary>
        /// Loads home state.
        /// </summary>
        internal void LoadHomeState(int secondsSinceLastSave)
        {
            this.State = State.Home;
            
            this.AttackTimer = null;
            this.ReplayTimer = null;

            this.Level.FastForwardTime(secondsSinceLastSave);
            this.Level.LoadingFinished();
        }

        /// <summary>
        /// Loads live replay state.
        /// </summary>
        internal void LoadLiveReplay()
        {
            if (this.State <= 0)
            {
                this.State = State.Replay;
            }
            else
                Logging.Info(this.GetType(), "LoadLiveReplay() called from invalid state.");
        }

        /// <summary>
        /// Loads matched attack state.
        /// </summary>
        internal void LoadMatchedAttackState()
        {
            if (this.State <= 0)
            {
                this.State = State.Attack;
            }
            else 
                Logging.Info(this.GetType(), "LoadAttackState() called from invalid state.");
        }

        /// <summary>
        /// Loads npc attack state.
        /// </summary>
        internal void LoadNpcAttackState()
        {
            if (this.State <= 0)
            {
                this.State = State.Attack;
            }
            else
                Logging.Info(this.GetType(), "LoadAttackState() called from invalid state.");
        }

        /// <summary>
        /// Loads npc visit state.
        /// </summary>
        internal void LoadNpcVisitState(Player Visit)
        {
            this.LoadVisitState(Visit);
        }

        /// <summary>
        /// Loads replay state.
        /// </summary>
        internal void LoadReplay()
        {
            if (this.State <= 0)
            {
                this.State = State.Replay;
            }
            else
                Logging.Info(this.GetType(), "LoadReplay() called from invalid state.");
        }

        /// <summary>
        /// Loads visit state.
        /// </summary>
        internal void LoadVisitState(Player Visit)
        {
            if (this.State <= 0)
            {
                this.State = State.Visit;
                this.Level.VisitorPlayer = Visit;
            }
            else
                Logging.Info(this.GetType(), "LoadVisitState() called from invalid state.");
        }

        /// <summary>
        /// Starts the defend state.
        /// </summary>
        internal void StartDefendState(Player Attacker)
        {
            if (((int) this.State | 2) == 3)
            {
                this.State = State.Defend;
                this.Level.DefenseStateStarted(Attacker);
            }
            else 
                Logging.Error(this.GetType(), "StartDefendState called from invalid state.");
        }

        /// <summary>
        /// Ticks this instance.
        /// </summary>
        internal void Tick()
        {
            this.Level.Tick();
        }

        /// <summary>
        /// Updates the subTick.
        /// </summary>
        internal void UpdateSubTick(int SubTick)
        {
            if (this.Time.SubTick <= SubTick)
            {
                this.Time.SubTick = SubTick;
                this.Tick();
            }
        }
    }
}