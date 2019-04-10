using GL.Servers.GS.Packets.Messages.Client;

namespace GL.Servers.GS.Packets
{
    using System;
    using System.Collections.Generic;

    internal static class Factory
    {
        /// <summary>
        /// The delimiter used to detect if x string is a call-command.
        /// </summary>
        internal const char Delimiter = '/';

        internal static readonly Dictionary<ushort, Type> Messages = new Dictionary<ushort, Type>();
        internal static readonly Dictionary<ushort, Type> Commands = new Dictionary<ushort, Type>();
        internal static readonly Dictionary<string, Type> Debugs  = new Dictionary<string, Type>();

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
            Factory.Messages.Add(10101, typeof(Login));

            Factory.Messages.Add(10200, typeof(CreatePlayer));
            Factory.Messages.Add(10201, typeof(SelectPlayer));
            Factory.Messages.Add(10202, typeof(Unknown10202));
        }

        /// <summary>
        /// Loads the game commands.
        /// </summary>
        private static void LoadCommands()
        {
            /*
      public const int COMMAND_TYPE_SERVER_TIME_COMMAND = 13;      
      public const int COMMAND_TYPE_ADD_PLAYER = 16;      
      public const int COMMAND_TYPE_REMOVE_PLAYER = 17;      
      public const int COMMAND_TYPE_ADD_FRIEND_COMPANION = 26;     
      public const int COMMAND_TYPE_TAKE_MAIL_ATTACHMENTS = 48;     
      public const int COMMAND_TYPE_AVATAR_CHANGE_COMMAND = 49;      

      public const int COMMAND_TYPE_MOVE = 501;      
      public const int COMMAND_TYPE_SET_TARGET = 502;      
      public const int COMMAND_TYPE_SKILL = 503;     
      public const int COMMAND_TYPE_SWAP_ITEM = 505;     
      public const int COMMAND_TYPE_DRAG_SKILL = 506;      
      public const int COMMAND_TYPE_ACCEPT_MISSION = 507;      
      public const int COMMAND_TYPE_RETURN_MISSION = 508;      
      public const int COMMAND_TYPE_ABANDON_MISSION = 509;      
      public const int COMMAND_TYPE_SPECIALIZE = 510;      
      public const int COMMAND_TYPE_TAXI_TO_LEVEL = 512;      
      public const int COMMAND_TYPE_END_TALK = 514;      
      public const int COMMAND_TYPE_BUY_ITEM = 515;      
      public const int COMMAND_TYPE_RESURRECT_PLAYER = 518;      
      public const int COMMAND_TYPE_SET_TUTORIAL_PROGRESS = 520;      
      public const int COMMAND_TYPE_SELL_ITEM = 522;      
      public const int COMMAND_TYPE_ADD_MERCENARY_COMPANION = 525;      
      public const int COMMAND_TYPE_TOGGLE_WEAPON = 530;    
      public const int COMMAND_TYPE_RESPEC = 534;     
      public const int COMMAND_TYPE_UNLOCK_TREASURE_BOX = 538;     
      public const int COMMAND_TYPE_CANCEL_ALL_ACTIONS = 541;      
      public const int COMMAND_TYPE_USE_TELEPORT = 543;      
      public const int COMMAND_TYPE_NEED_ROLL_RESPONSE = 544;      
      public const int COMMAND_TYPE_MODIFY_TRADE = 545;   
      public const int COMMAND_TYPE_CANCEL_TRADE = 546;    
      public const int COMMAND_TYPE_ACCEPT_TRADE = 547;   
      public const int COMMAND_TYPE_ACTIVATE_COMPANION = 550;   
      public const int COMMAND_TYPE_TAKE_DAILY_REWARD = 551;     
      public const int COMMAND_TYPE_STUDY_RECIPE = 554;     
      public const int COMMAND_TYPE_CRAFT_ITEM = 555;     
      public const int COMMAND_TYPE_CLAIM_CRAFTED_ITEM = 556;     
      public const int COMMAND_TYPE_BOOST_CRAFTING_WITH_DIAMONDS = 557;     
      public const int COMMAND_TYPE_BUY_INGREDIENT = 558;    
      public const int COMMAND_TYPE_BUY_BAG = 559;      
      public const int COMMAND_TYPE_ABANDON_CRAFTING = 560;
      public const int COMMAND_TYPE_PICKUP_LOOT = 561;
      public const int COMMAND_TYPE_COMPLETE_MISSION_TASK_WITH_DIAMONDS = 562;      
      public const int COMMAND_TYPE_FULLSCREEN_TOGGLED = 563;      
      public const int COMMAND_TYPE_CHAT_MESSAGE_SENT = 564;     
      public const int COMMAND_TYPE_CANCEL_PVP_QUEUE_COMMAND = 565;     
      public const int COMMAND_TYPE_JOIN_PVP_QUEUE_COMMAND = 566; 
      
      public const int COMMAND_TYPE_DEBUG_ADD_MONEY = 1000;      
      public const int COMMAND_TYPE_DEBUG_EXP_LEVEL = 1001;      
      public const int COMMAND_TYPE_DEBUG_ITEM_PICKUP = 1002;      
      public const int COMMAND_TYPE_DEBUG_ACCEPT_MISSION = 1003;      
      public const int COMMAND_TYPE_DEBUG_WEAR_RANDOM_GEAR = 1004;      
      public const int COMMAND_TYPE_DEBUG_SELECT_LEVEL = 1005;      
      public const int COMMAND_TYPE_DEBUG_LEARN_SKILL = 1006;      
      public const int COMMAND_TYPE_DEBUG_LEARN_ALL_SKILLS = 1007;     
      public const int COMMAND_TYPE_DEBUG_KILL = 1008;     
      public const int COMMAND_TYPE_DEBUG_COMPANION = 1009;      
      public const int COMMAND_TYPE_DEBUG_CHEAT = 1011;      
      public const int COMMAND_TYPE_DEBUG_FULL_MAP = 1012;      
      public const int COMMAND_TYPE_DEBUG_COMPLETE_MISSIONS = 1013;      
      public const int COMMAND_TYPE_DEBUG_CAUSE_DAMAGE = 1014;   
      public const int COMMAND_TYPE_DEBUG_TIMELINE_EVENT = 1015;
            */
        }

        /// <summary>
        /// Loads the debug commands.
        /// </summary>
        private static void LoadDebugs()
        {
            // Factory.Debugs.Add(Factory.Delimiter + "addgems", typeof());
        }
    }
}