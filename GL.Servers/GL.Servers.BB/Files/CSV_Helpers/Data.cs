namespace GL.Servers.BB.Files.CSV_Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using GL.Servers.Files.CSV_Reader;

    internal class Data
    {
        internal DataTable DataTable;
        internal Row Row;

        internal readonly int ClassID;
        internal readonly int InstanceID;
        internal readonly int GlobalID;

        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Data"/> class.
        /// </summary>
        internal Data()
        {
            // Data.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Data"/> class.
        /// </summary>
        /// <param name="Row">The row.</param>
        /// <param name="DataTable">The data table.</param>
        internal Data(Row Row, DataTable DataTable)
        {
            this.Row        = Row;
            this.DataTable  = DataTable;
            this.ClassID    = DataTable.Index;
            this.InstanceID = DataTable.Datas.Count;
            this.GlobalID   = (this.ClassID + 1) * 1000000 + this.InstanceID;
        }

        /// <summary>
        /// Loads the data.
        /// </summary>
        /// <param name="Data">The data.</param>
        /// <param name="Type">The type.</param>
        /// <param name="Row">The row.</param>
        internal static void Load(Data Data, Type Type, Row Row)
        {
            foreach (PropertyInfo Property in Type.GetProperties())
            {
                if (Property.PropertyType.IsGenericType)
                {
                    Type ListType               = typeof(List<>);
                    Type[] Generic              = Property.PropertyType.GetGenericArguments();
                    Type ConcreteType           = ListType.MakeGenericType(Generic);
                    object NewList              = Activator.CreateInstance(ConcreteType);
                    MethodInfo Add              = ConcreteType.GetMethod("Add");
                    string IndexerName          = ((DefaultMemberAttribute) NewList.GetType().GetCustomAttributes(typeof(DefaultMemberAttribute), true)[0]).MemberName;
                    PropertyInfo IndexProperty  = NewList.GetType().GetProperty(IndexerName);

                    for (int i = Row.Offset; i < Row.Offset + Row.GetArraySize(Property.Name); i++)
                    {
                        string Value = Row.GetValue(Property.Name, i - Row.Offset);

                        if (Value == string.Empty && i != Row.Offset)
                        {
                            Value = IndexProperty.GetValue(NewList, new object[]
                            {
                                i - Row.Offset - 1
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
                else
                {
                    Property.SetValue(Data, Row.GetValue(Property.Name, 0) == string.Empty ? null : Convert.ChangeType(Row.GetValue(Property.Name, 0), Property.PropertyType), null);
                }
            }
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        internal int GetID()
        {
            return this.InstanceID;
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        internal int GetDataType()
        {
            return this.ClassID;
        }
    }
}