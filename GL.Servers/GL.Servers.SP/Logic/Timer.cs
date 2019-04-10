namespace GL.Servers.SP.Logic
{
    using GL.Servers.SP.Extensions.Helper;
    using Newtonsoft.Json.Linq;

    internal class Timer
    {
        internal int EndSubTick;

        internal bool Started
        {
            get
            {
                return this.EndSubTick != -1;
            }
        }

        public Timer()
        {
            // Timer.
        }
        
        internal void StartTimer(Time Time, int Seconds)
        {
            this.EndSubTick = Time.ClientSubTick + Time.GetSecondsInTicks(Seconds);
        }

        internal void IncreaseTimer(int Seconds)
        {
            this.EndSubTick += Time.GetSecondsInTicks(Seconds);
        }

        internal void StopTimer()
        {
            this.EndSubTick = -1;
        }

        internal void FastForward(int Seconds)
        {
            this.EndSubTick -= Time.GetSecondsInTicks(Seconds);
        }

        internal void FastForwardSubTicks(int SubTick)
        {
            if (this.EndSubTick > 0)
            {
                this.EndSubTick -= SubTick;
            }
        }

        internal int GetRemainingSeconds(Time Time)
        {
            int SubTicks = this.EndSubTick - Time.ClientSubTick;

            if (SubTicks > 0)
            {
                int Secs = SubTicks / 20;

                if (Secs % 20 > 0)
                {
                    Secs++;
                }

                return Secs;
            }

            return 0;
        }

        internal int GetRemainingMs(Time Time)
        {
            int SubTicks = this.EndSubTick - Time.ClientSubTick;

            if (SubTicks > 0)
            {
                int MS = 1000 * (SubTicks / 20);

                if (MS % 20 > 0)
                {
                    MS += 6400 * SubTicks >> 7;
                }

                return MS;
            }

            return 0;
        }

        internal void AdjustSubTick(Time Time)
        {
            this.EndSubTick -= Time.ClientSubTick;

            if (this.EndSubTick < 0)
            {
                this.EndSubTick = 0;
            }
        }

        internal void Load(JToken Json)
        {
            JsonHelper.GetJsonNumber(Json, "TicksTotal", out int TicksTotal);
            JsonHelper.GetJsonNumber(Json, "TicksRemaining", out this.EndSubTick);
        }

        internal JObject Save(Time Time)
        {
            JObject Json = new JObject();

            int TicksRemaining = this.EndSubTick - Time.ClientSubTick;

            if (TicksRemaining > 0)
            {
                Json.Add("TicksTotal", this.EndSubTick);
                Json.Add("TicksRemaining", TicksRemaining);
            }

            return Json;
        }
    }
}