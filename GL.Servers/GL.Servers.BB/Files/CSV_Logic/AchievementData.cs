namespace GL.Servers.BB.Files.CSV_Logic
{
    using System;
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.BB.Files.Enums;
    using GL.Servers.Files.CSV_Reader;

    internal class AchievementData : Data
    {
        internal readonly Data ActionDataData;

        internal readonly int Type;

		/// <summary>
        /// Initializes a new instance of the <see cref="AchievementData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public AchievementData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);

            switch (this.Action)
            {
                case "upgrade":
                {
                    this.Type = 0;
                    this.ActionDataData = CSV.Tables.Get(Gamefile.Building).GetData(this.ActionData);

                    break;
                }

                case "victory_points":
                {
                    this.Type = 1;

                    break;
                }

                case "clear_obstacles":
                {
                    this.Type = 2;

                    break;
                }

                case "loot":
                {
                    this.Type = 3;
                    this.ActionDataData = CSV.Tables.Get(Gamefile.Resource).GetData(this.ActionData);

                    break;
                }

                case "destroy":
                {
                    this.Type = 4;
                    this.ActionDataData = CSV.Tables.Get(Gamefile.Building).GetData(this.ActionData);

                    break;
                }

                case "win_pvp_defense":
                {
                    this.Type = 5;

                    break;
                }

                case "win_boss":
                {
                    this.Type = 6;

                    break;
                }

                case "win_event":
                {
                    this.Type = 9;

                    break;
                }

                case "win_without_losses":
                {
                    this.Type = 7;
                    this.ActionDataData = null; // TODO Implement WinWithoutLossesData.

                    break;
                }

                case "control_outposts":
                {
                    this.Type = 8;

                    break;
                }

                case "deepsea":
                {
                    this.Type = 10;

                    break;
                }

                case "coopvps":
                {
                    this.Type = 11;

                    break;
                }

                case "hammerman":
                {
                    this.Type = 12;

                    break;
                }

                case "gearheart":
                {
                    this.Type = 13;

                    break;
                }

                case "explorer":
                {
                    this.Type = 14;

                    break;
                }

                case "armourer":
                {
                    this.Type = 15;

                    break;
                }

                case "artisan":
                {
                    this.Type = 16;

                    break;
                }

                case "prototype":
                {
                    this.Type = 17;

                    break;
                }

                default:
                {
                    throw new Exception("Unknown Action in achievements " + this.ActionData);
                }
            }

            if (!string.IsNullOrEmpty(this.ActionData))
            {
                this.Type = 0;
                this.ActionDataData = CSV.Tables.Get(Gamefile.Building).GetData(this.ActionData);
            }

            if (this.Action == "victory_points")
            {
                this.Type = 1;
            }

            if (this.Action == "clear_obstacles")
            {
                this.Type = 2;
            }

            if (this.Action == "loot")
            {
                this.Type = 3;
                this.ActionDataData = CSV.Tables.Get(Gamefile.Resource).GetData(this.ActionData);
            }
            else
            {
                if (this.Action != "destroy")
                {
                    
                }
            }
            if (this.Action == "victory_points")
            {
                this.Type = 1;
            }

            if (this.Action == "victory_points")
            {
                this.Type = 1;
            }
        }

        public int Level
        {
            get; set;
        }

        public string TID
        {
            get; set;
        }

        public string InfoTID
        {
            get; set;
        }

        public string Action
        {
            get; set;
        }

        public int ActionCount
        {
            get; set;
        }

        public string ActionData
        {
            get; set;
        }

        public int DiamondReward
        {
            get; set;
        }

        public string IconSWF
        {
            get; set;
        }

        public string IconExportName
        {
            get; set;
        }

        public string AndroidID
        {
            get; set;
        }

        public int GameCenterPoints
        {
            get; set;
        }
    }
}
