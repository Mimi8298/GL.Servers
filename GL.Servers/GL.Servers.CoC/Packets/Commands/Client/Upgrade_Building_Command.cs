namespace GL.Servers.CoC.Packets.Commands.Client
{
    using GL.Servers.CoC.Core;
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Files.CSV_Logic.Logic;
    using GL.Servers.CoC.Logic;
    using GL.Servers.Extensions.Binary;

    internal class Upgrade_Building_Command : Command
    {
        internal int Id;
        internal bool UseAltResource;

        /// <summary>
        /// Gets a value indicating the command type.
        /// </summary>
        internal override int Type
        {
            get
            {
                return 502;
            }
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode(Reader Reader)
        {
            this.Id              = Reader.ReadInt32();
            this.UseAltResource = Reader.ReadBoolean();

            base.Decode(Reader);
        }

        internal override void Execute(Level Level)
        {
            Building Building = Level.GameObjectManager.Filter.GetGameObjectById(this.Id) as Building;

            if (Building != null)
            {
                if (Building.UpgradeAvailable)
                {
                    BuildingData Data = (BuildingData) Building.Data;
                    ResourceData ResourceData = this.UseAltResource ? Data.AltBuildResourceData : Data.BuildResourceData;
                    
                    if (ResourceData != null)
                    {
                        if (Level.Player.Resources.GetCountByData(ResourceData) >= Data.BuildCost[Building.GetUpgradeLevel() + 1])
                        {
                            if (Level.WorkerManager.FreeWorkers > 0)
                            {
                                Level.Player.Resources.Remove(ResourceData, Data.BuildCost[Building.GetUpgradeLevel() + 1]);
                                Building.StartUpgrade();
                            }
                        }
#if DEBUG
                        else
                            Logging.Error(this.GetType(), "Unable to upgrade the building. You doesn't have enough resources.");
#endif
                    }
                }
#if DEBUG
                else
                    Logging.Error(this.GetType(), "Unable to upgrade the building. Upgrade is not available.");
#endif
            }
#if DEBUG
            else
                Logging.Error(this.GetType(), "Unable to upgrade the building. GameObject is not valid or not exist.");
#endif
        }
    }
}