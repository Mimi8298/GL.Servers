namespace GL.Servers.BB.Logic.Slots
{
    using GL.Servers.BB.Core;
    using GL.Servers.BB.Files.CSV_Logic;
    using GL.Servers.BB.Logic.Items;

    internal class UnitList : DataList<Item>
    {
        internal UnitList() : base()
        {
            // ResourceSlots.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitList"/> class.
        /// </summary>
        /// <param name="Player">The player.</param>
        internal UnitList(Player Player) : base(Player)
        {
            // UnitList.
        }

        /// <summary>
        /// Adds units to the player.
        /// </summary>
        /// <param name="BoatIndex">The boat.</param>
        /// <param name="Character">The unit data.</param>
        /// <param name="Count">The unit count.</param>
        internal void AddUnits(int BoatIndex, CharacterData Character, int Count)
        {
            if (Count > 0)
            {
                if (this.Count > BoatIndex)
                {
                    this[BoatIndex].Data = Character;
                    this[BoatIndex].Count += Count;
                }
                else
                    this.Insert(BoatIndex, new Item(Character, Count));
            }
        }

        /// <summary>
        /// Removes units to the player.
        /// </summary>
        /// <param name="BoatIndex">The boat.</param>
        /// <param name="Character">The unit data.</param>
        /// <param name="Count">The unit count.</param>
        internal void RemoveUnits(int BoatIndex, CharacterData Character, int Count)
        {
            if (Count > 0)
            {
                if (this.Count > BoatIndex)
                {
                    this[BoatIndex].Count -= Count;

                    if (this[BoatIndex].Count < 0)
                    {
                        this[BoatIndex].Count = Count;
                    }
                }
            }
        }
    }
}