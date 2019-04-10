namespace GL.Servers.BS.Packets
{
    using GL.Servers.BS.Packets.Messages.Client;
    using System;
    using System.Collections.Generic;

    using GL.Servers.BS.Packets.Commands.Client;
    using GL.Servers.BS.Packets.Commands.Server;

    using Change_Name = GL.Servers.BS.Packets.Messages.Client.Change_Name;

    internal static class Factory
    {
        /// <summary>
        /// The delimiter used to detect if x string is a call-command.
        /// </summary>
        internal const char Delimiter = '/';

        internal static readonly Dictionary<ushort, Type> Messages = new Dictionary<ushort, Type>();
        internal static readonly Dictionary<ushort, Type> Commands = new Dictionary<ushort, Type>();
        internal static readonly Dictionary<string, Type> Debugs   = new Dictionary<string, Type>();

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        internal static void Initialize()
        {
            Factory.LoadMessages();
            Factory.LoadCommands();
            Factory.LoadDebugs();
        }

        /// <summary>
        /// Loads the game messages.
        /// </summary>
        private static void LoadMessages()
        {
            Factory.Messages.Add(10101, typeof(Authentification));
            Factory.Messages.Add(10107, typeof(Client_Capabilities));
            Factory.Messages.Add(10108, typeof(Keep_Alive));
            Factory.Messages.Add(10113, typeof(Get_Device_Token));
            Factory.Messages.Add(10160, typeof(Apple_Billing));

            Factory.Messages.Add(10212, typeof(Change_Name));

            Factory.Messages.Add(10512, typeof(Gamecenter_Friends));
            Factory.Messages.Add(10513, typeof(Facebook_Friends));

            Factory.Messages.Add(10555, typeof(Battle_Command));

            Factory.Messages.Add(14101, typeof(Battle_Leave));
            Factory.Messages.Add(14102, typeof(Execute_Commands));
            Factory.Messages.Add(14103, typeof(Matchmaking));
            Factory.Messages.Add(14106, typeof(Matchmaking_Cancel));
            Factory.Messages.Add(14109, typeof(Go_Home));
            Factory.Messages.Add(14110, typeof(Battle_End));
            Factory.Messages.Add(14113, typeof(Ask_Profile));

            Factory.Messages.Add(14201, typeof(Bind_Facebook_Account));
            Factory.Messages.Add(14211, typeof(Unbind_Facebook_Account));
            Factory.Messages.Add(14212, typeof(Bind_Gamecenter_Account));

            Factory.Messages.Add(14301, typeof(Clan_Create));
            Factory.Messages.Add(14302, typeof(Clan_Ask));
            Factory.Messages.Add(14303, typeof(Clan_Joinable));
            Factory.Messages.Add(14305, typeof(Clan_Join));
            Factory.Messages.Add(14308, typeof(Clan_Leave));
            Factory.Messages.Add(14315, typeof(Clan_Chat));
            Factory.Messages.Add(14316, typeof(Clan_Edit));
            Factory.Messages.Add(14324, typeof(Clan_Search));

            Factory.Messages.Add(14350, typeof(Create_Gameroom));
            Factory.Messages.Add(14358, typeof(Join_Gameroom));
            Factory.Messages.Add(14359, typeof(Chat_Gameroom));

            Factory.Messages.Add(14403, typeof(Top_Global_Players));
        }

        /// <summary>
        /// Loads the game commands.
        /// </summary>
        private static void LoadCommands()
        {
            Factory.Commands.Add(1, typeof(Battle_Move_Brawler));

            Factory.Commands.Add(201, typeof(Change_Name_Callback));
            Factory.Commands.Add(203, typeof(Buy_Brawl_Box_Callback));

            Factory.Commands.Add(500, typeof(Buy_Brawl_Box));
            Factory.Commands.Add(502, typeof(Upgrade_Brawler));
            Factory.Commands.Add(504, typeof(Event_Earn_Gold));
            Factory.Commands.Add(506, typeof(Select_Thumbnail));

            Factory.Commands.Add(509, typeof(Settings_Joystick));
            Factory.Commands.Add(513, typeof(Setting_Hints));
        }

        /// <summary>
        /// Loads the debug commands.
        /// </summary>
        private static void LoadDebugs()
        {

        }
    }
}