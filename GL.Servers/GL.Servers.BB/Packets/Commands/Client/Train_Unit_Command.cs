namespace GL.Servers.BB.Packets.Commands.Client
{
    using GL.Servers.BB.Extensions.Helper;
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.BB.Files.CSV_Logic;
    using GL.Servers.BB.Logic;
    using GL.Servers.BB.Logic.GameObject;
    using GL.Servers.Extensions.List;
    using GL.Servers.Extensions.Binary;

    internal class Train_Unit_Command : Command
    {
        internal int BoatID;
        internal int UnitType;

        internal Data UnitData;

        /// <summary>
        /// Gets a value indicating the command type.
        /// </summary>
        internal override int Type
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the command is allowed in attack state.
        /// </summary>
        internal override bool AllowInAttackState
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the command is allowed in home state.
        /// </summary>
        internal override bool AllowInHomeState
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the command is allowed in visit state.
        /// </summary>
        internal override bool AllowInVisitState
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the command is a debug command.
        /// </summary>
        internal override bool IsDebugCommand
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the command is a server command.
        /// </summary>
        internal override bool IsServerCommand
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Train_Unit_Command"/> class.
        /// </summary>
        public Train_Unit_Command() : base()
        {
            // Train_Unit_Command.
        }

        /// <summary>
        /// Decodes this command.
        /// </summary>
        /// <param name="reader"></param>
        internal override void Decode(Reader reader)
        {
            this.BoatID = reader.ReadInt32();
            this.UnitType = reader.ReadInt32();

            if (this.UnitType != 0)
            {
                this.UnitData = reader.ReadData<SpellData>();
            }
            else
                this.UnitData = reader.ReadData<CharacterData>();

            base.Decode(reader);
        }

        /// <summary>
        /// Encodes this command.
        /// </summary>
        /// <param name="reader"></param>
        internal override void Encode(ByteWriter writer)
        {
            writer.AddInt(this.BoatID);
            writer.AddInt(this.UnitType);
            writer.AddData(this.UnitData);
        }

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="Level">The levl.</param>
        internal override void Execute(Level Level)
        {
            if (Level.Player.Home.GameObjectManager.GetGameObjectById(this.BoatID) is Building Building)
            {
                CharacterData UnitData = (CharacterData) this.UnitData;
                UnitProductionComponent UnitProductionComponent = (UnitProductionComponent) Building.GetComponent(3);
                UnitStorageComponent UnitStorageComponent = (UnitStorageComponent) Building.GetComponent(0);

                if (UnitProductionComponent != null && UnitStorageComponent != null)
                {
                    CharacterData PreviousUnitData = null;
                    int PreviousUnitCount = 0;

                    ResourceBundle ResourceBundle = UnitData.GetResourceBundle(Level.Player.UnitUpgrades.GetCount(this.UnitData));

                    if (UnitStorageComponent.HasUnits)
                    {
                        PreviousUnitData = UnitStorageComponent.Unit;
                        PreviousUnitCount += UnitStorageComponent.UnitCount;

                        if (PreviousUnitData != this.UnitData)
                        {
                            ResourceBundle.Removes(1, PreviousUnitData.GetResourceBundle(Level.Player.UnitUpgrades.GetCount(PreviousUnitData)).Resources[1].Count);

                            UnitStorageComponent.Unit = null;
                            UnitStorageComponent.UnitCount = 0;

                            PreviousUnitData = null;
                            PreviousUnitCount = 0;
                        }
                    }
                    
                    int TrainCount = UnitStorageComponent.MaxCapacity / UnitData.HousingSpace - PreviousUnitCount;

                    if (TrainCount > 0)
                    {
                        ResourceBundle.Multiply(TrainCount);

                        if (Level.Player.HasEnoughResources(ResourceBundle))
                        {
                            Level.Player.Resources.Remove(ResourceBundle.Resources[1].Data, ResourceBundle.Resources[1].Count);
                        }
                    }
                }
            }
        }
    }
}