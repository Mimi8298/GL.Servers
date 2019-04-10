namespace GL.Servers.CR.Logic
{
    using GL.Servers.CR.Extensions.Helper;
    using GL.Servers.CR.Extensions.Utils;
    using GL.Servers.DataStream;

    using Newtonsoft.Json.Linq;

    internal class Timer
    {
        internal int TotalSubTick;
        internal int EndSubTick;
        internal int EndTimestamp;

        /// <summary>
        /// Initializes a new instance of the timer.
        /// </summary>
        public Timer()
        {
            this.EndTimestamp = -1;
        }

        /// <summary>
        /// Adjustes the EndSubTick variable.
        /// </summary>
        internal void AdjustEndSubTick(Time Time)
        {
            if (this.EndSubTick > 0)
            {
                this.EndSubTick -= Time.SubTick;

                if (this.EndSubTick < 0)
                {
                    this.EndSubTick = 0;
                }
            }
        }

        /// <summary>
        /// Creates a fast forward.
        /// </summary>
        internal void FastForward(int Seconds)
        {
            if (this.EndSubTick > 0)
            {
                this.EndSubTick -= Time.GetSecondsInTicks(Seconds);

                if (this.EndSubTick < 0)
                {
                    this.EndSubTick = 0;
                }
            }
        }

        /// <summary>
        /// Gets the remaining seconds before that the timer is finished.
        /// </summary>
        internal int GetRemainingSeconds(Time Time)
        {
            if (this.EndSubTick > 0)
            {
                return Math.Max(this.EndSubTick / 20, 1);
            }

            return 0;
        }

        /// <summary>
        /// Gets if this timer is finished.
        /// </summary>
        internal bool IsFinished(Time Time)
        {
            return Time.SubTick >= this.EndSubTick;
        }

        /// <summary>
        /// Starts the timer.
        /// </summary>
        internal void StartTimer(Time Time, int Seconds)
        {
            this.StartTimer(Time, Seconds, TimeUtil.Timestamp);
        }

        /// <summary>
        /// Starts the timer.
        /// </summary>
        internal void StartTimer(Time Time, int Seconds, int Timestamp)
        {
            this.EndSubTick = Time.SubTick + 20 * Seconds;
            this.TotalSubTick = Time.SubTick + 20 * Seconds;

            if (this.EndTimestamp == -1)
            {
                this.EndTimestamp = Seconds + Timestamp;
            }
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal void Decode(ByteStream Reader)
        {
            this.EndSubTick = Reader.ReadVInt();
            this.TotalSubTick = Reader.ReadVInt();
            this.EndTimestamp = Reader.ReadVInt();
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal void Encode(ByteStream Packet)
        {
            Packet.AddVInt(this.EndSubTick);
            Packet.AddVInt(this.TotalSubTick);
            Packet.AddVInt(this.EndTimestamp);
        }
        
        /// <summary>
        /// Loads this instance from json.
        /// </summary>
        internal void Load(JToken Json)
        {
            JsonHelper.GetJsonNumber(Json, "ticks", out this.TotalSubTick);
            JsonHelper.GetJsonNumber(Json, "remaining", out this.EndSubTick);
            JsonHelper.GetJsonNumber(Json, "timestamp", out this.EndTimestamp);
        }

        /// <summary>
        /// Saves this instance to json.
        /// </summary>
        internal JObject Save(Time Time)
        {
            JObject Json = new JObject();

            Json.Add("ticks", this.TotalSubTick);
            Json.Add("remaining", this.EndSubTick);
            Json.Add("timestamp", Math.Max(1, this.EndSubTick - Time.SubTick));

            return Json;
        }
    }
}