namespace GL.Servers.SP.Logic.Slots.Items
{
    using System.Linq;
    using System.Collections.Generic;
    
    using GL.Servers.SP.Logic;

    internal class Chat
    {
        internal List<Device> Devices;

        internal object Gate = new object();

        public Chat()
        {
            this.Devices = new List<Device>(25);
        }

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
                            // new Global_Chat_Line_Message(Device2, Device.GameMode.Level.Player, Message).Send();
                        }
                        else
                            this.Quit(Device2);
                    }
                }
            }
        }
    }
}