namespace GL.Servers.CoC.Logic.Slots
{
    using GL.Servers.CoC.Files;
    using GL.Servers.CoC.Files.CSV_Helpers;
    using GL.Servers.CoC.Files.CSV_Logic.Logic;
    using GL.Servers.CoC.Logic.Items;
    using GL.Servers.CoC.Logic.Enums;

    internal class NpcMapSlots : DataSlots
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NpcMapSlots"/> class.
        /// </summary>
        /// <param name="Capacity"></param>
        internal NpcMapSlots(int Capacity = 10) : base(Capacity)
        {
            // NpcMapSlots.
        }
        
        /// <summary>
        /// Gets a value whether the npc specified can be attacked.
        /// </summary>
        internal bool CanAttackNPC(Data Data)
        {
            NpcData Npc = (NpcData) Data;

            for (int i = 0; i < Npc.MapDependencies.Count; i++)
            {
                Item Progress = this.GetByData(CSV.Tables.Get(Gamefile.Npc).GetData(Npc.MapDependencies[i]));

                if (Progress == null || Progress.Count <= 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}