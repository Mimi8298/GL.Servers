namespace GL.Servers.SL.Extensions.Game
{
    using GL.Servers.SL.Files;
    using GL.Servers.SL.Files.CSV_Logic.Logic;
    using GL.Servers.SL.Logic.Enums;

    internal class GameSettings
    {
        internal static int EnergyRegenateSeconds;
        internal static int StartingGold;
        internal static int StartingDiamonds;
        internal static HeroData StartingCharacter;
        internal static QuestData StartingQuest;

        internal static void Initialize()
        {
            GameSettings.EnergyRegenateSeconds = ((GlobalData)CSV.Tables.Get(Gamefile.Globals).GetData("ENERGY_REGENERATE_SECONDS")).NumberValue;

            GameSettings.StartingGold          = ((GlobalData)CSV.Tables.Get(Gamefile.Globals).GetData("STARTING_GOLD")).NumberValue;
            GameSettings.StartingDiamonds      = ((GlobalData)CSV.Tables.Get(Gamefile.Globals).GetData("STARTING_DIAMONDS")).NumberValue;
            GameSettings.StartingCharacter     = (HeroData)CSV.Tables.Get(Gamefile.Heroes).GetData(((GlobalData)CSV.Tables.Get(Gamefile.Globals).GetData("STARTING_CHARACTER")).TextValue);
            GameSettings.StartingQuest         = (QuestData)CSV.Tables.Get(Gamefile.Quests).GetData(((GlobalData)CSV.Tables.Get(Gamefile.Globals).GetData("STARTING_QUEST")).TextValue);
        }
    }
}