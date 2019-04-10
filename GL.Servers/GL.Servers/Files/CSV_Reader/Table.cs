namespace GL.Servers.Files.CSV_Reader
{
    using System.Collections.Generic;
    using Microsoft.VisualBasic.FileIO;

    public class Table
    {
        internal readonly List<Column> Columns;
        internal readonly List<string> Headers;
        internal readonly List<Row> Rows;
        internal readonly List<string> Types;

        public Table(string _Path)
        {
            this.Rows = new List<Row>();
            this.Headers = new List<string>();
            this.Types = new List<string>();
            this.Columns = new List<Column>();

            using (TextFieldParser Reader = new TextFieldParser(_Path))
            {
                Reader.SetDelimiters(",");

                string[] Columns = Reader.ReadFields();

                foreach (string Column in Columns)
                {
                    this.Headers.Add(Column);
                    this.Columns.Add(new Column());
                }

                string[] Types = Reader.ReadFields();

                foreach (string Type in Types)
                {
                    this.Types.Add(Type);
                }

                while (!Reader.EndOfData)
                {
                    string[] Values = Reader.ReadFields();

                    if (!string.IsNullOrEmpty(Values[0]))
                    {
                        this.AddRow(new Row(this));
                    }

                    for (int i = 0; i < this.Headers.Count; i++)
                    {
                        this.Columns[i].Add(Values[i]);
                    }
                }
            }
        }

        public Row GetRowAt(int _Index)
        {
            return this.Rows[_Index];
        }

        public int GetRowCount()
        {
            return this.Rows.Count;
        }

        public string GetValue(string _Name, int _Level)
        {
            int _Index = this.Headers.IndexOf(_Name);
            return this.GetValueAt(_Index, _Level);
        }

        public string GetValueAt(int _Column, int _Row)
        {
            if (_Column > -1 && _Row > -1)
            {
                return this.Columns[_Column].Get(_Row);
            }

            return null;
        }

        internal void AddRow(Row _Row)
        {
            this.Rows.Add(_Row);
        }

        internal int GetArraySizeAt(Row row, int columnIndex)
        {
            int _Index = this.Rows.IndexOf(row);
            if (_Index == -1)
            {
                return 0;
            }

            Column c = this.Columns[columnIndex];
            int _NextOffset = 0;
            if (_Index + 1 >= this.Rows.Count)
            {
                _NextOffset = c.GetSize();
            }
            else
            {
                Row _NextRow = this.Rows[_Index + 1];
                _NextOffset = _NextRow.Offset;
            }

            int _Offset = row.Offset;
            return Column.GetArraySize(_Offset, _NextOffset);
        }

        internal int GetColumnIndexByName(string _Name)
        {
            return this.Headers.IndexOf(_Name);
        }

        internal string GetColumnName(int _Index)
        {
            return this.Headers[_Index];
        }

        internal int GetColumnRowCount()
        {
            if (this.Columns.Count > 0)
            {
                return this.Columns[0].GetSize();
            }

            return 0;
        }
    }
}