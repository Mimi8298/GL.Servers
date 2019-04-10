namespace GL.Servers.Files.CSV_Reader
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using GL.Servers.Files.CSV_Data;

    public class Row
    {
        public readonly int RowStart;
        public readonly Table Table;

        /// <summary>
        /// Gets the name of this row.
        /// </summary>
        public string Name
        {
            get
            {
                return this.Table.GetValueAt(0, this.RowStart);
            }
        }

        /// <summary>
        /// Gets the row offset.
        /// </summary>
        public int Offset
        {
            get
            {
                return this.RowStart;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Row"/> class.
        /// </summary>
        /// <param name="Table">The table.</param>
        public Row(Table Table)
        {
            this.Table = Table;
            this.RowStart = this.Table.GetColumnRowCount();
        }

        public int GetArraySize(string Name)
        {
            int Index = this.Table.GetColumnIndexByName(Name);
            return Index != -1 ? this.Table.GetArraySizeAt(this, Index) : 0;
        }

        public string GetValue(string Name, int Level)
        {
            return this.Table.GetValue(Name, Level + this.RowStart);
        }

        public void LoadData(IData Data)
        {
            foreach (PropertyInfo Property in Data.GetType().GetProperties(BindingFlags.Instance |
                                                                           BindingFlags.NonPublic |
                                                                           BindingFlags.Public))
            {
                if (Property.CanRead && Property.CanWrite)
                {
                    if (Property.PropertyType.IsArray)
                    {
                        Type ElementType = Property.PropertyType.GetElementType();

                        if (ElementType == typeof(byte))
                        {
                            Property.SetValue(Data, this.LoadBoolArray(Property.Name));
                        }

                        else if (ElementType == typeof(int))
                        {
                            Property.SetValue(Data, this.LoadIntArray(Property.Name));
                        }

                        else if (ElementType == typeof(string))
                        {
                            Property.SetValue(Data, this.LoadStringArray(Property.Name));
                        }

                        else
                        {
                            throw new Exception(ElementType + "[] is not a valid array.");
                        }
                    }
                    else if (Property.PropertyType.IsGenericType)
                    {
                        if (Property.PropertyType == typeof(List<>))
                        {
                            Type ListType = typeof(List<>);
                            Type[] Generic = Property.PropertyType.GetGenericArguments();
                            Type ConcreteType = ListType.MakeGenericType(Generic);
                            object NewList = Activator.CreateInstance(ConcreteType);
                            MethodInfo Add = ConcreteType.GetMethod("Add");
                            string IndexerName = ((DefaultMemberAttribute) NewList.GetType().GetCustomAttributes(typeof(DefaultMemberAttribute), true)[0]).MemberName;
                            PropertyInfo IndexProperty = NewList.GetType().GetProperty(IndexerName);

                            for (int i = this.Offset; i < this.Offset + this.GetArraySize(Property.Name); i++)
                            {
                                string Value = this.GetValue(Property.Name, i - this.Offset);

                                if (Value == string.Empty && i != this.Offset)
                                {
                                    Value = IndexProperty.GetValue(NewList, new object[]
                                    {
                                        i - this.Offset - 1
                                    }).ToString();
                                }

                                if (string.IsNullOrEmpty(Value))
                                {
                                    object Object = Generic[0].IsValueType ? Activator.CreateInstance(Generic[0]) : string.Empty;

                                    Add.Invoke(NewList, new[]
                                    {
                                        Object
                                    });
                                }
                                else
                                {
                                    Add.Invoke(NewList, new[]
                                    {
                                        Convert.ChangeType(Value, Generic[0])
                                    });
                                }
                            }

                            Property.SetValue(Data, NewList);
                        }
                        else if (Property.PropertyType == typeof(IData) || Property.PropertyType.BaseType == typeof(IData))
                        {
                            IData PData = (IData) Activator.CreateInstance(Property.PropertyType);
                            this.LoadData(PData);
                            Property.SetValue(Data, PData);
                        }
                    }
                    else
                    {
                        string Value = this.GetValue(Property.Name, 0);

                        if (!string.IsNullOrEmpty(Value))
                        {
                            Property.SetValue(Data, Convert.ChangeType(Value, Property.PropertyType));
                        }
                    }
                }
            }
        }

        private bool[] LoadBoolArray(string Column)
        {
            bool[] Array = new bool[this.GetArraySize(Column)];

            for (int i = 0; i < Array.Length; i++)
            {
                string Value = this.GetValue(Column, i);

                if (!string.IsNullOrEmpty(Value))
                {
                    if (bool.TryParse(Value, out bool Boolean))
                    {
                        Array[i] = Boolean;
                    }
                    else 
                        throw new Exception("Value '" + Value + "' is not Boolean Value.");
                }
            }

            return Array;
        }

        private int[] LoadIntArray(string Column)
        {
            int[] Array = new int[this.GetArraySize(Column)];

            for (int i = 0; i < Array.Length; i++)
            {
                string Value = this.GetValue(Column, i);

                if (!string.IsNullOrEmpty(Value))
                {
                    if (int.TryParse(Value, out int Number))
                    {
                        Array[i] = Number;
                    }
                    else
                        throw new Exception("Value '" + Value + "' is not Integer Value.");
                }
            }

            return Array;
        }

        private string[] LoadStringArray(string Column)
        {
            string[] Array = new string[this.GetArraySize(Column)];

            for (int i = 0; i < Array.Length; i++)
            {
                Array[i] = this.GetValue(Column, i);
            }

            return Array;
        }
    }
}