namespace GL.Servers.Core.Consoles.Colorful
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class FigletFont
    {
        public static FigletFont Default
        {
            get
            {
                return FigletFont.Parse(DefaultFonts.SmallSlant);
            }
        }

        public int BaseLine { get; private set; }

        public int CodeTagCount { get; private set; }

        public int CommentLines { get; private set; }

        public int FullLayout { get; private set; }

        public string HardBlank { get; private set; }

        public int Height { get; private set; }

        public int Kerning { get; private set; }

        public string[] Lines { get; private set; }

        public int MaxLength { get; private set; }

        public int OldLayout { get; private set; }

        public int PrintDirection { get; private set; }

        public string Signature { get; private set; }

        public static FigletFont Load(byte[] bytes)
        {
            using (var stream = new MemoryStream(bytes))
            {
                return FigletFont.Load(stream);
            }
        }

        public static FigletFont Load(Stream stream)
        {
            if (stream == null) { throw new ArgumentNullException(nameof(stream)); }

            var fontLines = new List<string>();
            using (var streamReader = new StreamReader(stream))
            {
                while (!streamReader.EndOfStream)
                {
                    fontLines.Add(streamReader.ReadLine());
                }
            }

            return FigletFont.Parse(fontLines);
        }

        public static FigletFont Load(string filePath)
        {
            if (filePath == null) { throw new ArgumentNullException(nameof(filePath)); }

            return FigletFont.Parse(File.ReadLines(filePath));
        }

        public static FigletFont Parse(string fontContent)
        {
            if (fontContent == null) { throw new ArgumentNullException(nameof(fontContent)); }

            return FigletFont.Parse(fontContent.Split(new string[] { Environment.NewLine }, StringSplitOptions.None));
        }

        public static FigletFont Parse(IEnumerable<string> fontLines)
        {
            if (fontLines == null) { throw new ArgumentNullException(nameof(fontLines)); }

            var font = new FigletFont()
            {
                Lines = fontLines.ToArray()
            };
            var configString = font.Lines.First();
            var configArray = configString.Split(' ');
            font.Signature = configArray.First().Remove(configArray.First().Length - 1);
            if (font.Signature == "flf2a")
            {
                font.HardBlank = configArray.First().Last().ToString();
                font.Height = FigletFont.ParseIntValue(configArray, 1);
                font.BaseLine = FigletFont.ParseIntValue(configArray, 2);
                font.MaxLength = FigletFont.ParseIntValue(configArray, 3);
                font.OldLayout = FigletFont.ParseIntValue(configArray, 4);
                font.CommentLines = FigletFont.ParseIntValue(configArray, 5);
                font.PrintDirection = FigletFont.ParseIntValue(configArray, 6);
                font.FullLayout = FigletFont.ParseIntValue(configArray, 7);
                font.CodeTagCount = FigletFont.ParseIntValue(configArray, 8);
            }

            return font;
        }

        private static int ParseIntValue(string[] values, int index)
        {
            var integer = 0;

            if (values.Length > index)
            {
                int.TryParse(values[index], out integer);
            }

            return integer;
        }
    }
}