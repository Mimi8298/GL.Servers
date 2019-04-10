namespace GL.Servers.BS.Logic
{
    using GL.Servers.BS.Extensions;
    using GL.Servers.BS.Files;
    using GL.Servers.BS.Files.CSV_Logic;
    using GL.Servers.BS.Logic.Enums;
    using GL.Servers.BS.Logic.Slots.Items;

    internal class Reward
    {
        public const int LegendaryCardChance = 4;
        public const int EpicCardChance = 12;
        public const int RareCardChance = 30;

        public const int CardChance = 60;

        public const int BigDustPackChance = 5;
        public const int MediumDustPackChance = 15;
        public const int SmallDustPackChance = 30;

        internal static BrawlBox RandomizeBox(Player Player)
        {
            BrawlBox Box = new BrawlBox();

            if (Player.Device.Random.Next(100) >= 100 - CardChance)
            {
                Box.CardData = Reward.RandomizeCard(Player);
            }
            else
            {
                Reward.RandomizeElixir(Player, Box);
            }

            return Box;
        }

        private static Cards RandomizeCard(Player Player)
        {
            int Random = Player.Device.Random.Next(1001);

            if (Random >= 1000 - LegendaryCardChance * 10)
            {
                Cards Card = GameDatas.UnlockableLegendaryCards[Player.Device.Random.Next(GameDatas.UnlockableLegendaryCards.Length)];

                if (!string.IsNullOrEmpty(Card.RequiresCard))
                {
                    if (!Player.Deck.ContainsKey(CSV.Tables.Get(Gamefile.Cards).GetData(Card.Name).GlobalID))
                    {
                        return Reward.RandomizeCard(Player);
                    }
                }

                return Card;
            }

            if (Random >= 1000 - EpicCardChance * 10)
            {
                Cards Card = GameDatas.UnlockableEpicCards[Player.Device.Random.Next(GameDatas.UnlockableEpicCards.Length)];

                if (!string.IsNullOrEmpty(Card.RequiresCard))
                {
                    if (!Player.Deck.ContainsKey(CSV.Tables.Get(Gamefile.Cards).GetData(Card.Name).GlobalID))
                    {
                        return Reward.RandomizeCard(Player);
                    }
                }

                return Card;
            }

            if (Random >= 1000 - RareCardChance * 10)
            {
                Cards Card = GameDatas.UnlockableRareCards[Player.Device.Random.Next(GameDatas.UnlockableRareCards.Length)];

                if (!string.IsNullOrEmpty(Card.RequiresCard))
                {
                    if (!Player.Deck.ContainsKey(CSV.Tables.Get(Gamefile.Cards).GetData(Card.Name).GlobalID))
                    {
                        return Reward.RandomizeCard(Player);
                    }
                }

                return Card;
            }
            else
            {
                Cards Card = GameDatas.UnlockableCommonCards[Player.Device.Random.Next(GameDatas.UnlockableCommonCards.Length)];

                if (!string.IsNullOrEmpty(Card.RequiresCard))
                {
                    if (!Player.Deck.ContainsKey(CSV.Tables.Get(Gamefile.Cards).GetData(Card.Name).GlobalID))
                    {
                        return Reward.RandomizeCard(Player);
                    }
                }

                return Card;
            }
        }

        private static void RandomizeElixir(Player Player, BrawlBox Box)
        {
            Box.ResourceData = (Resources)CSV.Tables.Get(Gamefile.Resources).GetDataWithInstanceID(6);

            int Random = Player.Device.Random.Next(0, 1000);

            if (Random >= 1000 - BigDustPackChance * 10)
            {
                Box.Count        = 10;
                Box.BackgroundId = 3;

                return;
            }

            if (Random >= 1000 - MediumDustPackChance * 10)
            {
                Box.Count        = 5;
                Box.BackgroundId = 2;

                return;
            }

            if (Random >= 1000 - SmallDustPackChance * 10)
            {
                Box.Count        = 2;
                Box.BackgroundId = 1;

                return;
            }


            Box.Count        = 1;
            Box.BackgroundId = 0;
        }

        internal static int RefundCardReward(Cards Card)
        {
            switch (Card.Rarity)
            {
                case "common":
                    return 1;
                case "rare":
                    return 2;
                case "epic":
                    return 5;
                default:
                    return 10;
            }
        }
    }
}