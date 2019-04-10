namespace GL.Servers.BB.Extensions
{
    using GL.Servers.BB.Files;
    using GL.Servers.BB.Files.CSV_Logic;
    using GL.Servers.BB.Files.Enums;

    internal class GameSettings
    {
        internal const int MajorVersion = 31;
        internal const int MinorVersion = 0;
        internal const int BuildVersion = 146;

        internal readonly int StartingDiamonds;
        internal readonly ResourceBundle StartingResources;

        internal readonly int PrototypeTimeHours;

        internal readonly string HomeBaseLevelFile;

        public GameSettings()
        {
            this.StartingResources = new ResourceBundle(((GlobalData)CSV.Tables.Get(Gamefile.Global).GetData("STARTING_RESOURCES")).TextValue);
            this.StartingDiamonds  = ((GlobalData) CSV.Tables.Get(Gamefile.Global).GetData("STARTING_DIAMONDS")).NumberValue;

            this.PrototypeTimeHours = ((GlobalData) CSV.Tables.Get(Gamefile.Global).GetData("PROTOTYPE_TIME_HOURS")).NumberValue;

            this.HomeBaseLevelFile = ((GlobalData) CSV.Tables.Get(Gamefile.Global).GetData("HOME_BASE_LEVEL_FILE")).TextValue;

        }
    }
}