namespace GL.Servers.CoC.Extensions.Game
{
    using GL.Servers.CoC.Files;
    using GL.Servers.CoC.Files.CSV_Logic.Logic;
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Logic.Enums;

    internal class GamePlayUtil
    {
        public static int TimeToXp(int BuildTime)
        {
            return Math.Sqrt(BuildTime);
        }

        internal static void UpdateLeagueRank(Player Player)
        {
            CSV.Tables.Get(Gamefile.League).Datas.ForEach(Data =>
            {
                LeagueData LeagueData = (LeagueData) Data;
            });
        }

        public static int GetSpeedUpMultiplier(int Type)
        {
            switch (Type)
            {
                case 1:
                {
                    return 100;
                }
                case 2:
                {
                    return 100;
                }
                case 3:
                {
                    return 100;
                }
                default:
                    return 100;
            }
        }

        public static int GetResourceCost(ResourceData Resource, int Count, int VillageType)
        {
            if (Count > 0)
            {
                if (Resource.GlobalID != 3000003)
                {
                    int ResourceDiamondCost100;
                    int ResourceDiamondCost1000;
                    int ResourceDiamondCost10000;
                    int ResourceDiamondCost100000;
                    int ResourceDiamondCost1000000;
                    int ResourceDiamondCost10000000;

                    if (VillageType == 0)
                    {
                        ResourceDiamondCost100 = Globals.ResourceDiamondCost100;
                        ResourceDiamondCost1000 = Globals.ResourceDiamondCost1000;
                        ResourceDiamondCost10000 = Globals.ResourceDiamondCost10000;
                        ResourceDiamondCost100000 = Globals.ResourceDiamondCost100000;
                        ResourceDiamondCost1000000 = Globals.ResourceDiamondCost1000000;
                        ResourceDiamondCost10000000 = Globals.ResourceDiamondCost10000000;
                    }
                    else
                    {
                        ResourceDiamondCost100 = Globals.Village2ResourceDiamondCost100;
                        ResourceDiamondCost1000 = Globals.Village2ResourceDiamondCost1000;
                        ResourceDiamondCost10000 = Globals.Village2ResourceDiamondCost10000;
                        ResourceDiamondCost100000 = Globals.Village2ResourceDiamondCost100000;
                        ResourceDiamondCost1000000 = Globals.Village2ResourceDiamondCost1000000;
                        ResourceDiamondCost10000000 = Globals.Village2ResourceDiamondCost10000000;
                    }

                    if (Count > 99)
                    {
                        if (Count > 999)
                        {
                            if (Count > 9999)
                            {
                                if (Count > 99999)
                                {
                                    if (Count > 999999)
                                    {
                                        return ResourceDiamondCost1000000 + ((ResourceDiamondCost10000000 - ResourceDiamondCost1000000) * (Count / 1000 - 1000) + 4500) / 9000;
                                    }

                                    return ResourceDiamondCost100000 + ((ResourceDiamondCost1000000 - ResourceDiamondCost100000) * (Count / 100 - 1000) + 4500) / 9000;
                                }

                                return ResourceDiamondCost10000 + ((ResourceDiamondCost100000 - ResourceDiamondCost10000) * (Count / 10 - 1000) + 4500) / 9000;
                            }

                            return ResourceDiamondCost1000 + ((ResourceDiamondCost10000 - ResourceDiamondCost1000) * (Count - 1000) + 4500) / 9000;
                        }

                        return (int) ((uint) (((ulong) (-1851608123L * ((ResourceDiamondCost1000 - ResourceDiamondCost100) * (Count - 100) + 450)) >> 32)
                                              + (ulong) ((ResourceDiamondCost100 - ResourceDiamondCost1000) * (Count - 100))
                                              + 450) >> 31)
                               + ((int) (((ulong) (-1851608123L * ((ResourceDiamondCost1000 - ResourceDiamondCost100) * (Count - 100) + 450)) >> 32)
                                         + (ulong) ((ResourceDiamondCost1000 - ResourceDiamondCost100) * (Count - 100))
                                         + 450) >> 9);
                    }

                    return ResourceDiamondCost100;
                }

                int InfCost;
                uint Cost;

                int Var1;
                uint Var2;
                int Var3;

                if (Count > 9)
                {
                    if (Count > 99)
                    {
                        if (Count > 999)
                        {
                            if (Count > 9999)
                            {
                                InfCost = Globals.DarkElixirDiamondCost10000;
                                uint v11 = (uint) ((ulong) (1563749871L * ((Globals.DarkElixirDiamondCost100000 - InfCost) * (Count - 10000) + 45000)) >> 32);
                                Cost = v11 >> 31;
                                Var1 = (int) v11 >> 15;

                                goto End;
                            }

                            return Globals.DarkElixirDiamondCost1000 + ((Globals.DarkElixirDiamondCost10000 - Globals.DarkElixirDiamondCost1000) * (Count - 1000) + 4500) / 9000;
                        }

                        InfCost = Globals.DarkElixirDiamondCost100;
                        Var3 = (Globals.ResourceDiamondCost1000 - InfCost) * (Count - 100);
                        Var2 = (uint) (((ulong) (-1851608123L * (Var3 + 450)) >> 32) + (ulong) Var3 + 450);
                        Cost = Var2 >> 31;
                        Var1 = (int) Cost >> 9;

                        goto End;
                    }

                    InfCost = Globals.DarkElixirDiamondCost10;
                    Var3 = (Globals.ResourceDiamondCost100 - InfCost) * (Count - 10);
                    Var2 = (uint) (((ulong) (-1240768329L * (Var3 + 45)) >> 32) + (ulong) Var3 + 45);
                    Cost = Var2 >> 31;
                    Var1 = (int) Var2 >> 6;

                    goto End;
                }

                InfCost = Globals.DarkElixirDiamondCost1;
                Var2 = (uint) ((ulong) (954437177L * ((Globals.DarkElixirDiamondCost10 - InfCost) * (Count - 1) + 4)) >> 32);
                Cost = Var2 >> 31;
                Var1 = (int) Var2 >> 1;

                End:

                return (int) (InfCost + Cost + Var1);
            }

            return 0;
        }

