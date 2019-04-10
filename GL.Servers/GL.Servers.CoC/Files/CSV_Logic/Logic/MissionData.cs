namespace GL.Servers.CoC.Files.CSV_Logic.Logic
{
    using System;

    using GL.Servers.CoC.Core;
    using GL.Servers.Files.CSV_Reader;
	using GL.Servers.CoC.Files.CSV_Helpers;
	using GL.Servers.CoC.Logic;
	using GL.Servers.CoC.Logic.Enums;

    internal class MissionData : Data
    {
        internal NpcData AttackNpcData;
        internal MissionData Dependency;
        internal BuildingData BuildBuildingData;
        internal ResourceData RewardResourceData;
        internal CharacterData RewardCharacterData;

        internal int MissionType = -1;

		/// <summary>
        /// Initializes a new instance of the <see cref="MissionData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public MissionData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // MissionData.
        }

        internal override void LoadingFinished()
        {
            if (!string.IsNullOrEmpty(this.Dependencies))
            {
                this.Dependency = (MissionData) CSV.Tables.Get(Gamefile.Mission).GetData(this.Dependencies);
            }

            if (!string.IsNullOrWhiteSpace(this.FixVillageObject))
            {
                this.MissionType = 13;
            }

            if (string.Equals(this.Action, "travel"))
            {
                this.MissionType = 14;
            }

            if (string.Equals(this.Action, "upgrade2"))
            {
                this.MissionType = 17;
            }

            if (string.Equals(this.Action, "duel"))
            {
                this.MissionType = 18;
            }

            if (string.Equals(this.Action, "duel_end"))
            {
                this.MissionType = 19;
            }

            if (string.Equals(this.Action, "duel_end2"))
            {
                this.MissionType = 20;
            }

            if (string.Equals(this.Action, "show_builder_menu"))
            {
                this.MissionType = 21;
            }

            if (!string.IsNullOrWhiteSpace(this.BuildBuilding))
            {
                this.BuildBuildingData = (BuildingData)CSV.Tables.Get(Gamefile.Building).GetData(this.BuildBuilding);
                this.BuildBuildingLevel--;

                if (string.Equals(this.Action, "unlock"))
                {
                    this.MissionType = 15;
                }
                else
                    this.MissionType = this.BuildBuildingLevel > 0 ? 5 : 0;

                if (this.BuildBuildingCount < 0)
                {
                    throw new Exception("missions.csv: BuildBuildingCount is invalid!");
                }
            }

            if (this.MissionType == -1)
            {
                if (this.OpenAchievements)
                {
                    this.MissionType = 7;
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(this.DefendNPC))
                    {
                        this.MissionType = 1;
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(this.AttackNPC))
                        {
                            this.MissionType = 2;
                            this.AttackNpcData = (NpcData) CSV.Tables.Get(Gamefile.Npc).GetData(this.AttackNPC);

                            if (this.AttackNPC == null)
                            {
                                throw new Exception("missions.csv: AttackNPC is invalid!");
                            }
                        }
                        else
                        {
                            if (this.ChangeName)
                            {
                                this.MissionType = 6;
                            }
                            else
                            {
                                if (this.TrainTroops > 0)
                                {
                                    this.MissionType = 4;
                                }
                                else
                                {
                                    if (this.SwitchSides)
                                    {
                                        this.MissionType = 8;
                                    }
                                    else
                                    {
                                        if (this.ShowWarBase)
                                        {
                                            this.MissionType = 9;
                                        }
                                        else
                                        {
                                            if (this.OpenInfo)
                                            {
                                                this.MissionType = 11;
                                            }
                                            else
                                            {
                                                if (this.ShowDonate)
                                                {
                                                    this.MissionType = 10;
                                                }
                                                else
                                                {
                                                    if (this.WarStates)
                                                    {
                                                        this.MissionType = 12;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (this.Villagers > 0)
            {
                this.MissionType = 16;
            }

            if (!string.IsNullOrWhiteSpace(this.RewardResource))
            {
                this.RewardResourceData = (ResourceData)CSV.Tables.Get(Gamefile.Resource).GetData(this.RewardResource);

                if (this.RewardResourceData == null)
                {
                    throw new Exception("missions.csv: RewardResource is invalid!");
                }

                if (this.RewardResourceCount < 0)
                {
                    throw new Exception("missions.csv: RewardResourceCount is negative!");
                }
            }

            if (!string.IsNullOrWhiteSpace(this.RewardTroop))
            {
                this.RewardCharacterData = (CharacterData)CSV.Tables.Get(Gamefile.Character).GetData(this.RewardTroop);

                if (this.RewardCharacterData == null)
                {
                    throw new Exception("missions.csv: RewardTroop is invalid!");
                }

                if (this.RewardTroopCount < 0)
                {
                    throw new Exception("missions.csv: RewardTroopCount is negative!");
                }
            }

            if (this.RewardXP < 0)
            {
                throw new Exception("missions.csv: RewardXP is negative!");
            }
        }

        public string Dependencies
        {
            get; set;
        }

        public int MissionCategory
        {
            get; set;
        }

        public int VillageType
        {
            get; set;
        }

        public bool FirstStep
        {
            get; set;
        }

        public bool WarStates
        {
            get; set;
        }

        public bool Deprecated
        {
            get; set;
        }

        public bool OpenInfo
        {
            get; set;
        }

        public bool ShowWarBase
        {
            get; set;
        }

        public bool ShowDonate
        {
            get; set;
        }

        public bool SwitchSides
        {
            get; set;
        }

        public bool OpenAchievements
        {
            get; set;
        }

        public string Action
        {
            get; set;
        }

        public string Character
        {
            get; set;
        }

        public string FixVillageObject
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

        public bool AttackPlayer
        {
            get; set;
        }

        public bool ChangeName
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

        public bool ShowMap
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

        public string TutorialCharacter
        {
            get; set;
        }

        public string CharacterSWF
        {
            get; set;
        }

        public bool LoopAnim
        {
            get; set;
        }

        public bool SwitchAnim
        {
            get; set;
        }

        public string SpeechBubble
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

        public string TutorialMusicAlt
        {
            get; set;
        }

        public string TutorialSound
        {
            get; set;
        }

        public string RewardResource
        {
            get; set;
        }

        public int RewardResourceCount
        {
            get; set;
        }

        public int RewardXP
        {
            get; set;
        }

        public string RewardTroop
        {
            get; set;
        }

        public int RewardTroopCount
        {
            get; set;
        }

        public int CustomData
        {
            get; set;
        }

        public bool ShowGooglePlusSignin
        {
            get; set;
        }

        public bool HideGooglePlusSignin
        {
            get; set;
        }

        public bool ShowInstructor
        {
            get; set;
        }

        public int Villagers
        {
            get; set;
        }

        public bool ForceCamera
        {
            get; set;
        }

        internal bool IsOpenForAvatar(Player Player)
        {
            if (!Player.Missions.Contains(this))
            {
                if (this.Villagers > Player.TownHallLevel)
                {
                    return false;
                }

                if (this.Dependency != null)
                {
                    if (!Player.Missions.Contains(this.Dependency))
                    {
                        Logging.Info(this.GetType(), "Unable to start mission " + this.Name + ". Dependency " + this.Dependency.Name + " is not finish.");
                        return false;
                    }
                }

                return true;
            }

            return false;
        }
    }
}
