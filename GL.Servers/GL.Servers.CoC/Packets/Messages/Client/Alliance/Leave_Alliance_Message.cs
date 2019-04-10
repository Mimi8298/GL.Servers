namespace GL.Servers.CoC.Packets.Messages.Client.Alliance
{
    using System.Linq;
    using GL.Servers.CoC.Extensions;
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Logic.Clan;
    using GL.Servers.CoC.Logic.Clan.Enums;
    using GL.Servers.CoC.Logic.Clan.Items;
    using GL.Servers.CoC.Logic.Enums;
    using GL.Servers.CoC.Packets.Enums;
    using GL.Servers.Extensions.Binary;

    internal class Leave_Alliance_Message : Message
    {
        private string Message;

        /// <summary>
        /// Gets a value indicating the service node of message.
        /// </summary>
        internal override ServiceNode Node
        {
            get
            {
                return ServiceNode.Alliance;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Leave_Alliance_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Leave_Alliance_Message(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Leave_Alliance_Message.
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            if (this.Device.GameMode.Level.Player.InAlliance)
            {
                if (this.Device.GameMode.Level.Player.Alliance.Members.Slots.TryRemove(this.Device.GameMode.Level.Player.PlayerID, out Member Member))
                {
                    if (Member.Role == Role.Leader)
                    {
                        Member NextLeader = null;

                        foreach (Member member in this.Device.GameMode.Level.Player.Alliance.Members.Slots.Values.ToArray())
                        {
                            if (NextLeader == null || member.Role.Superior(NextLeader.Role))
                            {
                                NextLeader = member;

                                if (member.Role == Role.CoLeader)
                                    break;
                            }
                        }

                        /*
                        if (NextLeader != null)
                        {
                            NextLeader.Role = Role.Leader;
                            Player Player   = NextLeader.Player;

                            if (Player != null && Player.Connected)
                            {
                                Player.Level.GameMode.CommandManager.AddCommand(null); // TODO Send promote command.
                            }

                            this.Device.GameMode.Level.Player.Alliance.Streams.AddEntry(new AllianceEventStreamEntry(NextLeader, Member, StreamEvent.Promoted));

                            this.Device.GameMode.CommandManager.AddCommand(null); // TODO Send Demote command.

                            this.Device.GameMode.Level.Player.Alliance.Streams.AddEntry(new AllianceEventStreamEntry(NextLeader, NextLeader, StreamEvent.Demoted));
                        }
                        */

                        this.Device.GameMode.CommandManager.AddCommand(null); // TODO Send Leave Alliance command.
                        this.Device.GameMode.Level.Player.Alliance.Streams.AddEntry(new AllianceEventStreamEntry(NextLeader, NextLeader, StreamEvent.Left));
                    }
                }
            }
        }
    }
}