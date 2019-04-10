namespace GL.Servers.CoC.Logic.Attack
{
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Packets;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    internal class Replay
    {
        internal bool StartRecorded;
        internal bool Ended;

        internal Home Level;

        internal Player Attacker;
        internal Player Defender;

        internal int Timestamp;
        internal int EndSubTick;
        internal int SkippedPreparationTime;

        internal JObject Json;
        internal JArray Commands;
        
        public Replay(Home Level, Player Attacker, Player Defender)
        {
            this.Json     = new JObject();
            this.Commands = new JArray();

            this.Level = Level;
            this.Attacker = Attacker;
            this.Defender = Defender;
        }

        internal string GetJSON()
        {
            if (!this.StartRecorded)
            {
                this.RecordStart();
            }

            this.Json["end_tick"] = this.EndSubTick;

            if (this.SkippedPreparationTime > 0)
            {
                this.Json["skip_time"] = this.SkippedPreparationTime;
            }

            this.Json["cmd"] = this.Commands;

            return this.Json.ToString(Formatting.None);
        }

        internal void RecordStart()
        {
            if (!this.StartRecorded)
            {
                this.StartRecorded = true;

                this.Json.Add("level", this.Level.Save());
                this.Json.Add("attacker", this.Attacker.Save());
                this.Json.Add("defender", this.Defender.Save());

                this.Json.Add("end_tick", this.EndSubTick);

                if (this.SkippedPreparationTime > 0)
                {
                    this.Json["skip_time"] = this.SkippedPreparationTime;
                }

                this.Json.Add("cmd", this.Commands);
            }
        }

        internal void RecordCommand(Command Command)
        {
            JObject Item = new JObject();

            Item.Add("t", Command.Type);
            Item.Add("c", Command.Save());

            this.Commands.Add(Item);
        }

        internal void SetSkippedPreparationTime(int Skip)
        {
            this.SkippedPreparationTime = Skip;
            this.Json["skip_time"] = Skip;
        }
    }
}