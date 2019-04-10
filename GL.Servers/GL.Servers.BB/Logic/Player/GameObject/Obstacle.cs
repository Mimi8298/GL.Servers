namespace GL.Servers.BB.Logic.GameObject
{
    using GL.Servers.BB.Core;
    using GL.Servers.BB.Extensions.Helper;
    using GL.Servers.BB.Files.CSV_Logic;
    using GL.Servers.BB.Files.CSV_Helpers;
    using Newtonsoft.Json.Linq;

    internal class Obstacle : GameObject
    {
        internal ObstacleData ObstacleData;
        internal Timer ClearingTimer;

        internal int State;

        internal bool CanStartClearing
        {
            get
            {
                return this.ClearingTimer == null && this.State == 0;
            }
        }

        internal int RemainingClearingTime
        {
            get
            {
                return this.ClearingTimer != null ? this.ClearingTimer.GetRemainingSeconds(this.Home.Time) : 0;
            }
        }

        internal int RemainingClearingTimeMS
        {
            get
            {
                return this.ClearingTimer != null ? this.ClearingTimer.GetRemainingMS(this.Home.Time) : 0;
            }
        }

        internal int RemainingPrototypeTime
        {
            get
            {
                return 1;
            }
        }

        internal bool IsClearingOnGoing
        {
            get
            {
                return this.ClearingTimer != null;
            }
        }
        
        internal bool IsPassable
        {
            get
            {
                return this.ObstacleData.Passable;
            }
        }

        internal bool IsTree
        {
            get
            {
                return this.ObstacleData.LottResourceData.GlobalID == 3000002; // Wood Data
            }
        }
        
        internal override int HeightInTiles
        {
            get
            {
                return this.ObstacleData.Height;
            }
        }

        internal override int WidthInTiles
        {
            get
            {
                return this.ObstacleData.Width;
            }
        }

        internal override int Type
        {
            get
            {
                return 3;
            }
        }

        internal override bool IsBuilding
        {
            get
            {
                return true;
            }
        }

        internal override bool IsUnbuildable
        {
            get
            {
                return false;
            }
        }

        internal override bool ShouldDestruct
        {
            get
            {
                return this.State > 999;
            }
        }

        public Obstacle(Data Data, Home Home) : base(Data, Home)
        {
            this.ObstacleData = (ObstacleData) Data;
        }

        internal override void Load(JToken Token)
        {
            if (JsonHelper.GetInt(Token["clear_t"], out int ClearTime))
            {
                this.ClearingTimer = new Timer();
                this.ClearingTimer.StartTimer(ClearTime, this.Home.Time);
                this.Home.WorkerManager.AllocateWorker(this);
            }

            base.Load(Token);
        }

        internal override void Save(JObject Json)
        {
            if (this.ClearingTimer != null)
            {
                Json.Add("clear_t", this.ClearingTimer.GetRemainingSeconds(this.Home.Time));
            }

            base.Save(Json);
        }
    }
}