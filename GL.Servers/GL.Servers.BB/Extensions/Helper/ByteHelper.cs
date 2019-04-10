namespace GL.Servers.BB.Extensions.Helper
{
    using GL.Servers.BB.Files;
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.Extensions.Binary;

    using GL.Servers.Library.ZLib;

    internal static class ByteHelper
    {
        internal static void AddData(this ByteWriter Writer, Data Data)
        {
            Writer.AddInt(Data != null ? Data.GlobalID : 0);
        }

        internal static void AddCompressableString(this ByteWriter Writer, string Value)
        {
            if (Value != null)
            {
                int length = Value.Length;

                if (length > 0)
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

        internal static Data ReadData(this Reader Reader)
        {
            int Id = Reader.ReadInt32();

            if (Id != 0)
            {
                return CSV.Tables.GetDataById(Id);
            }

            return null;
        }

        internal static T ReadData<T>(this Reader Reader) where T : Data
        {
            return Reader.ReadData() as T;
        }
    }
}