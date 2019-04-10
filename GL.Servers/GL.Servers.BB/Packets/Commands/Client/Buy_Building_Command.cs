namespace GL.Servers.BB.Packets.Commands.Client
{
    using GL.Servers.BB.Extensions.Helper;
    using GL.Servers.BB.Files.CSV_Logic;
    using GL.Servers.BB.Logic;
    using GL.Servers.BB.Logic.GameObject;
    using GL.Servers.Extensions.Binary;
    using GL.Servers.Extensions.List;

    internal class Buy_Building_Command : Command
    {
        internal bool Instant;

        internal int X;
        internal int Y;

        internal BuildingData Data;

        /// <summary>
        /// Gets the command type.
        /// </summary>
        internal override int Type
        {
            get
            {
                return 500;
            }
        }

        /// <summary>
        /// Gets a value indicate whether the command is allowed in attack state.
        /// </summary>
        internal override bool AllowInAttackState
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicate whether the command is allowed in home state.
        /// </summary>
        internal override bool AllowInHomeState
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets a value indicate whether the command is allowed in visit state.
        /// </summary>
        internal override bool AllowInVisitState
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicate whether the command is a debug command.
        /// </summary>
        internal override bool IsDebugCommand
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicate whether the command is a server command.
        /// </summary>
        internal override bool IsServerCommand
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Buy_Building_Command"/> class.
        /// </summary>
        public Buy_Building_Command() : base()
        {
            // Buy_Building_Command.
        }

        /// <summary>
        /// Decodes this command.
        /// </summary>
        /// <param name="reader">The byte stream.</param>
        internal override void Decode(Reader reader)
        {
            this.X = reader.ReadInt32();
            this.Y = reader.ReadInt32();
            this.Data = reader.ReadData<BuildingData>();
            this.Instant = reader.ReadBoolean();

            reader.ReadInt32();

            base.Decode(reader);
        }

        /// <summary>
        /// Encodes this command.
        /// </summary>
        /// <param name="writer">The byte stream.</param>
        internal override void Encode(ByteWriter writer)
        {
            writer.AddInt(this.X);
            writer.AddInt(this.Y);
            writer.AddData(this.Data);
            writer.AddBoolean(this.Instant);

            writer.AddInt(0);

            base.Encode(writer);
        }

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="Level">The level.</param>
        internal override void Execute(Level Level)
        {
            if (this.Data != null)
            {
                if (this.Data.GetBuildingClassData() != null
                    || this.Data.IsArtifact()
                    || this.Data.IsPrototype())
                {
                    ResourceBundle Cost = this.Data.GetBuildCost(0);

                    if (!this.Data.IsArtifact())
                    {
                        if (this.Data.IsPrototype())
                        {
                            // TODO Implement Prototype.
                        }

                        if (this.Instant)
                        {
                            // TODO Implement Instant Buy.
                        }
                        else
                        {
                            if (Level.Player.HasEnoughWorkersAndResources(Cost))
                            {
                                if (Cost.IsPremiumCurrency())
                                {
                                    Level.Player.Diamonds -= Cost.Resources[0].Count;
                                }

                                for (int i = 1; i < 5; i++)
                                {
                                    if (Cost.Resources[i].Count != -1)
                                    {
                                        Level.Player.Resources.Remove(Cost.Resources[i].Data, Cost.Resources[i].Count);
                                    }
                                }

                                this.ExecuteBuild(Level.Player, false, false, false);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Starts build of building.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="isArtifact"></param>
        /// <param name="isArtifactStorage"></param>
        /// <param name="producesPrototype"></param>
        internal void ExecuteBuild(Player player, bool isArtifact, bool isArtifactStorage, bool producesPrototype)
        {
            Building Building = new Building(this.Data, player.Home);

            Building.UpgradeLevel = -1;

            Building.X = this.X << 9;
            Building.Y = this.Y << 9;

            Building.StartConstructing();

            if (isArtifact)
            {
                // TODO Implement Artifact.
            }

            if (isArtifactStorage)
            {
                // TODO Implement Artifact Storage.
            }

            player.Home.GameObjectManager.AddGameObject(Building);

            if (producesPrototype)
            {
                // TODO Implement Production of Prototype.
            }

            if (this.Instant && Building.IsConstructing)
            {
                Building.FinishConstruction();
            }
        }
    }
}