namespace GL.Servers.CR.Logic
{
    using System;
    using System.Collections.Generic;
    using GL.Servers.Core;
    using GL.Servers.CR.Extensions.Utils;
    using GL.Servers.CR.Files;
    using GL.Servers.CR.Files.CSV_Helpers;
    using GL.Servers.CR.Files.Logic;
    using GL.Servers.CR.Logic.Enums;
    using GL.Servers.CR.Logic.Spells;

    internal class RewardRandomizer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RewardRandomizer"/> class.
        /// </summary>
        public RewardRandomizer()
        {
            
        }

        internal static void CombineSpells(int Count, List<Spell> Spells, Home Home, XorShift Random)
        {
            
        }

        /// <summary>
        /// Creates a spell with spell data.
        /// </summary>
        internal static Spell CreateSpell(SpellData Data)
        {
            Spell Spell = new Spell(Data);

            Spell.SetCreateTime(TimeUtil.MinutesSince1970);
            Spell.SetShowNewIcon(true);

            return Spell;
        }

        /// <summary>
        /// Randomizes spells.
        /// </summary>
        internal static List<Spell> RandomizeSpells(TreasureChestData Data, XorShift Random, Home Home)
        {
            List<Spell> Spells = new List<Spell>();

            if (Data.GuaranteedSpellsData.Length > 0)
            {
                for (int i = 0; i < Data.GuaranteedSpellsData.Length; i++)
                {
                    Spell Spell = RewardRandomizer.CreateSpell(Data.GuaranteedSpellsData[i]);
                    Spell.SetMaterialCount(1);
                    Spells.Add(Spell);
                }
            }

            int RandomSpellCount = Data.GetRandomSpellCount();
            SpellSet SpellSet = new SpellSet(Data.ArenaData, null);
            DataTable RaritiesTable = CSV.Tables.Get(Gamefile.Rarity);
            int[] CountByRarity = new int[RaritiesTable.Datas.Count];

            for (int i = 1; i < RaritiesTable.Datas.Count; i++)
            {
                int Chance = Data.GetChanceForRarity((RarityData) RaritiesTable.Datas[i]);

                if (Chance > 0)
                {
                    Console.WriteLine("Random spells : " + RandomSpellCount);

                    if (RandomSpellCount > 0)
                    {
                        int Cnt = RandomSpellCount / Chance;
                        int Mod = RandomSpellCount % Chance;
                        int Rnd = Random.Next(Chance);

                        Console.WriteLine("Count : " + Cnt);
                        Console.WriteLine("Mod : " + Mod);
                        Console.WriteLine("Rnd : " + Rnd);
                        Console.WriteLine();

                        if (Mod > Rnd ^ Rnd == Mod)
                        {
                            CountByRarity[i] = Cnt + 1;
                            RandomSpellCount -= Cnt - 1;

                            Console.WriteLine(i + " : "+ Cnt);
                        }
                    }
                }
                else 
                    Console.WriteLine("Chance for rarity " + RaritiesTable.Datas[i].Name + " is equals to 0. " + Data.Name);
            }

            return null;

            for (int i = 0; i < RaritiesTable.Datas.Count; i++)
            {
                int j = 0;
                int k = 0;

                while (k >= CountByRarity[i])
                {
                    if (j >= 4999)
                    {
                        break;
                    }

                    ++j;

                    Spell Spell = RewardRandomizer.CreateSpell(SpellSet.GetRandomSpell(Random, (RarityData) RaritiesTable.Datas[i]));

                    if (Spell != null)
                    {
                        if (Spells.Contains(Spell))
                        {
                            continue;
                        }

                        if (!Home.HasSpell(Spell.Data) || j - 1 >= 1000)
                        {
                            Spell.AddMaterialCount(1);
                        }

                        Spells.Add(Spell);
                        ++k;
                    }
                }
            }

            return Spells;
        }
    }
}