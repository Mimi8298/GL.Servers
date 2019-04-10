namespace GL.Servers.CoC.Packets.Commands.Client
{
    using System;
    using System.Collections.Generic;
    using GL.Servers.CoC.Extensions.Game;
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Logic;
    using GL.Servers.Extensions.Binary;

    internal class Collect_Resources_Command : Command
    {
        private int Id;

        /// <summary>
        /// Gets a value indicating the command type.
        /// </summary>
        internal override int Type
        {
            get
            {
                return 506;
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

        /// <summary>
        /// Executes this instance.
        /// </summary>
        internal override void Execute(Level Level)
        {
            Building Building = (Building) Level.GameObjectManager.Filter.GetGameObjectById(this.Id);

            if (Building != null)
            {
                if (Globals.CollectAllResourcesAtOnce)
                {
                    List<GameObject> Buildings = Level.GameObjectManager.Filter.GetGameObjectsByData(Building.Data);

                    Buildings.ForEach(GameObject =>
                    {
                        Building building = (Building) GameObject;

                        if (!building.Constructing)
                        {
                            ResourceProductionComponent ResourceProductionComponent = building.ResourceProductionComponent;

                            if (ResourceProductionComponent != null)
                            {
                                ResourceProductionComponent.CollectResources();
                            }
                        }
                    });
                }
                else
                {
                    ResourceProductionComponent ResourceProductionComponent = Building.ResourceProductionComponent;

                    if (ResourceProductionComponent != null)
                    {
                        ResourceProductionComponent.CollectResources();
                    }
                }
            }
        }
    }
}