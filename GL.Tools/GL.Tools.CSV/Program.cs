namespace GL.Tools.CSV
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;

    internal class Program
    {
        internal static char[] Vowel = "aeiouAEIOU".ToCharArray();

        /// <summary>
        ///     Defines the entry point of the application.
        /// </summary>
        private static void Main()
        {
            List<string> Files = new List<string>(Directory.GetFiles("Gamefiles", "*.csv", SearchOption.AllDirectories));

            Files.Sort();

            for (int i = 1; i < 5; i++)
            {
                Mode Mode = (Mode) i;

                switch (Mode)
                {
                    case Mode.LIST:
                    {
                        Program.ProcessList(Files);
                        break;
                    }

                    case Mode.DATAS:
                    {

                        Program.ProcessDatas(Files);
                        break;
                    }

                    case Mode.ENUM:
                    {
                        Program.ProcessEnums(Files);
                        break;
                    }

                    case Mode.SWITCH:
                    {
                        Program.ProcessSwitch(Files);
                        break;
                    }

                    default:
                    {
                        break;
                    }
                }
            }
        }

        internal static void ProcessList(List<string> Files)
        {
            string Template = "CSV.Gamefiles.Add(#ID, @\"#Path\");";

            StringBuilder StringBuilder = new StringBuilder();

            for (int Index = 0; Index < Files.Count; Index++)
            {
                string File         = Files[Index];
                string FileName     = Path.GetFileName(File);
                string ClassName    = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(FileName.Replace("_", " ").Replace(".csv", ""));

                Debug.WriteLine("[*] Building string for the " + ClassName + " class.");

                StringBuilder.AppendLine(Template.Replace("#Path", "Gamefiles/csv_logic/" + FileName).Replace("#ID", (Index + 1).ToString()));
            }

            File.WriteAllText("CSV.txt", StringBuilder.ToString());
        }

        internal static void ProcessEnums(List<string> Files)
        {
            StringBuilder Stringer = new StringBuilder();

            int EnumIndex = 1;

            foreach (string File in Files)
            {
                StringBuilder CleanName     = new StringBuilder();
                FileInfo FileInfo           = new FileInfo(File);

                string Name                 = FileInfo.Name.Replace(" ", "_");
                Name                        = char.ToUpper(Name[0]) + Name.Substring(1).Replace(".csv", string.Empty);

                bool Upper = true;

                foreach (char Character in Name)
                {
                    if (Upper)
                    {
                        CleanName.Append(char.ToUpper(Character));
                        Upper = false;
                    }
                    else CleanName.Append(Character);

                    if (Character == '_')
                    {
                        Upper = true;
                    }
                }

                Name = CleanName.ToString();

                Stringer.AppendLine(Name + " = " + EnumIndex++ + ",");
            }

            File.WriteAllText("Names.txt", Stringer.ToString());
        }

        internal static void ProcessDatas(List<string> Files)
        {
            Directory.CreateDirectory("Output");

            foreach (string File in Files)
            {
                FileInfo FileInfo           = new FileInfo(File);
                
                List<char> StringName = (FileInfo.Name + "Data").Replace(".csv", string.Empty).ToList();
                string Template       = System.IO.File.ReadAllText("Template.txt");

                StringName[0] = char.ToUpper(StringName[0]);

                for (int i = 1; i < StringName.Count; i++)
                {
                    if (StringName[i] == 's')
                    {
                        if (i + 1 < StringName.Count)
                        {
                            if (char.IsUpper(StringName[i + 1]) || StringName[i + 1] == '_')
                            {
                                StringName.RemoveAt(i--);
                            }

                            if (StringName[i] == 'e')
                            {
                                if (i - 1 > 0)
                                {
                                    switch (StringName[i - 1])
                                    {
                                        case 'i':
                                        {
                                            StringName[i - 1] = 'y';
                                            StringName.RemoveAt(i--);

                                            break;
                                        }

                                        case 'o':
                                        {
                                            StringName.RemoveAt(i--);

                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            StringName.RemoveAt(i--);
                        }
                    }
                    else if (i <= StringName.Count && StringName[i] == '_')
                    {
                        StringName[i + 1] = char.ToUpper(StringName[i + 1]);
                        StringName.RemoveAt(i);
                    }
                }

                string Name         = string.Join(string.Empty, StringName);
                
                string[] Lines      = System.IO.File.ReadAllLines(File);

                string[] Columns    = Lines[0].Replace("\"", string.Empty).Split(',');
                string[] Types      = Lines[1].Replace("\"", string.Empty).Split(',');

                Template            = Template.Replace("#Name#", Name);

                StringBuilder Properties = new StringBuilder();

                for (int i = 0; i < Columns.Length; i++)
                {
                    if (Columns[i] == "Name")
                    {
                        continue;
                    }

                    Properties.AppendLine();

                    string Type = Types[i].ToLower();

                    if (Type == "boolean")
                    {
                        Type = "bool";
                    }

                    if (Columns[i].ToLower().Contains("array"))
                    {
                        Type = Type + "[]";
                    }

                    Properties.AppendLine("        internal " + Type + " " + Columns[i].Replace(" ", string.Empty));
                    Properties.AppendLine("        {");
                    Properties.AppendLine("            get; set;");
                    Properties.AppendLine("        }");
                }
                
                Template = Template.Replace("#Property#", Properties.ToString());

                System.IO.File.WriteAllText("Output/" + Name + ".cs", Template);
            }
        }

        internal static void ProcessSwitch(List<string> Files)
        {
            StringBuilder Case = new StringBuilder();

            int CaseIndex = 1;

            foreach (string File in Files)
            {
                StringBuilder CleanName = new StringBuilder();
                FileInfo FileInfo       = new FileInfo(File);

                string Name             = FileInfo.Name.Replace(" ", "_");
                Name                    = char.ToUpper(Name[0]) + Name.Substring(1).Replace(".csv", string.Empty);

                bool Upper = true;

                foreach (char Character in Name)
                {
                    if (Upper)
                    {
                        CleanName.Append(char.ToUpper(Character));
                        Upper = false;
                    }
                    else CleanName.Append(Character);

                    if (Character == '_')
                        Upper = true;
                }

                Name = CleanName.ToString();

                Case.AppendLine("    case " + CaseIndex++ + ":");
                Case.AppendLine("    {");
                Case.AppendLine("        Data = new " + Name + "(Row, this);");
                Case.AppendLine("        break;");
                Case.AppendLine("    }");
                Case.AppendLine();
            }

            File.WriteAllText("Cases.txt", Case.ToString());
        }
    }
}