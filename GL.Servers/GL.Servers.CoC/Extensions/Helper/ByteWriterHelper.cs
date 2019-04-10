namespace GL.Servers.CoC.Extensions.Helper
{
    using GL.Servers.Extensions.List;
    using GL.Servers.Library.ZLib;

    internal static class ByteWriterHelper
    {
        internal static void AddCompressableString(this ByteWriter Writer, string Value)
        {
            if (Value != null)
            {
                int length = Value.Length;

                if (length > 50)
                {
                    Writer.AddBoolean(true);
                    Writer.AddCompressedString(Value);
                }
                else
                {
                    Writer.AddBoolean(false);
                    Writer.AddString(Value);
                }
            }
            else
            {
                Writer.AddBoolean(false);
                Writer.AddInt(-1);
            }
        }

        internal static void AddCompressed(this ByteWriter Writer, byte[] Value)
        {
            byte[] Data = ZlibStream.CompressBuffer(Value);

            Writer.AddInt(Data.Length + 4);
            Writer.AddIntEndian(Value.Length);
            Writer.AddRange(Data);
        }

        internal static void AddCompressedString(this ByteWriter Writer, string Value)
        {
            byte[] Data = ZlibStream.CompressString(Value);

            Writer.AddInt(Data.Length + 4);
            Writer.AddIntEndian(Value.Length);
            Writer.AddRange(Data);
        }
    }
}