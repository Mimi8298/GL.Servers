namespace GL.Servers.BS.Extensions
{
    using System.Collections.Generic;
    using GL.Servers.BS.Files;
    using GL.Servers.BS.Files.CSV_Helpers;
    using GL.Servers.Extensions.Binary;
    using GL.Servers.Extensions.List;

    internal static class Extension
    {
        public static void AddData(this List<byte> Packet, Data Data)
        {
            if (Data != null)
            {
                Packet.AddVInt(Data.GetDataType());
                Packet.AddVInt(Data.GetID());
            }
            else Packet.AddVInt(0);
        }

        public static Data ReadData(this Reader Reader)
        {
            int DataType = Reader.ReadVInt();

            if (DataType > 0)
            {
                return CSV.Tables.Get(DataType)?.GetDataWithInstanceID(Reader.ReadVInt());
            }

            return null;
        }

        public static T ReadData<T>(this Reader Reader) where T : Data
        {
            return Reader.ReadData() as T;
        }
    }
}