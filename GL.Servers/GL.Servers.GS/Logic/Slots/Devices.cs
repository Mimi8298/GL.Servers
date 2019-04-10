namespace GL.Servers.GS.Logic.Slots
{
    using System;
    using System.Collections.Generic;
    using System.Net.Sockets;

    internal class Devices : Dictionary<IntPtr, Device>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Devices"/> class.
        /// </summary>
        internal Devices()
        {
            // Devices.
        }

        /// <summary>
        /// Adds the specified device.
        /// </summary>
        /// <param name="Device">The device.</param>
        internal void Add(Device Device)
        {
            if (this.ContainsKey(Device.Socket.Handle))
            {
                this[Device.Socket.Handle] = Device;
            }
            else
            {
                this.Add(Device.Socket.Handle, Device);
            }
        }

        /// <summary>
        /// Removes the specified device.
        /// </summary>
        /// <param name="Device">The device.</param>
        internal void Remove(Device Device)
        {
            if (Device.Player != null && Device.Player.Device == Device)
            {
                Core.Resources.Players.Remove(Device.Player);
            }
            else
            {
                if (this.ContainsKey(Device.Socket.Handle))
                {
                    this.Remove(Device.Socket.Handle);
                }

                try
                {
                    Device.Socket.Shutdown(SocketShutdown.Send);
                }
                catch (Exception)
                {
                    // Already Closed.
                }

                Device.Socket.Close();
            }
        }
    }
}