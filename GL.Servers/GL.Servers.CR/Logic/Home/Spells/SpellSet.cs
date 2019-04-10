namespace GL.Servers.CR.Logic.Spells
{
    using System.Collections.Generic;
    using GL.Servers.Core;
    using GL.Servers.CR.Core;
    using GL.Servers.CR.Files;
    using GL.Servers.CR.Files.Logic;
    using GL.Servers.CR.Logic.Enums;

    internal class SpellSet
    {
        internal readonly List<SpellData>[] Spells;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpellSet"/> class.
        /// </summary>
        public SpellSet(ArenaData ArenaData, SpellSetData SetData)
        {
            this.Spells = new List<SpellData>[CSV.Tables.Get(Gamefile.Rarity).Datas.Count];

            for (int i = 0; i < CSV.Tables.Get(Gamefile.Rarity).Datas.Count; i++)
            {
                this.Spells[i] = new List<SpellData>(32);
            }

            if (SetData != null)
            {
                for (int i = 0; i < SetData.SpellsData.Length; i++)
                {
                    this.AddSpell(SetData.SpellsData[i]);
                }
            }
            else
            {
                CSV.Tables.Spells.ForEach(SpellData =>
                {
                    if (SpellData.IsUnlockedInArena(ArenaData))
                    {
                        this.AddSpell(SpellData);
                    }
                });
            }
        }

        /// <summary>
        /// Adds the spell to the collection.
        /// </summary>
        internal void AddSpell(SpellData Data)
        {
            if (!Data.NotInUse)
            {
                this.Spells[Data.RarityData.Instance].Add(Data);
            }
        }
        
        /// <summary>
        /// Gets a random spell.
        /// </summary>
        internal SpellData GetRandomSpell(XorShift Random, RarityData Data)
        {
            int Count = this.Spells[Data.Instance].Count;

            if (Count > 0)
            {
                return this.Spells[Data.Instance][Random.Next(Count)];
            }

            Logging.Info(this.GetType(), "GetRandomSpell() - No spell found for rarity: " + Data.Name);

            return null;
        }
    }
}