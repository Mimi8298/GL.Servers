namespace GL.Servers.CoC.Logic.Items
{
    using System.Collections.Generic;
    using GL.Servers.CoC.Core;
    using GL.Servers.CoC.Files.CSV_Logic.Logic;
    using GL.Servers.CoC.Logic.Manager;
    using GL.Servers.CoC.Logic.Mode.Enums;

    internal class Mission
    {
        internal Level Level;
        internal MissionData MissionData;

        internal int Completed;
        internal int RequiredCompleted;

        internal bool IsFinished;

        internal int MissionType
        {
            get
            {
                return this.MissionData.MissionType;
            }
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Mission"/> class.
        /// </summary>
        public Mission(MissionData MissionData, Level Level)
        {
            this.Level = Level;
            this.MissionData = MissionData;

            switch (this.MissionType)
            {
                case 0:
                case 5:
                {
                    this.RequiredCompleted = this.MissionData.BuildBuildingCount;
                    break;
                }
                case 4:
                {
                    this.RequiredCompleted = this.MissionData.TrainTroops;
                    break;
                }
                default:
                {
                    this.RequiredCompleted = 1;
                    break;
                }
            }
        }

        internal void AddRewardUnits()
        {
            // this.Level.Player.Units.Add(this.MissionData.RewardCharacterData, this.MissionData.RewardTroopCount);

            if (this.MissionData.RewardCharacterData != null)
            {
                if (this.MissionData.RewardTroopCount > 0)
                {
                    int RewardCount = this.MissionData.RewardTroopCount;
                    List<Component> UnitStorages = this.Level.ComponentManager.FindAll(0, Component => Component.Type == 0);

                    for (int i = UnitStorages.Count; i > 0; i--)
                    {
                        UnitStorageComponent Component = (UnitStorageComponent) ComponentManager.GetClosestComponent(0, 0, UnitStorages);

                        while (Component.CanAddUnit(this.MissionData))
                        {
                            this.Level.Player.Units.Add(this.MissionData.RewardCharacterData, 1);
                            Component.AddUnit(this.MissionData.RewardCharacterData);

                            if (--RewardCount <= 0)
                            {
                                return;
                            }
                        }
                        
                        UnitStorages.Remove(Component);
                    }
                }
            }
        }

        internal void AddRewardResource()
        {
            this.Level.Player.Resources.Add(this.MissionData.RewardResourceData, this.MissionData.RewardResourceCount);
        }

        internal void Finished()
        {
            if (!this.IsFinished)
            {
                this.AddRewardUnits();
                this.AddRewardResource();

                this.Level.Player.Missions.Add(this.MissionData);
            }
            else
                Logging.Info(this.GetType(), "Mission is already finished.");

            this.IsFinished = true;
        }

        internal void RefreshProgress()
        {
            switch (this.MissionType)
            {
                case 0:
                case 5:
                {
                    if (this.Level.GameMode.State == State.Home)
                    {
                        int Count = 0;

                        this.Level.GameObjectManager.GameObjects[0][0].ForEach(GameObject =>
                        {
                            Building Building = (Building) GameObject;

                            if (Building.Data == this.MissionData.BuildBuildingData && Building.GetUpgradeLevel() >= this.MissionData.BuildBuildingLevel)
                            {
                                ++Count;
                            }
                        });

                        this.Completed = Count;
                    }

                    break;
                }
                case 4:
                {
                    this.Completed = this.Level.Player.Units.GetUnitsTotalCapacity();
                    break;
                }
                case 6:
                {
                    if (this.Level.Player.NameSetByUser)
                    {
                        this.Completed = 1;
                    }
                    
                    break;
                }
            }

            if (this.Completed >= this.RequiredCompleted)
            {
                this.Finished();
            }
        }

        internal void StateChangeConfirmed()
        {
            switch (this.MissionType)
            {
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 20:
                case 21:
                {
                    this.Completed = 1;
                    this.Finished();

                    break;
                }

                case 1:
                {
                    if (this.Completed < 1)
                    {
                        this.Completed = 1;
                        this.Level.GameMode.StartDefendState(null);
                    }

                    break;
                }

                // case 16 (Villagers)
                // case 19 (duel_end)
            }
        }
        
        /// <summary>
        /// Ticks this instance.
        /// </summary>
        internal void Tick()
        {
            if (this.MissionType == 1)
            {
                if (this.Level.GameMode.State == State.Home)
                {
                    if (this.Completed == 1)
                    {
                        this.Finished();
                    }
                }
            }
            else if (this.MissionType == 2)
            {
                if (this.Completed == 1)
                {
                    if (this.Level.GameMode.State == State.Attack)
                    {
                        this.Finished();
                    }
                }
            }
        }
    }
}