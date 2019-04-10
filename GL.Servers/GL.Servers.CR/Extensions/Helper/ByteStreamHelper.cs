namespace GL.Servers.CR.Extensions.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using GL.Servers.CR.Core;
    using GL.Servers.CR.Files;
    using GL.Servers.CR.Files.CSV_Helpers;
    using GL.Servers.CR.Files.Logic;
    using GL.Servers.DataStream;
    using GL.Servers.Library.ZLib;

    public static class ByteStreamHelper
    {
        /// <summary>
        /// Adds a compressable byte array.
        /// </summary>
        internal static void AddCompressableBytes(this ChecksumEncoder Writer, byte[] Value)
        {
            if (Value != null)
            {
                int length = Value.Length;

                if (false)
                {
                    Writer.AddBoolean(true);
                    Writer.AddCompressed(Value);
                }
                else
                {
                    Writer.AddBoolean(false);
                    Writer.AddRange(Value);
                }
            }
            else
            {
                Writer.AddBoolean(false);
            }
        }

        /// <summary>
        /// Adds a compressable string.
        /// </summary>
        internal static void AddCompressableString(this ChecksumEncoder Writer, string Value)
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

        /// <summary>
        /// Adds a compressed byte array.
        /// </summary>
        internal static void AddCompressed(this ChecksumEncoder Writer, byte[] Value)
        {
            byte[] ZLib = ZlibStream.CompressBuffer(Value);

            Writer.AddRange(BitConverter.GetBytes(ZLib.Length));
            Writer.AddRange(ZLib);
        }

        /// <summary>
        /// Adds a compressed string.
        /// </summary>
        internal static void AddCompressedString(this ChecksumEncoder Writer, string Value)
        {
            Writer.AddCompressed(Encoding.UTF8.GetBytes(Value));
        }


        /// <summary>
        /// Adds a data logic long.
        /// </summary>
        internal static void AddLogicLong(this ChecksumEncoder ByteStream, int High, int Low)
        {
            ByteStream.AddVInt(High);
            ByteStream.AddVInt(Low);
        }

        /// <summary>
        /// Adds a data data reference.
        /// </summary>
        internal static void EncodeData(this ChecksumEncoder ByteStream, Data Data)
        {
            if (Data != null)
            {
                ByteStream.AddVInt(Data.Type);
                ByteStream.AddVInt(Data.Instance);
            }
            else 
                ByteStream.AddVInt(0);
        }

        /// <summary>
        /// Adds a constant size int array.
        /// </summary>
        internal static void EncodeConstantSizeIntArray(this ChecksumEncoder ByteStream, int[] Array, int Size)
        {
            for (int i = 0; i < Size; i++)
            {
                ByteStream.AddVInt(Array[i]);
            }
        }
        
        /// <summary>
        /// Adds a data data reference.
        /// </summary>
        internal static void EncodeLogicData(this ChecksumEncoder ByteStream, Data Data, int BaseDataType)
        {
            if (Data != null)
            {
                int ID = 1;

                for (int i = 0; i < Data.Type - BaseDataType; i++)
                {
                    ID += CSV.Tables.Get(BaseDataType + i).Datas.Count;
                }

                ByteStream.AddVInt(ID + Data.Instance);
            }
            else 
                ByteStream.AddVInt(0);
        }

        /// <summary>
        /// Encodes a collection of spells.
        /// </summary>
        internal static void EncodeSpellList(this ChecksumEncoder ByteStream, List<SpellData> Spells)
        {
            if (Spells.Count < 200)
            {
                ByteStream.AddVInt(Spells.Count);
                Spells.ForEach(ByteStream.EncodeData);
            }
            else 
                ByteStream.AddVInt(0);
        }

        /// <summary>
        /// Reads a data reference.
        /// </summary>
        internal static Data DecodeData(this ByteStream ByteStream)
        {
            int Type = ByteStream.ReadVInt();

            if (Type > 0)
            {
                DataTable Table = CSV.Tables.Get(Type);

                if (Table != null)
                {
                    return Table.GetWithInstanceID(ByteStream.ReadVInt());
                }
#if DEBUG
                Logging.Error(typeof(ByteStreamHelper), "ReadData() - Table " + Type + " doesn't exists.");
#endif
            }

            return null;
        }

        /// <summary>
        /// Reads a data reference.
        /// </summary>
        internal static T DecodeData<T>(this ByteStream ByteStream) where T : Data
        {
            int Type = ByteStream.ReadVInt();

            if (Type > 0)
            {
                DataTable Table = CSV.Tables.Get(Type);

                if (Table != null)
                {
                    return Table.GetWithInstanceID(ByteStream.ReadVInt()) as T;
                }
#if DEBUG
                Logging.Error(typeof(ByteStreamHelper), "ReadData() - Table " + Type + " doesn't exists.");
#endif
            }

            return null;
        }

        /// <summary>
        /// Reads a data reference.
        /// </summary>
        internal static Data DecodeLogicData(this ByteStream ByteStream, int BaseType)
        {
            int ID = ByteStream.ReadVInt();

            if (ID > 0)
            {
                while (true)
                {
                    DataTable Table = CSV.Tables.Get(BaseType++);

                    if (ID <= Table.Datas.Count)
                    {
                        return Table.GetWithInstanceID(ID - 1);
                    }
                    else
                        ID -= Table.Datas.Count;
                }
            }

            return null;
        }

        /// <summary>
        /// Reads a data reference.
        /// </summary>
        internal static T DecodeLogicData<T>(this ByteStream ByteStream, int BaseType) where T : Data
        {
            int ID = ByteStream.ReadVInt();

            if (ID > 0)
            {
                while (true)
                {
                    DataTable Table = CSV.Tables.Get(BaseType++);

                    if (ID <= Table.Datas.Count)
                    {
                        return Table.GetWithInstanceID(ID - 1) as T;
                    }
                    else
                        ID -= Table.Datas.Count;
                }
            }

            return null;
        }
    }
}