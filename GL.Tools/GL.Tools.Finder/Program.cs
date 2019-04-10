namespace GL.Tools.Finder
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;

    using GL.Tools.Finder.Logic;

    internal class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        private static void Main()
        {
            DirectoryInfo Folder    = Directory.CreateDirectory("Patched");
            string[] Paths          = Directory.GetFiles("Files", "*", SearchOption.TopDirectoryOnly);

            foreach (string Path in Paths)
            {
                Supercell FileClass = null;
                byte[] File         = System.IO.File.ReadAllBytes(Path);

                using (MemoryStream Stream = new MemoryStream(File))
                {
                    if (Encoding.UTF8.GetString(File, 0, File.Length).Contains("clashofclans"))
                    {
                        FileClass   = new ClashOfClans();
                    }
                    else if (Encoding.UTF8.GetString(File, 0, File.Length).Contains("boombeach"))
                    {
                        FileClass   = new BoomBeach();
                    }
                    else if (Encoding.UTF8.GetString(File, 0, File.Length).Contains("scroll"))
                    {
                        FileClass   = new ClashRoyale();
                    }

                    if (FileClass != null)
                    {
                        FileClass.Process(File);
                    }
                    else
                    {
                        Console.WriteLine("[*] This file (" + Path + ") is not supported.");
                    }
                }
            }

            Console.ReadKey(false);
        }

        /// <summary>
        /// Finds the position.
        /// </summary>
        /// <param name="_Stream">The stream.</param>
        /// <param name="Search">The pattern.</param>
        /// <returns>The offset of the specified search.</returns>
        public static long FindPosition(Stream _Stream, byte[] Search)
        {
            byte[] Buffer = new byte[Search.Length];

            using (BufferedStream Stream = new BufferedStream(_Stream, Search.Length))
            {
                int Index = 0;

                while ((Index = Stream.Read(Buffer, 0, Search.Length)) == Search.Length)
                {
                    if (Search.SequenceEqual(Buffer))
                    {
                        return Stream.Position - Search.Length;
                    }

                    Stream.Position -= Search.Length - Program.Padding(Buffer, Search);
                }
            }

            return -1;
        }

        private static int Padding(byte[] Buffer, byte[] Search)
        {
            int Index = 1;

            while (Index < Buffer.Length)
            {
                int Length = Buffer.Length - Index;

                byte[] Buffer1 = new byte[Length];
                byte[] Buffer2 = new byte[Length];

                Array.Copy(Buffer, Index, Buffer1, 0, Length);
                Array.Copy(Search, Buffer2, Length);

                if (Buffer1.SequenceEqual(Buffer2))
                {
                    return Index;
                }

                Index++;
            }

            return Index;
        }
    }
}