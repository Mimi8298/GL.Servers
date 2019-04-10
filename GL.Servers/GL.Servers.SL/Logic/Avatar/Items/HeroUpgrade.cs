namespace GL.Servers.SL.Logic.Avatar.Items
{
    using GL.Servers.SL.Files.CSV_Logic.Logic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    internal class HeroUpgrade
    {
        internal Player Player;

        [JsonProperty("hero_data")] internal int HeroData;
        [JsonProperty("upg_timer")] internal Timer Timer;

        internal bool Upgrading
        {
            get
            {
                if (this.HeroData != -1)
                {
                    if (this.Timer.Started)
                    {
                        return true;
                    }

                    this.HeroData = -1;
                }

                return false;
            }
        }

        public HeroUpgrade()
        {
            this.Timer = new Timer();
        }

        public HeroUpgrade(Player Player)
        {
            this.Player = Player;
            this.Timer  = new Timer(Player.Time);
        }

        internal void AdjustSubTick()
        {
            if (this.Upgrading)
            {
                this.Timer.AdjustSubTick();
            }
        }

        internal bool CanUpgrade(HeroData Hero)
        {
            if (!this.Upgrading)
            {
                if (this.Player.HeroLevels.ContainsKey(Hero.GlobalID))
                {
                    if (Hero.Cost.Count > this.Player.HeroLevels[Hero.GlobalID].Count + 1)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        internal void FastForward(int Seconds)
        {
            if (this.Upgrading)
            {
                this.Timer.FastForward(Seconds);

                if (this.Timer.RemainingSecs <= 0)
                {
                    this.FinishUpgrade();
                }
            }
        }

        internal void FinishUpgrade()
        {
            if (this.Upgrading)
            {
                this.Player.HeroLevels.AddItem(this.HeroData, 1);
                this.Timer.StopTimer();
                this.HeroData = -1;
            }
        }

        internal void StartUpgrade(HeroData Hero)
        {
            if (this.CanUpgrade(Hero))
            {
                this.HeroData = Hero.GlobalID;
                this.Timer.StartTimer(this.Player.Time, Hero.UpgradeTimeSeconds[this.Player.HeroLevels.GetCount(Hero.GlobalID)]);

                if (this.Timer.RemainingSecs <= 0)
                {
                    this.FinishUpgrade();
                }
            }
        }

        internal void Tick()
        {
            if (this.Timer.Started)
            {
                if (this.Timer.RemainingSecs <= 0)
                {
                    this.FinishUpgrade();
                }
            }
        }

        internal void Save(JObject Json)
        {
            if (this.Upgrading)
            {
                Json.Add("upgradingHero", this.HeroData);
                Json.Add("upgradingHeroTime", this.Timer.RemainingSecs);
            }
        }
    }
}