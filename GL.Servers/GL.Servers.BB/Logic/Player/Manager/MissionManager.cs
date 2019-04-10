namespace GL.Servers.BB.Logic.Manager
{
    using GL.Servers.BB.Core;
    using GL.Servers.BB.Files;
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.BB.Files.CSV_Logic;
    using GL.Servers.BB.Files.Enums;
    using GL.Servers.BB.Logic.Enums;

    internal class MissionManager
    {
        internal Level Level;
        internal Mission Mission;

        /// <summary>
        /// Gets a value that indicates the mission data of current tutorial.
        /// </summary>
        internal MissionData CurrentTutorialData
        {
            get
            {
                return this.Mission != null ? this.Mission.Data : null;
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the tutorial has been completed.
        /// </summary>
        internal bool IsTutorialCompleted
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MissionManager"/> class.
        /// </summary>
        /// <param name="Level">The level.</param>
        public MissionManager(Level Level)
        {
            this.Level = Level;
        }

        /// <summary>
        /// Cancels the current tutorial.
        /// </summary>
        internal void CancelCurrentTutorial()
        {
            this.Mission = null;
        }

        /// <summary>
        /// Completes all missions.
        /// </summary>
        internal void DebugCompleteAllMission()
        {
            DataTable Table = CSV.Tables.Get(Gamefile.Mission);

            for (int i = 0; i < Table.Datas.Count; i++)
            {
                MissionData Data = (MissionData) Table.Datas[i];

                if (!this.Level.Player.MissionCompleted.Contains(Data.MissionType))
                {
                    this.Level.Player.MissionCompleted.Add(Data.GlobalID);
                }
            }
        }

        /// <summary>
        /// To call after initialization or after deserialization.
        /// </summary>
        internal void LoadingFinished()
        {
            this.RefreshOpenMission();
        }

        /// <summary>
        /// Refreshes the mission in going.
        /// </summary>
        internal void RefreshOpenMission()
        {
            if (this.Mission == null)
            {
                if (this.Level.State != State.Visit && this.Level.State != State.Attack)
                {
                    foreach (MissionData Data in CSV.Tables.Get(Gamefile.Mission).Datas)
                    {
                        if (!this.Level.Player.MissionCompleted.Contains(Data.GlobalID) && (Data.DependencyData == null || this.Level.Player.MissionCompleted.Contains(Data.DependencyData.GlobalID)))
                        {
                            this.Mission = new Mission(Data, this.Level.Player);
                            this.Mission.RefreshProgress();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Starts the specified mission.
        /// </summary>
        /// <param name="Data">The mission data.</param>
        internal void StartMission(MissionData Data)
        {
            if (this.Mission == null && (this.Level.State == State.Home || this.Level.State == State.Visit))
            {
                this.Mission = new Mission(Data, this.Level.Player);
                this.Mission.RefreshProgress();
            }
        }

        /// <summary>
        /// Ticks this instance.
        /// </summary>
        internal void Tick()
        {
            if (this.Mission != null)
            {
                this.Mission.RefreshProgress();

                if (this.Mission != null)
                {
                    if (this.Mission.IsFinished)
                    {
                        this.Mission = null;
                        this.RefreshOpenMission();
                        this.Tick();
                    }
                    else
                        this.Mission.Tick();
                }

                if (this.Mission != null)
                {
                    Logging.Info(this.GetType(), "Tick() - Current Mission : " + this.Mission.Data.Name + ".");
                }
            }
        }
    }
}