        public static int GetSpeedUpCost(int Time, int VillageType, int Multiplier)
        {
            if (Time > 0)
            {
                int SpeedUpDiamondCost1Min;
                int SpeedUpDiamondCost1Hour;
                int SpeedUpDiamondCost24Hours;
                int SpeedUpDiamondCost1Week;

                if (VillageType == 0)
                {
                    SpeedUpDiamondCost1Min = Globals.SpeedUpDiamondCost1Min;
                    SpeedUpDiamondCost1Hour = Globals.SpeedUpDiamondCost1Hour;
                    SpeedUpDiamondCost24Hours = Globals.SpeedUpDiamondCost24Hours;
                    SpeedUpDiamondCost1Week = Globals.SpeedUpDiamondCost1Week;
                }
                else
                {
                    SpeedUpDiamondCost1Min = Globals.Village2SpeedUpDiamondCost1Min;
                    SpeedUpDiamondCost1Hour = Globals.Village2SpeedUpDiamondCost1Hour;
                    SpeedUpDiamondCost24Hours = Globals.Village2SpeedUpDiamondCost24Hours;
                    SpeedUpDiamondCost1Week = Globals.Village2SpeedUpDiamondCost1Week;
                }

                if (Time > 59)
                {
                    if (Time > 3599)
                    {
                        if (Time > 86399)
                        {
                            return SpeedUpDiamondCost24Hours * Multiplier / 100
                                   + (SpeedUpDiamondCost1Week - SpeedUpDiamondCost24Hours)
                                   * (Time - 86400)
                                   / 100
                                   * Multiplier
                                   / 518400;
                        }

                        return SpeedUpDiamondCost1Hour * Multiplier / 100
                               + (SpeedUpDiamondCost24Hours - SpeedUpDiamondCost1Hour)
                               * (Time - 3600)
                               / 100
                               * Multiplier
                               / 82800;
                    }

                    return SpeedUpDiamondCost1Min * Multiplier / 100
                           + Multiplier
                           * (SpeedUpDiamondCost1Hour - SpeedUpDiamondCost1Min)
                           * (Time - 60)
                           / 354000;
                }

                return Math.Max(SpeedUpDiamondCost1Min * Multiplier / 100, 1);
            }

            return 0;
        }
    }
}