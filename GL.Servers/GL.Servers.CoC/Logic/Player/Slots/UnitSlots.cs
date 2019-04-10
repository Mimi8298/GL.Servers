namespace GL.Servers.CoC.Logic.Slots
{
    using GL.Servers.CoC.Files.CSV_Logic.Logic;

    internal class UnitSlots : DataSlots
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceSlots"/> class.
        /// </summary>
        internal UnitSlots(int Capacity = 10) : base(Capacity)
        {
            // UnitSlots.
        }
        
        internal int GetUnitsTotal()
        {
            int Total = 0;

            this.ForEach(Slot =>
            {
                Total += Slot.Count;
            });

            return Total;
        }

        internal int GetUnitsTotalCapacity()
        {
            int Total = 0;

            this.ForEach(Slot =>
            {
                Total += Slot.Count * ((CharacterData) Slot.Data).HousingSpace;
            });

            return Total;
        }
    }
}