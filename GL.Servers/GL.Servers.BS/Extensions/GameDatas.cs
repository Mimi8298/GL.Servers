namespace GL.Servers.BS.Extensions
{
    using System.Linq;

    using GL.Servers.BS.Files;
    using GL.Servers.BS.Files.CSV_Logic;
    using GL.Servers.BS.Logic.Enums;

    internal class GameDatas
    {
        internal static Cards[] Cards;

        internal static Cards[] UnlockableCommonCards;
        internal static Cards[] UnlockableRareCards;
        internal static Cards[] UnlockableEpicCards;
        internal static Cards[] UnlockableLegendaryCards;

        internal static void Initialize()
        {
            GameDatas.Cards = CSV.Tables.Get(Gamefile.Cards).Datas.Select(T => (Cards) T).ToArray();
            GameDatas.UnlockableCommonCards = GameDatas.Cards.Where(T => T.Type == "unlock" && T.Rarity == "common").ToArray();
            GameDatas.UnlockableRareCards = GameDatas.Cards.Where(T => T.Type == "unlock" && T.Rarity == "rare").ToArray();
            GameDatas.UnlockableEpicCards = GameDatas.Cards.Where(T => T.Type == "unlock" && T.Rarity == "epic").ToArray();
            GameDatas.UnlockableLegendaryCards = GameDatas.Cards.Where(T => T.Type == "unlock" && T.Rarity == "legendary").ToArray();
        }
    }
}