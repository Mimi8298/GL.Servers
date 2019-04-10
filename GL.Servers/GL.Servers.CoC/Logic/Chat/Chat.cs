namespace GL.Servers.CoC.Logic
{
    using System.Linq;
    using System.Collections.Generic;

    using GL.Servers.CoC.Core.Network;
    using GL.Servers.CoC.Packets.Messages.Server.GlobalChat;

    internal class Chat
    {
        internal List<Device> Devices;

        internal object Gate = new object();

        public Chat()
        {
            this.Devices = new List<Device>(25);
        }

        /// <summary>
        /// Try add device to the chat.
        /// </summary>
        internal bool TryAdd(Device Device)
        {
            lock (this.Gate)
            {
                if (this.Devices.Count < 25)
                {
                    this.Devices.Add(Device);

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Try remove the device of chat.
        /// </summary>
        internal void Quit(Device Device)
        {
            lock (this.Gate)
            {
                this.Devices.Remove(Device);
            }
        }

        internal void AddEntry(Device Device, string Message)
        {
            Device[] Devices = this.Devices.ToArray();

            if (Devices.Contains(Device))
            {
                if (!string.IsNullOrEmpty(Message))
                {
                    foreach (Device Device2 in Devices)
                    {
                        if (Device2.Connected)
                        {
                            new Global_Chat_Line_Message(Device2, Device.GameMode.Level.Player, Device.GameMode.Level.Home, Message).Send();
                        }
                        else
                            this.Quit(Device2);
                    }
                }
            }
        }
    }
}