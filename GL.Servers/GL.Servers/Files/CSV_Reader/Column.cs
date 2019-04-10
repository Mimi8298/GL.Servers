namespace GL.Servers.Files.CSV_Reader
{
    using System.Collections.Generic;
    using System.Linq;

    internal class Column
    {
        internal readonly List<string> Values;

        internal Column()
        {
            this.Values = new List<string>();
        }

        internal static int GetArraySize(int _Offset, int _NOffset)
        {
            return _NOffset - _Offset;
        }

        internal void Add(string _Value)
        {
            if (_Value == null)
            {
                this.Values.Add(this.Values.Count > 0 ? this.Values.Last() : string.Empty);
            }
            else
            {
                this.Values.Add(_Value);
            }
        }

        internal string Get(int _Row)
        {
            if (this.Values.Count > _Row)
            {
                return this.Values[_Row];
            }

            return null;
        }

        internal int GetSize()
        {
            return this.Values.Count;
        }
    }
}