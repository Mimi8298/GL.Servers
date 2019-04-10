namespace GL.Servers.BB.Logic
{
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.BB.Files.CSV_Logic;
    using GL.Servers.BB.Logic.GameObject;

    internal class Mission
    {
        internal MissionData Data;
        internal Player Player;

        internal bool IsFinished;

        internal int Count;
        internal int RequiredCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="Mission"/>.
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="Player"></param>
        public Mission(Data Data, Player Player)
        {
            this.Data = (MissionData) Data;
            this.Player = Player;

            switch (this.Data.MissionType)
            {
                case 0:
                    this.RequiredCount = this.Data.BuildBuildingCount;
                    break;
                case 4:
                    this.RequiredCount = this.Data.TrainTroops;
                    break;
                default:
                    this.RequiredCount = 1;
                    break;
            }

            if (this.Data.MissionType == 0)
            {
                this.RequiredCount = this.Data.BuildBuildingCount;
            }
            else
                this.RequiredCount = 1;
        }

        /// <summary>
        /// Refreshes the progress of the mission.
        /// </summary>
        internal void RefreshProgress()
        {
            switch (this.Data.MissionType)
            {
                case 0:
                {
                    this.Count = 0;

                    foreach (Building GameObject in this.Player.Home.GameObjectManager.GameObjects[0])
                    {
                        if (GameObject.Data == this.Data.BuildBuildingData)
                        {
                            if (GameObject.UpgradeLevel >= this.Data.BuildBuildingLevel - 1)
                            {
                                this.Count++;
                            }
                        }
                    }
                    
                    break;
                }

                case 4:
                {
                    this.Count = this.Player.UnitsTotalCapacity;

                    break;
                }

                case 11:
                {
                    if (this.Player.NameSetByUser)
                    {
                        this.Count = 1;
                    }

                    break;
                }
            }

            if (this.Count >= this.RequiredCount)
            {
                this.Finished();
            }
        }

        internal void StateChangeConfirmed()
        {
            if (this.Data.MissionType == 10)
            {
                if (this.Count == 0)
                {
                    this.Count = 1;
                }
            }
            else
            {
                if (this.Data.MissionType == 1)
                {
                    if (this.Count == 0)
                    {
                        this.Player.Level.GameMode.StartDefendState(this.Player);
                        this.Count = 1;
                    }
                }
            }
        }

        /// <summary>
        /// Finishes the mission.
        /// </summary>
        internal void Finished()
        {
            if (!this.IsFinished)
            {
                this.IsFinished = true;

                if (!this.Player.MissionCompleted.Contains(this.Data.GlobalID))
                {
                    this.Player.MissionCompleted.Add(this.Data.GlobalID);
                }
            }
        }

        /// <summary>
        /// Ticks this instance.
        /// </summary>
        internal void Tick()
        {
            if (this.Data.MissionType == 1)
            {
                if (this.Count == 1)
                {
                    this.Finished();
                }
            }
            else if (this.Data.MissionType == 2)
            {
                if (this.Count == 1)
                {
                    this.Finished();
                }
            }
        }

        /// <summary>
        /// Destructs this instance.
        /// </summary>
        ~Mission()
        {
            this.Data = null;
            this.Player = null;
        }
    }
}