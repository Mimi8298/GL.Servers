namespace GL.Servers.CoC.Logic.Manager
{
    using GL.Servers.CoC.Core;
    using GL.Servers.CoC.Files;
    using GL.Servers.CoC.Logic.Items;
    using GL.Servers.CoC.Logic.Enums;
    using GL.Servers.CoC.Logic.Mode.Enums;
    using GL.Servers.CoC.Files.CSV_Logic.Logic;

    internal class MissionManager
    {
        internal Level Level;
        internal Mission CurrentMission;

        /// <summary>
        /// Initializes a new instance of the <see cref="MissionManager"/> class.
        /// </summary>
        public MissionManager(Level Level)
        {
            this.Level = Level;
        }

        /// <summary>
        /// Initializes the manager.
        /// </summary>
        internal void LoadingFinished()
        {
            this.RefreshOpenMissions();
        }

        /// <summary>
        /// Refreshes the open missions.
        /// </summary>
        private void RefreshOpenMissions()
        {
            if (this.Level.GameMode.State != State.Visit)
            {
                if (this.CurrentMission != null)
                {
                    return;
                }

                foreach (MissionData Data in CSV.Tables.Get(Gamefile.Mission).Datas)
                {
                    if (Data.IsOpenForAvatar(this.Level.Player))
                    {
                        this.CurrentMission = new Mission(Data, this.Level);
                        this.CurrentMission.RefreshProgress();

                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Ticks this instance.
        /// </summary>
        internal void Tick()
        {
            if (this.CurrentMission != null)
            {
                Logging.Info(this.GetType(), "Mission : " + this.CurrentMission.MissionData.Name + ".");

                this.CurrentMission.RefreshProgress();
                
                while (this.CurrentMission.IsFinished)
                {
                    this.CurrentMission = null;
                    this.RefreshOpenMissions();

                    if (this.CurrentMission == null)
                    {
                        return;
                    }
                }

                this.CurrentMission.Tick();
            }
        }
    }
}