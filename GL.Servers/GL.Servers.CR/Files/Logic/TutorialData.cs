namespace GL.Servers.CR.Files.Logic
{
    using GL.Servers.Files.CSV_Reader;
    using GL.Servers.CR.Files.CSV_Helpers;

    internal class TutorialData : Data
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="TutorialData"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        public TutorialData(Row Row, DataTable DataTable) : base(Row, DataTable)
        {
            // TutorialNpcData.
        }

        /// <summary>
        /// Called when all instances has been loaded for initialized members in instance.
        /// </summary>
		internal override void LoadingFinished()
		{
	    	// LoadingFinished.
		}
	
        internal string Location
        {
            get; set;
        }

        internal string NPC
        {
            get; set;
        }

        internal string TID
        {
            get; set;
        }

        internal string ButtonTID
        {
            get; set;
        }

        internal string FinishRequirement
        {
            get; set;
        }

        internal string Chest
        {
            get; set;
        }

        internal int WaitTimeMS
        {
            get; set;
        }

        internal string FileName
        {
            get; set;
        }

        internal int PopupCorner
        {
            get; set;
        }

        internal string PopupExportName
        {
            get; set;
        }

        internal string BubbleExportName
        {
            get; set;
        }

        internal string Sound
        {
            get; set;
        }

        internal bool Darkening
        {
            get; set;
        }

        internal string BubbleObject
        {
            get; set;
        }

        internal string OverlayExportName
        {
            get; set;
        }

        internal string SpellDragExportName
        {
            get; set;
        }

        internal string SpellToCast
        {
            get; set;
        }

        internal bool ForceSpellTile
        {
            get; set;
        }

        internal bool DisableOtherSpells
        {
            get; set;
        }

        internal int SpellTileX
        {
            get; set;
        }

        internal int SpellTileY
        {
            get; set;
        }

        internal bool DisableSpells
        {
            get; set;
        }

        internal bool HideCombatUI
        {
            get; set;
        }

        internal bool DisableTroopMovement
        {
            get; set;
        }

        internal bool DisableLeaderMovement
        {
            get; set;
        }

        internal bool DisableSpawnPoints
        {
            get; set;
        }

        internal bool DisableOpponentSpells
        {
            get; set;
        }

        internal bool PauseCombat
        {
            get; set;
        }

        internal string Dependency
        {
            get; set;
        }

        internal int Priority
        {
            get; set;
        }

        internal string Taunt
        {
            get; set;
        }

        internal bool HighlightTargetsOnManaFull
        {
            get; set;
        }

        internal bool DisableBattleStartScreen
        {
            get; set;
        }

        internal int NpcMatchesPlayed
        {
            get; set;
        }

        internal bool DisableBattleMenu
        {
            get; set;
        }

        internal int CloseAutomaticallyAfterSeconds
        {
            get; set;
        }

        internal int GroupMod
        {
            get; set;
        }

        internal int GroupValue
        {
            get; set;
        }

    }
}