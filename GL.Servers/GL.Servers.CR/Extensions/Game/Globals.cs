namespace GL.Servers.CR.Extensions.Game
{
    using System.Collections.Generic;

    using GL.Servers.CR.Files;
    using GL.Servers.CR.Files.Logic;
    using GL.Servers.CR.Logic.Enums;

    internal class Globals
    {
        internal static int StartingGold;
        internal static int StartingDiamonds;
        internal static bool MultipleDecks;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        internal static void Initialize()
        {
            Globals.StartingGold = CSV.Tables.Get(Gamefile.Global).GetData<GlobalData>("STARTING_GOLD").NumberValue;
            Globals.StartingDiamonds = CSV.Tables.Get(Gamefile.Global).GetData<GlobalData>("STARTING_DIAMONDS").NumberValue;
            Globals.MultipleDecks = CSV.Tables.Get(Gamefile.Global).GetData<GlobalData>("MULTIPLE_DECKS_ENABLED").BooleanValue;
        }
    }
}