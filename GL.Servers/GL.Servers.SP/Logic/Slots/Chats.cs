namespace GL.Servers.SP.Logic.Slots
{
    using System.Collections.Generic;
    using System.Collections.Concurrent;

    using GL.Servers.SP.Core;
    using GL.Servers.SP.Files;
    using GL.Servers.SP.Logic.Enums;
    using GL.Servers.SP.Logic;
    using GL.Servers.SP.Logic.Slots.Items;

    internal class Chats : Dictionary<int, ConcurrentStack<Chat>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Players"/> class.
        /// </summary>
        internal Chats()
        {
            /*
            foreach (LocaleData Data in CSV.Tables.Get(Gamefile.Locale).Datas)
            {
                for (int i = 0; i < Math.Max(Constants.MaxPlayers / 25, 1); i++)
                {
                    if (!this.ContainsKey(Data.GlobalID))
                    {
                        this.Add(Data.GlobalID, new ConcurrentStack<Chat>());
                    }

                    this[Data.GlobalID].Push(new Chat());
                }
            }
            */
        }
        
        internal void Join(Device Device)
        {
            /*
            if (Device.Connected)
            {
                if (Device.Chat == null && Device.GameMode.Level.Player != null)
                {
                    if (Device.GameMode.Level.Player.Locale != 0)
                    {
                        if (this.TryGetValue(Device.GameMode.Level.Player.Locale, out ConcurrentStack<Chat> Chats))
                        {
                            if (!Chats.TryPop(out Chat Chat))
                            {
                                Chat = new Chat();
                            }

                            if (Chat.TryAdd(Device))
                            {
                                Device.Chat = Chat;
                                Chats.Push(Chat);
                            }
                        }
                    }
                }
            }
            */
        }
    }
}