namespace GL.Servers.CoC.Packets.Commands.Client
{
    using GL.Servers.CoC.Files;
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Logic.Enums;
    using GL.Servers.CoC.Extensions.Game;
    using GL.Servers.CoC.Extensions.Helper;
    using GL.Servers.CoC.Files.CSV_Logic.Logic;

    using GL.Servers.Extensions.Binary;
    
    internal class Buy_Building_Command : Command
    {
        internal int X;
        internal int Y;

        internal BuildingData Data;

        /// <summary>
        /// Gets a value indicating the command type.
        /// </summary>
        internal override int Type
        {
            get
            {
                return 500;
            }
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode(Reader Reader)
        {
            this.X = Reader.ReadInt32();
            this.Y = Reader.ReadInt32();

            this.Data = Reader.ReadData<BuildingData>();

            base.Decode(Reader);
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        internal override void Execute(Level Level)
        {
            if (this.Data != null)
            {
                if (!Level.IsBuildingCapReached(this.Data))
                {
                    BuildingClassData BuildingClassData = (BuildingClassData) CSV.Tables.Get(Gamefile.BuildingClass).GetData(this.Data.BuildingClass);
                    ResourceData ResourceData = (ResourceData) CSV.Tables.Get(Gamefile.Resource).GetData(this.Data.BuildResource);

                    if (BuildingClassData.CanBuy)
                    {
                        if (this.Data.TownHallLevel[0] <= Level.GameObjectManager.TownHall.GetUpgradeLevel() + 1)
                        {
                            if (this.Data.IsWorker)
                            {
                                int Cost;

                                switch (Level.WorkerManager.WorkerCount)
                                {
                                    case 1:
                                        Cost = Globals.WorkerCost2Nd;
                                        break;
                                    case 2:
                                        Cost = Globals.WorkerCost3Rd;
                                        break;
                                    case 3:
                                        Cost = Globals.WorkerCost4Th;
                                        break;
                                    case 4:
                                        Cost = Globals.WorkerCost5Th;
                                        break;

                                    default:
                                        Cost = this.Data.BuildCost[0];
                                        break;
                                }

                                if (Level.Player.HasEnoughDiamonds(Cost))
                                {
                                    Level.Player.UseDiamonds(Cost);
                                    this.StartConstruction(Level);
                                }
                            }

                            if (Level.Player.Resources.GetCountByData(ResourceData) >= this.Data.BuildCost[0])
                            {
                                if (Level.WorkerManager.FreeWorkers > 0)
                                {
                                    Level.Player.Resources.Remove(ResourceData, this.Data.BuildCost[0]);
                                    this.StartConstruction(Level);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Starts the construction of building.
        /// </summary>
        internal void StartConstruction(Level Level)
        {
            Building GameObject = new Building(this.Data, Level);

            GameObject.SetUpgradeLevel(-1);

            GameObject.Position.X = this.X << 9;
            GameObject.Position.Y = this.Y << 9;

            Level.WorkerManager.AllocateWorker(GameObject);

            if (this.Data.GetBuildTime(0) <= 0)
            {
                GameObject.FinishConstruction();
            }
            else
            {
                GameObject.ConstructionTimer = new Timer();
                GameObject.ConstructionTimer.StartTimer(Level.GameMode.Time, this.Data.GetBuildTime(0));
            }

            Level.GameObjectManager.AddGameObject(GameObject, Level.Player.Map);
        }
    }
}