namespace GL.Servers.SL.Packets
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using GL.Servers.SL.Logic;
    using GL.Servers.Extensions;
    using GL.Servers.Extensions.Binary;

    internal class Command
    {
        internal virtual int Type
        {
            get
            {
                return 0;
            }
        }

        internal int ExecuteSubTick;

        internal int ExecutorHighID;
        internal int ExecutorLowID;
        
        internal Device Device;

        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class.
        /// </summary>
        /// <param name="Device">The device.</param>
        internal Command(Device Device)
        {
            this.Device     = Device;
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal virtual void Decode(Reader Reader)
        {
            // Decode.
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal virtual void Encode(List<byte> Packet)
        {
            // Encode.
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        internal virtual void Process()
        {
            // Process.
        }

        /// <summary>
        /// Reads the header.
        /// </summary>
        internal void ReadHeader(Reader Reader)
        {
            this.ExecuteSubTick = Reader.ReadInt32();

            this.ExecutorHighID = Reader.ReadInt32();
            this.ExecutorLowID  = Reader.ReadInt32();
        }

        /// <summary>
        /// Debugs this instance.
        /// </summary>
        internal void Debug(Reader Reader)
        {
            System.Diagnostics.Debug.WriteLine(this.GetType().Name + " : " + BitConverter.ToString(Reader.ReadBytes((int) (Reader.BaseStream.Length - Reader.BaseStream.Position))));
        }

        /// <summary>
        /// Shows the values.
        /// </summary>
        internal void ShowValues()
        {
            Console.WriteLine(Environment.NewLine);

            foreach (FieldInfo Field in this.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (Field != null)
                {
                    Console.WriteLine(ConsolePad.Padding(this.GetType().Name) + " - " + ConsolePad.Padding(Field.Name) + " : " + ConsolePad.Padding(!string.IsNullOrEmpty(Field.Name) ? (Field.GetValue(this) != null ? Field.GetValue(this).ToString() : "(null)") : "(null)", 40));
                }
            }
        }
    }
}