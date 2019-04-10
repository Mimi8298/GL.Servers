namespace GL.Tools.Finder.Logic
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    internal class BoomBeach : Supercell
    {
        /// <summary>
        /// Gets or sets the key offset.
        /// </summary>
        public long Offset
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BoomBeach"/> class.
        /// </summary>
        internal BoomBeach()
        {
            Console.WriteLine("[*] Boom Beach :");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BoomBeach"/> class.
        /// </summary>
        /// <param name="Offset">The offset.</param>
        internal BoomBeach(long Offset) : this()
        {
            this.Offset = Offset;
        }

        /// <summary>
        /// Processes this instance.
        /// </summary>
        public void Process(byte[] File)
        {
            if (Offset > -1)
            {
                Console.WriteLine("[*] Offset : " + Offset + " [0x" + (Offset - 32).ToString("X") + "]");
                Console.WriteLine("[*] Key    : " + BitConverter.ToString(File.ToList().GetRange((int)Offset - 32, 32).ToArray()));
                Console.WriteLine("[*] Replace ?");
                Console.Write("\b");

                if (Console.ReadKey(false).Key == ConsoleKey.Y)
                {
                    this.Patch(File);
                }
            }
            else
            {
                Console.WriteLine("[*] Offset : " + " -------------------------------");
                Console.WriteLine("[*] Key    : " + " -------------------------------");
            }
        }

        /// <summary>
        /// Patches this instance.
        /// </summary>
        public void Patch(byte[] File)
        {
            if (Console.ReadKey(false).Key == ConsoleKey.Y)
            {
                List<byte> Patched = File.ToList();

                Patched.RemoveRange((int)Offset - 32, 32);
                Patched.InsertRange((int)Offset - 32, Constants.ModdedKey);

                using (FileStream FStream = System.IO.File.Create(Directory.GetCurrentDirectory() + "\\Patched\\" + "BB-libg.so", Patched.Count, FileOptions.None))
                {
                    FStream.Write(Patched.ToArray(), 0, Patched.Count);
                }
            }
        }
    }
}