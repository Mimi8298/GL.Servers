namespace GL.Servers.CoC.Packets.Commands.Client
{
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Files.CSV_Logic.Logic;

    using GL.Servers.Extensions.Binary;

    internal class Unlock_Building_Command : Command
    {
        internal int Id;

        /// <summary>
        /// Gets a value indicating the command type.
        /// </summary>
        internal override int Type
        {
            get
            {
                return 520;
            }
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode(Reader Reader)
        {
            this.Id = Reader.ReadInt32();

            base.Decode(Reader);
        }

        internal override void Execute(Level Level)
        {
            Building Building = Level.GameObjectManager.Filter.GetGameObjectById(this.Id) as Building;

            if (Building != null)
            {
                if (Building.Locked)
                {
                    BuildingData Data = Building.BuildingData;

                    if (Data.BuildCost[0] > 0)
                    {
                        ResourceData ResourceData = Data.BuildResourceData;

                        if (Level.Player.Resources.GetCountByData(ResourceData) < Data.BuildCost[0])
                        {
                            return;
                        }

                        Level.Player.Resources.Remove(ResourceData, Data.BuildCost[0]);
                    }

                    Building.Locked = false;
                    Building.SetUpgradeLevel(Building.GetUpgradeLevel());
                }
            }
        }
    }
}