using GL.Servers.BS.Files;
using GL.Servers.BS.Files.CSV_Logic;
using Gl.Servers.BS.Logic.Enums;

namespace GL.Servers.BS.Extensions
{
    internal class GameTools
    {
      internal static Resources AllianceCreateResource;

      internal static int StartingDiamonds;
      internal static int AllianceCreateCost;
      internal static int MaxMessageLength;
      internal static int MaxAllianceMailLength;
      internal static int SpeedUpDiamondCost1Min;
      internal static int SpeedUpDiamondCost1Hour;
      internal static int SpeedUpDiamondCost24Hours;
      internal static int SpeedUpDiamondCost1Week;
      internal static int SpeedUpFreeSeconds;
      internal static int ResourceDiamondCost1;
      internal static int ResourceDiamondCost10;
      internal static int ResourceDiamondCost100;
      internal static int ResourceDiamondCost1000;
      internal static int ResourceDiamondCost10000;
      internal static int ResourceDiamondCost100000;
      internal static int ResourceDiamondCost1000000;
      internal static int ResourceDiamondCost10000000;
      internal static int[] AllianceScoreContributionPercentage;
      internal static int StartingGold;
      internal static int BattlePingSampleSeconds;
      internal static int AllianceUnlockExpLevel;
      internal static int AfkTimerSeconds;
      internal static int BossDamagePercentIncrease;

      internal static void Initialize()
      {
      }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameTools"/> class.
        /// </summary>
        internal GameTools()
        {
        }
    }
}
