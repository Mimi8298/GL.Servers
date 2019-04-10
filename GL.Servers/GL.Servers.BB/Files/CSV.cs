namespace GL.Servers.BB.Files
{
    using System.Collections.Generic;
    using System.IO;

    using GL.Servers.BB.Core;
    using GL.Servers.BB.Files.CSV_Reader;
    using GL.Servers.Files.CSV_Reader;

    internal class CSV
    {
        internal static readonly Dictionary<int, string> Gamefiles = new Dictionary<int, string>();

        internal static Gamefiles Tables;

        /// <summary>
        /// Initializes a new instance of the <see cref="CSV"/> class.
        /// </summary>
        internal static void Initialize()
        {
            CSV.Tables = new Gamefiles();

            CSV.Gamefiles.Add(0, @"Gamefiles/csv/buildings.csv");
            CSV.Gamefiles.Add(1, @"Gamefiles/csv/locales.csv");
            CSV.Gamefiles.Add(2, @"Gamefiles/csv/resources.csv");
            CSV.Gamefiles.Add(3, @"Gamefiles/csv/characters.csv");
            // CSV.Gamefiles.Add(4, @"Gamefiles/csv/animations.csv");
            CSV.Gamefiles.Add(5, @"Gamefiles/csv/projectiles.csv");
            CSV.Gamefiles.Add(6, @"Gamefiles/csv/building_classes.csv");
            CSV.Gamefiles.Add(7, @"Gamefiles/csv/obstacles.csv");
            CSV.Gamefiles.Add(8, @"Gamefiles/csv/effects.csv");
            // CSV.Gamefiles.Add(9, @"Gamefiles/csv/particle_emitters.csv");
            CSV.Gamefiles.Add(10, @"Gamefiles/csv/experience_levels.csv");
            CSV.Gamefiles.Add(11, @"Gamefiles/csv/traps.csv");
            CSV.Gamefiles.Add(12, @"Gamefiles/csv/alliance_badges.csv");
            CSV.Gamefiles.Add(13, @"Gamefiles/csv/globals.csv");
            CSV.Gamefiles.Add(14, @"Gamefiles/csv/townhall_levels.csv");
            CSV.Gamefiles.Add(15, @"Gamefiles/csv/prototypes.csv");
            CSV.Gamefiles.Add(16, @"Gamefiles/csv/npcs.csv");
            CSV.Gamefiles.Add(17, @"Gamefiles/csv/decos.csv");
            CSV.Gamefiles.Add(18, @"Gamefiles/csv/resource_packs.csv");

            CSV.Gamefiles.Add(20, @"Gamefiles/csv/missions.csv");
            CSV.Gamefiles.Add(21, @"Gamefiles/csv/billing_packages.csv");
            CSV.Gamefiles.Add(22, @"Gamefiles/csv/achievements.csv");
            CSV.Gamefiles.Add(25, @"Gamefiles/csv/spells.csv");
            CSV.Gamefiles.Add(26, @"Gamefiles/csv/hints.csv");
            CSV.Gamefiles.Add(27, @"Gamefiles/csv/landing_ships.csv");
            CSV.Gamefiles.Add(28, @"Gamefiles/csv/artifacts.csv");
            CSV.Gamefiles.Add(29, @"Gamefiles/csv/artifact_bonuses.csv");
            CSV.Gamefiles.Add(30, @"Gamefiles/csv/deepsea_parameters.csv");
            CSV.Gamefiles.Add(31, @"Gamefiles/csv/exploration_costs.csv");
            CSV.Gamefiles.Add(34, @"Gamefiles/csv/resource_ships.csv");
            CSV.Gamefiles.Add(35, @"Gamefiles/csv/loot_boxes.csv");
            CSV.Gamefiles.Add(36, @"Gamefiles/csv/liberated_income.csv");
            CSV.Gamefiles.Add(37, @"Gamefiles/csv/regions.csv");
            CSV.Gamefiles.Add(38, @"Gamefiles/csv/defense_rewards.csv");
            CSV.Gamefiles.Add(39, @"Gamefiles/csv/locators.csv");
            CSV.Gamefiles.Add(40, @"Gamefiles/csv/events.csv");
            CSV.Gamefiles.Add(41, @"Gamefiles/csv/footsteps.csv");
            CSV.Gamefiles.Add(42, @"Gamefiles/csv/persistent_event_rewards.csv");
            CSV.Gamefiles.Add(43, @"Gamefiles/csv/community_links.csv");
            CSV.Gamefiles.Add(44, @"Gamefiles/csv/shields.csv");
            CSV.Gamefiles.Add(45, @"Gamefiles/csv/ab_tests.csv");
            CSV.Gamefiles.Add(46, @"Gamefiles/csv/letters.csv");
            CSV.Gamefiles.Add(47, @"Gamefiles/csv/ranks.csv");
            CSV.Gamefiles.Add(48, @"Gamefiles/csv/countries.csv");
            CSV.Gamefiles.Add(51, @"Gamefiles/csv/boombox.csv");
            CSV.Gamefiles.Add(52, @"Gamefiles/csv/heroes.csv");
            CSV.Gamefiles.Add(53, @"Gamefiles/csv/hero_abilities.csv");
            CSV.Gamefiles.Add(54, @"Gamefiles/csv/offers.csv");
            CSV.Gamefiles.Add(55, @"Gamefiles/csv/deeplinks.csv");
            CSV.Gamefiles.Add(56, @"Gamefiles/csv/sectors.csv");
            CSV.Gamefiles.Add(57, @"Gamefiles/csv/sector_bonuses.csv");

            foreach (var File in CSV.Gamefiles)
            {
                if (new FileInfo(File.Value).Exists)
                {
                    CSV.Tables.Initialize(new Table(File.Value), File.Key);
                }
                else
                {
                    Logging.Error(typeof(CSV), "The CSV file at \"" + File.Value + "\" is missing, aborting.");
                }
            }

            Logging.Error(typeof(CSV), "Loaded and stored in memory " + CSV.Gamefiles.Count + " gamefiles.");
        }
    }
}