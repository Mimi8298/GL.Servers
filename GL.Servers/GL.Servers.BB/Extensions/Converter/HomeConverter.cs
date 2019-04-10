namespace GL.Servers.BB.Extensions.Converter
{
    using System;
    using GL.Servers.BB.Core;
    using GL.Servers.BB.Logic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    internal class HomeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Home);
        }

        public override bool CanRead
        {
            get
            {
                return true;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return true;
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Home Home = (Home) existingValue;

            if (Home != null)
            {
                Home.Load(JObject.Load(reader));
            }
            else 
                Logging.Error(this.GetType(), "Unable to deserialize Home. existingValue is null.");

            return Home;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value != null)
            {
                Home Home = (Home) value;
                Home.Save(true).WriteTo(writer);
            }
            else
                writer.WriteNull();
        }
    }
}