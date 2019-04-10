namespace GL.Servers.CoC.Packets.Commands.Client
{
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Extensions.Helper;
    using GL.Servers.CoC.Files.CSV_Logic.Logic;

    using GL.Servers.Extensions.Binary;

    internal class Claim_Achievement_Reward_Command : Command
    {
        private AchievementData Data;

        /// <summary>
        /// Gets a value indicating the command type.
        /// </summary>
        internal override int Type
        {
            get
            {
                return 523;
            }
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal override void Decode(Reader Reader)
        {
            this.Data = Reader.ReadData<AchievementData>();
            base.Decode(Reader);
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        internal override void Execute(Level Level)
        {
            if (this.Data != null)
            {
                if (!Level.Player.Achievements.Contains(this.Data))
                {
                    Level.Player.AddDiamonds(this.Data.DiamondReward);
                    Level.Player.AddExperience(this.Data.ExpReward);

                    Level.Player.Achievements.Add(this.Data);
                }
            }
        }
    }
}