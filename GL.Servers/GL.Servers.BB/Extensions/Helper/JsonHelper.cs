namespace GL.Servers.BB.Extensions.Helper
{
    using GL.Servers.BB.Files;
    using GL.Servers.BB.Files.CSV_Helpers;
    using Newtonsoft.Json.Linq;

    internal class JsonHelper
    {
        public static bool GetArray(JToken Token, out JArray Array)
        {
            if (Token != null)
            {
                Array = (JArray) Token;
            }
            else
                Array = null;

            return Token != null;
        }

        public static bool GetBool(JToken Token, out bool Number)
        {
            if (Token != null)
            {
                Number = (bool) Token;
            }
            else
                Number = false;

            return Token != null;
        }

        public static bool GetInt(JToken Token, out int Number, int Default = 0)
        {
            if (Token != null)
            {
                Number = (int) Token;
            }
            else
                Number = Default;

            return Token != null;
        }

        public static bool GetData(JToken Token, out Data Data)
        {
            if (Token != null)
            {
                Data = CSV.Tables.GetDataById((int) Token);
            }
            else
                Data = null;

            return Token != null;
        }

        public static bool GetData<T>(JToken Token, out T Data) where T : Data
        {
            if (Token != null)
            {
                Data = CSV.Tables.GetDataById((int) Token) as T;
            }
            else
                Data = null;

            return Token != null;
        }

        public static bool GetLong(JToken Token, out long Number)
        {
            if (Token != null)
            {
                Number = (long) Token;
            }
            else
                Number = 0;

            return Token != null;
        }

        public static bool GetString(JToken Token, out string Number)
        {
            if (Token != null)
            {
                Number = (string) Token;
            }
            else
                Number = null;

            return Token != null;
        }
    }
}