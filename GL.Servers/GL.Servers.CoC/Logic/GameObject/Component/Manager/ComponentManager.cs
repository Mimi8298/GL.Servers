namespace GL.Servers.CoC.Logic.Manager
{
    using System;
    using System.Collections.Generic;
    using GL.Servers.CoC.Core;
    using GL.Servers.CoC.Files;
    using GL.Servers.CoC.Files.CSV_Helpers;
    using GL.Servers.CoC.Files.CSV_Logic.Logic;
    using GL.Servers.CoC.Logic.Enums;
    using Math = GL.Servers.CoC.Logic.Math;

    internal class ComponentManager
    {
        internal Level Level;
        internal List<Component>[] Components;

        /// <summary>
        /// Gets a value indicating the total of max housing.
        /// </summary>
        internal int TotalMaxHousing
        {
            get
            {
                int Count = 0;

                this.Components[0].ForEach(Component =>
                {
                    if (Component.Type == 0)
                    {
                        Count += ((UnitStorageComponent) Component).HousingSpace;
                    }
                });

                return Count;
            }
        }

        /// <summary>
        /// Gets a value indicating the max barrack level.
        /// </summary>
        internal int MaxBarrackLevel
        {
            get
            {
                int MaxLevel = -1;

                this.Components[0].ForEach(Component =>
                {
                    Building Building = (Building) Component.Parent;

                    if (Building.BuildingData.IsBarrack)
                    {
                        if (Building.BuildingData.ProducesUnitsOfType == 1)
                        {
                            MaxLevel = Math.Max(MaxLevel, Building.GetUpgradeLevel());
                        }
                    }
                });

                return MaxLevel;
            }
        }

        /// <summary>
        /// Gets a value indicating the max dark barrack level.
        /// </summary>
        internal int MaxDarkBarrackLevel
        {
            get
            {
                int MaxLevel = -1;

                this.Components[0].ForEach(Component =>
                {
                    Building Building = (Building)Component.Parent;

                    if (Building.BuildingData.IsDarkBarrack)
                    {
                        if (Building.BuildingData.ProducesUnitsOfType == 2)
                        {
                            MaxLevel = Math.Max(MaxLevel, Building.GetUpgradeLevel());
                        }
                    }
                });

                return MaxLevel;
            }
        }

        /// <summary>
        /// Gets a value indicating the max spell forge level.
        /// </summary>
        internal int MaxSpellForgeLevel
        {
            get
            {
                int MaxLevel = -1;

                /*
                this.Components[0].ForEach(Component =>
                {
                    Building Building = (Building)Component.Parent;
                    
                    if (Building.BuildingData.ProducesUnitsOfType == 1)
                    {
                        MaxLevel = Math.Max(MaxLevel, Building.GetUpgradeLevel());
                    }
                });
                */

                // TODO Implement MaxSpellForgeLevel.

                return MaxLevel;
            }
        }

        /// <summary>
        /// Gets a value indicating the max dark spell forge level.
        /// </summary>
        internal int MaxDarkSpellForgeLevel
        {
            get
            {
                int MaxLevel = -1;

                /*
                this.Components[0].ForEach(Component =>
                {
                    Building Building = (Building) Component.Parent;
                    
                    if (Building.BuildingData.ProducesUnitsOfType == 2)
                    {
                        MaxLevel = Math.Max(MaxLevel, Building.GetUpgradeLevel());
                    }
                });
                */

                // TODO Implement MaxDarkSpellForgeLevel.

                return MaxLevel;
            }
        }

        /// <summary>
        /// Gets a value indicating the all troop housings.
        /// </summary>
        internal List<GameObject> TroopHousings
        {
            get
            {
                return this.Level.GameObjectManager.GameObjects[0][0].FindAll(T => ((BuildingData) T.Data).IsTrainingHousing);
            }
        }

        /// <summary>
        /// Gets a value indicating the all barracks.
        /// </summary>
        internal List<Building> Barracks
        {
            get
            {
                List<Building> Barracks = new List<Building>(8);

                this.Level.GameObjectManager.GameObjects[0][0].ForEach(GameObject =>
                {
                    Building Building = (Building) GameObject;

                    if (Building.BuildingData.IsBarrack)
                    {
                        if (Building.GetUpgradeLevel() > -1)
                        {
                            Barracks.Add(Building);
                        }
                    }
                });

                return Barracks;
            }
        }

        /// <summary>
        /// Gets a value indicating the all dark barracks.
        /// </summary>
        internal List<Building> DarkBarracks
        {
            get
            {
                List<Building> Barracks = new List<Building>(8);

                this.Level.GameObjectManager.GameObjects[0][0].ForEach(GameObject =>
                {
                    Building Building = (Building)GameObject;

                    if (Building.BuildingData.IsDarkBarrack)
                    {
                        if (Building.GetUpgradeLevel() > -1)
                        {
                            Barracks.Add(Building);
                        }
                    }
                });

                return Barracks;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentManager"/> class.
        /// </summary>
        public ComponentManager(Level Level)
        {
            this.Level      = Level;
            this.Components = new List<Component>[20];
            
            for (int i = 0; i < 20; i++)
            {
                this.Components[i] = new List<Component>(32);
            }
        }

        /// <summary>
        /// Adds component to the list.
        /// </summary>
        internal void AddComponent(Component Component)
        {
            this.Components[Component.Parent.Type].Add(Component);
        }

        /// <summary>
        /// Removes the component of the list.
        /// </summary>
        internal void RemoveComponent(Component Component)
        {
            this.Components[Component.Parent.Type].Remove(Component);
        }

        /// <summary>
        /// Finds all components matched.
        /// </summary>
        internal void FindAll(Predicate<Component> Match)
        {
            List<Component> Components = new List<Component>(16);

            this.Components[0].ForEach(Component =>
            {
                if (Match(Component))
                {
                    Components.Add(Component);
                }
            });

            this.Components[1].ForEach(Component =>
            {
                if (Match(Component))
                {
                    Components.Add(Component);
                }
            });
        }

        /// <summary>
        /// Finds all components matched.
        /// </summary>
        internal List<Component> FindAll(int GameObjectType, Predicate<Component> Match)
        {
            List<Component> Components = new List<Component>(16);

            this.Components[GameObjectType].ForEach(Component =>
            {
                if (Match(Component))
                {
                    Components.Add(Component);
                }
            });

            return Components;
        }

        /// <summary>
        /// Refreshes the resource caps.
        /// </summary>
        internal void RefreshResourceCaps()
        {
            if (this.Level.Player != null)
            {
                Player Player = this.Level.Player;

                Player.ResourceCaps.ForEach(Slot =>
                {
                    Slot.Count = 0;
                });

                this.Components[0].ForEach(Component =>
                {
                    if (Component.Type == 6)
                    {
                        ResourceStorageComponent StorageComponent = (ResourceStorageComponent) Component;

                        foreach (ResourceData Data in CSV.Tables.Get(Gamefile.Resource).Datas)
                        {
                            Player.ResourceCaps.Add(Data, StorageComponent.GetMax(Data.GetID()));
                        }
                    }
                });
            }
            else 
                Logging.Info(this.GetType(), "Unable to refresh resource caps. Player is NULL.");
        }

        internal static Component GetClosestComponent(int X, int Y, List<Component> Components)
        {
            Component Closest = null;
            int ClosestDistance = 0;

            Components.ForEach(Component =>
            {
                if (Closest == null || ClosestDistance > Component.Parent.Position.GetDistanceSquaredHelper(X, Y))
                {
                    Closest = Component;
                }
            });

            return Closest;
        }
    }
}