namespace GL.Servers.CoC.Extensions.Helper
{
    using System;
    using GL.Servers.CoC.Files;
    using GL.Servers.CoC.Files.CSV_Helpers;
    using Newtonsoft.Json.Linq;

    public class JsonHelper
    {
        internal static bool GetJsonArray(JToken Token, string Key, out JArray JArray)
        {
            return (JArray = (JArray) Token[Key]) != null;
        }

        internal static bool GetJsonObject(JToken Token, string Key, out JToken JToken)
        {
            return (JToken = Token[Key]) != null;
        }

        internal static bool GetJsonString(JToken Token, string Key, out string String)
        {
            return (String = (string) Token[Key]) != null;
        }

        internal static bool GetJsonData(JToken Token, string Key, out Data Data)
        {
            return (Data = JsonHelper.GetJsonNumber(Token, Key, out int Id) ? CSV.Tables.GetWithGlobalID(Id) : null) != null;
        }

        internal static bool GetJsonData<T>(JToken Token, string Key, out T Data) where T : Data
        {
            return (Data = JsonHelper.GetJsonNumber(Token, Key, out int Id) ? CSV.Tables.GetWithGlobalID(Id) as T : null) != null;
        }

        internal static bool GetJsonBoolean(JToken Token, string Key, out bool Bool)
        {
            JToken KeyValue = Token[Key];

            if (KeyValue != null)
            {
                Bool = (bool) KeyValue;
                return true;
            }
            else
                Bool = false;

            return false;
        }

        internal static bool GetJsonNumber(JToken Token, string Key, out int Int)
        {
            JToken KeyValue = Token[Key];

            if (KeyValue != null)
            {
                Int = (int) KeyValue;
                return true;
            }
            else
                Int = 0;

            return false;
        }

        internal static bool GetJsonDateTime(JToken Token, string Key, out DateTime Time)
        {
            JToken KeyValue = Token[Key];

            if (KeyValue != null)
            {
                Time = (DateTime) KeyValue;
                return true;
            }
            else
                Time = DateTime.UtcNow;

            return false;
        }
    }
}