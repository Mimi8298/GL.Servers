namespace GL.Servers.CoC.Logic
{
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Extensions.Game;
    using GL.Servers.CoC.Extensions.Helper;

    using Newtonsoft.Json.Linq;

    internal class BunkerComponent : Component
    {
        internal override int Type
        {
            get
            {
                return 7;
            }
        }

        internal Timer UnitRequestTimer;
        internal Timer ClanMailTimer;
        internal Timer ShareReplayTimer;
        internal Timer ElderKickTimer;
        internal Timer ChallengeTimer;
        internal Timer ArrangeWarTimer;

        internal bool CanSendUnitRequest
        {
            get
            {
                return this.UnitRequestTimer.GetRemainingSeconds(this.Parent.Level.Time) <= 0;
            }
        }

        internal bool CanSendClanMail
        {
            get
            {
                return this.ClanMailTimer.GetRemainingSeconds(this.Parent.Level.Time) <= 0;
            }
        }

        internal bool CanShareReplay
        {
            get
            {
                return this.ShareReplayTimer.GetRemainingSeconds(this.Parent.Level.Time) <= 0;
            }
        }

        internal bool CanElderKick
        {
            get
            {
                return this.ElderKickTimer.GetRemainingSeconds(this.Parent.Level.Time) <= 0;
            }
        }

        internal bool CanCreateChallenge
        {
            get
            {
                return this.ChallengeTimer.GetRemainingSeconds(this.Parent.Level.Time) <= 0;
            }
        }

        internal bool CanArrangeWar
        {
            get
            {
                return this.ChallengeTimer.GetRemainingSeconds(this.Parent.Level.Time) <= 0;
            }
        }

        public BunkerComponent(GameObject GameObject) : base(GameObject)
        {
            this.UnitRequestTimer = new Timer();
            this.ClanMailTimer    = new Timer();
            this.ShareReplayTimer = new Timer();
            this.ElderKickTimer   = new Timer();
            this.ChallengeTimer   = new Timer();
            this.ArrangeWarTimer  = new Timer();
        }
        
        internal override void FastForwardTime(int Secs)
        {
            if (this.UnitRequestTimer.Started)
            {
                this.UnitRequestTimer.FastForward(Secs);
            }

            if (this.ClanMailTimer.Started)
            {
                this.ClanMailTimer.FastForward(Secs);
            }

            if (this.ShareReplayTimer.Started)
            {
                this.ShareReplayTimer.FastForward(Secs);
            }

            if (this.ElderKickTimer.Started)
            {
                this.ElderKickTimer.FastForward(Secs);
            }

            if (this.ChallengeTimer.Started)
            {
                this.ChallengeTimer.FastForward(Secs);
            }

            if (this.ArrangeWarTimer.Started)
            {
                this.ArrangeWarTimer.FastForward(Secs);
            }
        }

        internal override void Load(JToken Json)
        {
            if (JsonHelper.GetJsonNumber(Json, "unit_req_time", out int UnitRequestTime))
            {
                if (UnitRequestTime >= 0)
                {
                    if (UnitRequestTime > Globals.AllianceTroopRequestCooldown)
                    {
                        UnitRequestTime = Globals.AllianceTroopRequestCooldown;
                    }
                }

                this.UnitRequestTimer.StartTimer(this.Parent.Level.Time, UnitRequestTime);
            }

            if (JsonHelper.GetJsonNumber(Json, "clan_mail_time", out int ClanMailTime))
            {
                if (ClanMailTime >= 0)
                {
                    if (ClanMailTime > Globals.ClanMailCooldown)
                    {
                        ClanMailTime = Globals.ClanMailCooldown;
                    }
                }

                this.ClanMailTimer.StartTimer(this.Parent.Level.Time, ClanMailTime);
            }

            if (JsonHelper.GetJsonNumber(Json, "share_replay_time", out int ShareReplayTime))
            {
                if (ShareReplayTime >= 0)
                {
                    if (ShareReplayTime > Globals.ReplayShareCooldown)
                    {
                        ShareReplayTime = Globals.ReplayShareCooldown;
                    }
                }

                this.ShareReplayTimer.StartTimer(this.Parent.Level.Time, ShareReplayTime);
            }

            if (JsonHelper.GetJsonNumber(Json, "elder_kick_time", out int ElderKickTime))
            {
                if (ElderKickTime >= 0)
                {
                    if (ElderKickTime > Globals.ElderKickCooldown)
                    {
                        ElderKickTime = Globals.ElderKickCooldown;
                    }
                }

                this.ElderKickTimer.StartTimer(this.Parent.Level.Time, ElderKickTime);
            }

            if (JsonHelper.GetJsonNumber(Json, "challenge_time", out int ChallengeTime))
            {
                if (ChallengeTime >= 0)
                {
                    if (ChallengeTime > Globals.ChallengeCooldown)
                    {
                        ChallengeTime = Globals.ChallengeCooldown;
                    }
                }

                this.ChallengeTimer.StartTimer(this.Parent.Level.Time, ChallengeTime);
            }

            if (JsonHelper.GetJsonNumber(Json, "arrwar_time", out int ArrangeWarTime))
            {
                if (ArrangeWarTime >= 0)
                {
                    if (ArrangeWarTime > Globals.ArrangeWarCooldown)
                    {
                        ArrangeWarTime = Globals.ArrangeWarCooldown;
                    }
                }

                this.ArrangeWarTimer.StartTimer(this.Parent.Level.Time, ArrangeWarTime);
            }
        }

        internal override void Save(JObject Json)
        {
            if (this.UnitRequestTimer.Started)
            {
                Json.Add("unit_req_time", this.UnitRequestTimer.GetRemainingSeconds(this.Parent.Level.Time));
            }

            if (this.ClanMailTimer.Started)
            {
                Json.Add("clan_mail_time", this.ClanMailTimer.GetRemainingSeconds(this.Parent.Level.Time));
            }

            if (this.ShareReplayTimer.Started)
            {
                Json.Add("share_replay_time", this.ShareReplayTimer.GetRemainingSeconds(this.Parent.Level.Time));
            }

            if (this.ElderKickTimer.Started)
            {
                Json.Add("elder_kick_time", this.ElderKickTimer.GetRemainingSeconds(this.Parent.Level.Time));
            }

            if (this.ChallengeTimer.Started)
            {
                Json.Add("challenge_time", this.ChallengeTimer.GetRemainingSeconds(this.Parent.Level.Time));
            }

            if (this.ArrangeWarTimer.Started)
            {
                Json.Add("arrwar_time", this.ArrangeWarTimer.GetRemainingSeconds(this.Parent.Level.Time));
            }
        }

        internal override void Tick()
        {
            if (this.UnitRequestTimer.Started)
            {
                if (this.UnitRequestTimer.GetRemainingSeconds(this.Parent.Level.Time) <= 0)
                {
                    this.UnitRequestTimer.StopTimer();
                }
            }

            if (this.ClanMailTimer.Started)
            {
                if (this.ClanMailTimer.GetRemainingSeconds(this.Parent.Level.Time) <= 0)
                {
                    this.ClanMailTimer.StopTimer();
                }
            }

            if (this.ShareReplayTimer.Started)
            {
                if (this.ShareReplayTimer.GetRemainingSeconds(this.Parent.Level.Time) <= 0)
                {
                    this.ShareReplayTimer.StopTimer();
                }
            }

            if (this.ElderKickTimer.Started)
            {
                if (this.ElderKickTimer.GetRemainingSeconds(this.Parent.Level.Time) <= 0)
                {
                    this.ElderKickTimer.StopTimer();
                }
            }

            if (this.ChallengeTimer.Started)
            {
                if (this.ChallengeTimer.GetRemainingSeconds(this.Parent.Level.Time) <= 0)
                {
                    this.ChallengeTimer.StopTimer();
                }
            }

            if (this.ArrangeWarTimer.Started)
            {
                if (this.ArrangeWarTimer.GetRemainingSeconds(this.Parent.Level.Time) <= 0)
                {
                    this.ArrangeWarTimer.StopTimer();
                }
            }
        }
    }
}