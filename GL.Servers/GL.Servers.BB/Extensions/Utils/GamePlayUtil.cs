namespace GL.Servers.BB.Extensions.Utils
{
    using GL.Servers.BB.Files.CSV_Logic;

    internal static class GamePlayUtil
    {
        internal static int GetXpGain(BuildingData Data, int Level)
        {
            return Data.XpGain[Level];
        }
    }
}