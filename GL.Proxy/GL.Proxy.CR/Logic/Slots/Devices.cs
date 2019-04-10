namespace GL.Proxy.CR.Logic.Slots
{
    using System;
    using System.Collections.Concurrent;

    using GL.Proxy.CR.Core;

    public class Devices : ConcurrentDictionary<IntPtr, Device>
    {
        /// <summary>
        /// Initialize a new instance of the <see cref="Devices"/> class.
        /// </summary>
        public Devices()
        {
            // Devices.
        }

        /// <summary>
        /// Add the specified device to the list.
        /// </summary>
        /// <param name="Device">The device.</param>
        public void Add(Device Device)
        {
            if (this.ContainsKey(Device.Client.Handle))
            {
                Logging.Info(this.GetType(), "The device socket handle was already used at Add(Device).");
            }
            else
            {
                if (!this.TryAdd(Device.Client.Handle, Device))
                {
                    Logging.Error(this.GetType(), "Error, TryAdd(IntPtr, Device) returned false.");
                }
            }
        }

        /// <summary>
        /// Remove the specified device from the list.
        /// </summary>
        /// <param name="Device">The device.</param>
        public new void Remove(Device Device)
        {
            Device TmpDevice;

            if (!this.TryRemove(Device.Client.Handle, out TmpDevice))
            {
                Logging.Error(this.GetType(), "Error, TryRemove(IntPtr, out TmpDevice) returned false.");
            }
        }
    }
}