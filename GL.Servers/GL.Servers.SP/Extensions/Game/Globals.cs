namespace GL.Servers.SP.Extensions.Game
{
    using GL.Servers.SP.Files;
    using GL.Servers.SP.Files.CSV_Logic;
    using GL.Servers.SP.Logic.Enums;

    internal class Globals
    {
        internal static int MaxEnergy;
        internal static int LifeRegenerationTimeMinutes;

        internal static int StartingDiamonds;
        internal static int StartingEnergy;

        internal static void Initialize()
        {
            Globals.MaxEnergy = ((GlobalData)CSV.Tables.Get(Gamefile.Globals).GetData("MAX_ENERGY")).NumberValue;
            Globals.LifeRegenerationTimeMinutes = ((GlobalData) CSV.Tables.Get(Gamefile.Globals).GetData("LIFE_REGENERATION_TIME_MINUTES")).NumberValue;

            Globals.StartingDiamonds = ((GlobalData) CSV.Tables.Get(Gamefile.Globals).GetData("STARTING_DIAMONDS")).NumberValue;
            Globals.StartingEnergy = ((GlobalData) CSV.Tables.Get(Gamefile.Globals).GetData("STARTING_ENERGY")).NumberValue;
        }
    }
}