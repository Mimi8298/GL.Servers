namespace GL.Servers.SL.Extensions.Json
{
    using System;
    using GL.Servers.SL.Logic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    internal class TimerConverter : JsonConverter
    {
        public override bool CanConvert(Type Type)
        {
            return Type == typeof(Timer);
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
            JObject JTimer = JObject.Load(reader);

            int remainingTime = (int) (JTimer["t"] ?? -1);
            
            if (remainingTime >= 0)
            {
                Timer Timer = (Timer) existingValue;

                if (Timer.Time != null)
                {
                    Timer.StartTimer(remainingTime);
                }
                else
                {
                    Timer.TotalSeconds = remainingTime;
                    Timer.StartSubTick = 0;
                }
            }

            return existingValue;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            int Remaining = -1;

            if (value != null)
            {
                Timer Timer = (Timer) value;

                if (Timer.Started)
                {
                    Remaining = Timer.RemainingSecs;
                }
            }
            
            writer.WriteStartObject();
            writer.WritePropertyName("t");
            writer.WriteValue(Remaining);
            writer.WriteEndObject();
        }
    }
}