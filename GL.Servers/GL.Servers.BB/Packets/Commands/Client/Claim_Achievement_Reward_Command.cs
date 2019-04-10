namespace GL.Servers.BB.Packets.Commands.Client
{
    using GL.Servers.BB.Core;
    using GL.Servers.BB.Extensions.Helper;
    using GL.Servers.BB.Files.CSV_Logic;
    using GL.Servers.BB.Logic;
    using GL.Servers.Extensions.List;
    using GL.Servers.Extensions.Binary;

    internal class Claim_Achievement_Reward_Command : Command
    {
        internal AchievementData AchievementData;

        /// <summary>
        /// Gets the command type.
        /// </summary>
        internal override int Type
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets a value indicate whether the command is allowed in attack state.
        /// </summary>
        internal override bool AllowInAttackState
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicate whether the command is allowed in home state.
        /// </summary>
        internal override bool AllowInHomeState
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets a value indicate whether the command is allowed in visit state.
        /// </summary>
        internal override bool AllowInVisitState
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicate whether the command is a debug command.
        /// </summary>
        internal override bool IsDebugCommand
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicate whether the command is a server command.
        /// </summary>
        internal override bool IsServerCommand
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Claim_Achievement_Reward_Command"/> class.
        /// </summary>
        public Claim_Achievement_Reward_Command() : base()
        {
            // Claim_Achievement_Reward_Command.
        }

        /// <summary>
        /// Decodes this command.
        /// </summary>
        /// <param name="reader"></param>
        internal override void Decode(Reader reader)
        {
            this.AchievementData = reader.ReadData<AchievementData>();
            base.Decode(reader);
        }

        /// <summary>
        /// Encodes this command.
        /// </summary>
        /// <param name="reader"></param>
        internal override void Encode(ByteWriter writer)
        {
            writer.AddData(this.AchievementData);
            base.Encode(writer);
        }

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="Player">The player.</param>
        internal override void Execute(Level Level)
        {
            if (this.AchievementData != null)
            {
                if (Level.Player.AchievementProgresses.GetCount(this.AchievementData) >= this.AchievementData.ActionCount)
                {
                    if (!Level.Player.AchievementClaimed.Contains(this.AchievementData.GlobalID))
                    {
                        Level.Player.AchievementClaimed.Add(this.AchievementData.GlobalID);
                        Level.Player.Diamonds += this.AchievementData.DiamondReward;
                    }
                    else
                        Logging.Error(this.GetType(), $"Execute() - Failed to claim achievement reward. Already claimed, avatarId: {Level.Player.HighID}-{Level.Player.LowID}, achievement: {this.AchievementData.GlobalID}."); // assertlogic()
                }
            }
            else 
                Logging.Error(this.GetType(), "Execute() - Failed to claim achievement reward. Missing achievement."); // assertlogic()
        }
    }
}