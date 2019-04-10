namespace GL.Servers.CoC.Packets.Messages.Client.Alliance
{
    using System.Linq;
    using GL.Servers.CoC.Core;
    using GL.Servers.CoC.Core.Network;
    using GL.Servers.CoC.Logic;
    using GL.Servers.CoC.Packets.Enums;
    using GL.Servers.CoC.Packets.Messages.Server.Alliance;
    using GL.Servers.Extensions.Binary;
    using GL.Servers.Logic.Enums;

    internal class Ask_For_Joinable_Alliance_List_Message : Message
    {
        /// <summary>
        /// Gets a value indicating the message type.
        /// </summary>
        internal override short Type
        {
            get
            {
                return 14303;
            }
        }

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
        /// Initializes a new instance of the <see cref="Ask_For_Joinable_Alliance_List_Message"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        /// <param name="Reader">The reader.</param>
        public Ask_For_Joinable_Alliance_List_Message(Device Device, Reader Reader) : base(Device, Reader)
        {
            // Ask_For_Joinable_Alliance_List_Message.
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal override void Process()
        {
            new Joinable_Alliance_List_Message(this.Device, Resources.Clans.Values.Where(T => T.Header.Type == Hiring.OPEN && T.Header.NumberOfMembers > 0 && T.Header.NumberOfMembers < 50 && T.Header.RequiredScore <= this.Device.GameMode.Level.Player.Score).OrderByDescending(T => T.Header.Score).ToArray()).Send();
        }
    }
}