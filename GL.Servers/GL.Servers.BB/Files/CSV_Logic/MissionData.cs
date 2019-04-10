namespace GL.Servers.BB.Files.CSV_Logic
{
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.BB.Files.Enums;
    using GL.Servers.Files.CSV_Reader;

    internal class MissionData : Data
    {
        internal BuildingData BuildBuildingData;
        internal MissionData DependencyData;
        internal NpcData AttackNpcData;
        internal int MissionType;

        /// <summary>
        /// Initializes a new instance of the <see cref="MissionData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public MissionData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            Data.Load(this, this.GetType(), Row);

            if (!string.IsNullOrEmpty(this.Dependencies))
            {
                this.DependencyData = (MissionData) DataTable.GetData(this.Dependencies);
            }

            if (!string.IsNullOrEmpty(this.BuildBuilding))
            {
                this.BuildBuildingData = (BuildingData) CSV.Tables.Get(Gamefile.Building).GetData(this.BuildBuilding);
                this.MissionType = 0;
            }
            else
            {
                if (!string.IsNullOrEmpty(this.DefendNPC))
                {
                    this.MissionType = 1;
                    return;
                }

                if (!string.IsNullOrEmpty(this.AttackNPC))
                {
                    this.AttackNpcData = (NpcData) CSV.Tables.Get(Gamefile.Npc).GetData(this.AttackNPC);
                    this.MissionType = 2;
                    return;
                }

                if (this.TrainTroops > 0)
                {
                    this.MissionType = 4;
                    return;
                }

                if (this.Name == "set name")
                {
                    this.MissionType = 11;
                    return;
                }
                
                if (this.Name == "daily mission")
                {
                    this.MissionType = 12;
                    return;
                }

                this.MissionType = 10;
            }
        }

        public string Dependencies
        {
            get; set;
        }

        public bool Deprecated
        {
            get; set;
        }

        public string BuildBuilding
        {
            get; set;
        }

        public int BuildBuildingLevel
        {
            get; set;
        }

        public int BuildBuildingCount
        {
            get; set;
        }

        public string DefendNPC
        {
            get; set;
        }

        public string AttackNPC
        {
            get; set;
        }

        public int Delay
        {
            get; set;
        }

        public int TrainTroops
        {
            get; set;
        }

        public string TutorialText
        {
            get; set;
        }

        public int TutorialStep
        {
            get; set;
        }

        public bool Darken
        {
            get; set;
        }

        public string TutorialTextBox
        {
            get; set;
        }

        public string SpeechBubble
        {
            get; set;
        }

        public string AbilityName
        {
            get; set;
        }

        public string CustomIconExportName
        {
            get; set;
        }

        public bool RightAlignTextBox
        {
            get; set;
        }

        public string ButtonText
        {
            get; set;
        }

        public string TutorialMusic
        {
            get; set;
        }

        public string TutorialSound
        {
            get; set;
        }

        public int TriggerAtTownHallLevel
        {
            get; set;
        }

        public string TriggerAtNodeTypeFound
        {
            get; set;
        }

        public int TriggerAtRegionIndexExplored
        {
            get; set;
        }

        public int TriggerAtRegionIndexLiberated
        {
            get; set;
        }

        public int TriggerAtNodeIndexLiberated
        {
            get; set;
        }

        public string TriggerAtHeroUnlocked
        {
            get; set;
        }

        public string TriggerAtResourceGained
        {
            get; set;
        }

        public bool InitialTutorial
        {
            get; set;
        }

        public string TriggerAtEventSpawn
        {
            get; set;
        }

        public string TriggerAtEventWin
        {
            get; set;
        }

        public int MegabaseState
        {
            get; set;
        }
    }
}
