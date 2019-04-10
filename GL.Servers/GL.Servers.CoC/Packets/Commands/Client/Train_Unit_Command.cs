namespace GL.Servers.CoC.Packets.Commands.Client
{
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Extensions.Helper;
    using GL.Servers.CoC.Files.CSV_Helpers;
    using GL.Servers.CoC.Files.CSV_Logic.Logic;

    using GL.Servers.Extensions.Binary;

    internal class Train_Unit_Command : Command
    {
        internal int UnitType;
        internal int Count;
        internal int BarrackId;

        internal Data Unit;

        /// <summary>
        /// Gets a value indicating the command type.
        /// </summary>
        internal override int Type
        {
            get
            {
                return 508;
            }
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode(Reader Reader)
        {
            Reader.ReadInt32();

            this.UnitType  = Reader.ReadInt32();
            this.Unit      = Reader.ReadData<CharacterData>();
            this.Count     = Reader.ReadInt32();

            this.BarrackId = Reader.ReadInt32();

            base.Decode(Reader);
        }

        internal override void Execute(Level Level)
        {
            if (this.Unit != null)
            {
                if (this.UnitType == 0)
                {
                    CharacterData Character = this.Unit as CharacterData;

                    if (Character != null)
                    {
                        ResourceData TrainingResource = Character.TrainingResourceData;

                        if(Level.Player.Resources.GetCountByData(TrainingResource) >= Character.TrainingCost[0] * this.Count)
                        {
                            if (Level.UnitProductionManager.CanProduce(Character, this.Count))
                            {
                                Level.UnitProductionManager.AddUnit(Character, this.Count);
                                Level.Player.Resources.Remove(TrainingResource, Character.TrainingCost[0] * this.Count);
                            }
                        }
                    }
                }
            }
        }
    }
}