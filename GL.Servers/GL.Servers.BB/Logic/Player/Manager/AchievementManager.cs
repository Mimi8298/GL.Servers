namespace GL.Servers.BB.Logic.Manager
{
    using GL.Servers.BB.Files;
    using GL.Servers.BB.Files.CSV_Logic;
    using GL.Servers.BB.Files.Enums;
    using GL.Servers.BB.Logic.Enums;

    internal class AchievementManager
    {
        internal Level Level;

        /// <summary>
        /// Creates a new instance of the <see cref="AchievementManager"/> class.
        /// </summary>
        /// <param name="Level">The level.</param>
        public AchievementManager(Level Level)
        {
            this.Level = Level;
        }

        /// <summary>
        /// Increases the building destroyed count for specified building data.
        /// </summary>
        /// <param name="BuildingData">The building data.</param>
        internal void BuildingDestroyed(BuildingData BuildingData)
        {
            foreach (AchievementData Data in CSV.Tables.Get(Gamefile.Achievement).Datas)
            {
                switch (Data.Type)
                {
                    case 4:
                    {
                        if (Data.ActionDataData == BuildingData)
                        {
                            this.IncreaseAchievementProgress(Data, 1);
                        }

                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Increases the total coop mission completed for all coops achievements.
        /// </summary>
        internal void CoopMissionCompleted()
        {
            foreach (AchievementData Data in CSV.Tables.Get(Gamefile.Achievement).Datas)
            {
                if (Data.Type == 11)
                {
                    this.IncreaseAchievementProgress(Data, 1);
                }
            }
        }

        /// <summary>
        /// Increases the total deepsea for all deepsea achievements.
        /// </summary>
        /// <param name="Data"></param>
        internal void DeepseaDone()
        {
            foreach (AchievementData Data in CSV.Tables.Get(Gamefile.Achievement).Datas)
            {
                if (Data.Type == 10)
                {
                    this.IncreaseAchievementProgress(Data, 1);
                }
            }
        }

        /// <summary>
        /// Increases the total gearheart points for all gearheart achievements.
        /// </summary>
        /// <param name="Data"></param>
        internal void GearheartPoints()
        {
            foreach (AchievementData Data in CSV.Tables.Get(Gamefile.Achievement).Datas)
            {
                if (Data.Type == 13)
                {
                    this.IncreaseAchievementProgress(Data, 1);
                }
            }
        }

        /// <summary>
        /// Increases the total of obstacle cleared for all clear obstacles achievements.
        /// </summary>
        /// <param name="Data"></param>
        internal void ObstacleCleared()
        {
            foreach (AchievementData Data in CSV.Tables.Get(Gamefile.Achievement).Datas)
            {
                if (Data.Type == 2)
                {
                    this.IncreaseAchievementProgress(Data, 1);
                }
            }
        }

        /// <summary>
        /// Increases the count of the specified achievement progress.
        /// </summary>
        /// <param name="Data">The data.</param>
        /// <param name="Count">The count.</param>
        internal void IncreaseAchievementProgress(AchievementData Data, int Count)
        {
            if (this.Level.GameMode.State != State.Replay)
            {
                this.Level.Player.AchievementProgresses.Add(Data, Count);
            }
        }

        /// <summary>
        /// Refreshes the specified achievement progress.
        /// </summary>
        /// <param name="Data">The data.</param>
        /// <param name="Count">The count.</param>
        internal void RefreshAchievementProgress(AchievementData Data, int Count)
        {
            if (this.Level.GameMode.State != State.Replay)
            {
                this.Level.Player.AchievementProgresses.Set(Data, Math.Max(Count, this.Level.Player.AchievementProgresses.GetCount(Data)));
            }
        }

        /// <summary>
        /// Refreshes status of all achievements.
        /// </summary>
        internal void RefreshStatus()
        {
            if (this.Level.GameMode.State == State.Home)
            {
                foreach (AchievementData Data in CSV.Tables.Get(Gamefile.Achievement).Datas)
                {
                    switch (Data.Type)
                    {
                        case 0:
                        {
                            this.RefreshAchievementProgress(Data, this.Level.Player.Home.GameObjectManager.GetHighestBuildingLevel((BuildingData) Data.ActionDataData) + 1);

                            break;
                        }

                        case 1:
                        {
                            this.RefreshAchievementProgress(Data, this.Level.Player.Score);

                            break;
                        }

                        case 14:
                        {
                            this.RefreshAchievementProgress(Data, this.Level.Player.PlayerMap.NumExploredRegions - 1);

                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Ticks this instance.
        /// </summary>
        internal void Tick()
        {
            // Tick.
        }
    }
}