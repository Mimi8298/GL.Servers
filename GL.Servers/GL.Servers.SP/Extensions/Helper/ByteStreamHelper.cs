namespace GL.Servers.SP.Extensions.Helper
{
    using GL.Servers.Extensions.Binary;
    using GL.Servers.Extensions.List;
    using GL.Servers.Library.ZLib;
    using GL.Servers.SP.Files;
    using GL.Servers.SP.Files.CSV_Helpers;

    internal static class ByteStreamHelper
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

        internal static void AddData(this ByteWriter Writer, Data Data)
        {
            Writer.AddInt(Data != null ? Data.GlobalID : 0);
        }

        internal static Data ReadData(this Reader Reader)
        {
            return CSV.Tables.GetWithGlobalID(Reader.ReadInt32());
        }

        internal static T ReadData<T>(this Reader Reader) where T : Data
        {
            return CSV.Tables.GetWithGlobalID(Reader.ReadInt32()) as T;
        }
    }
}