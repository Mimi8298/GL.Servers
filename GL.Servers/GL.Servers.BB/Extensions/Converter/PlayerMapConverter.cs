namespace GL.Servers.BB.Extensions.Converter
{
    using System;
    using System.Collections.Generic;
    using GL.Servers.BB.Core;
    using GL.Servers.BB.Files;
    using GL.Servers.BB.Files.CSV_Helpers;
    using GL.Servers.BB.Files.CSV_Logic;
    using GL.Servers.BB.Files.Enums;
    using GL.Servers.BB.Logic;
    using GL.Servers.BB.Logic.Map;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    internal class PlayerMapConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(PlayerMap);
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
                return false;
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            PlayerMap PlayerMap = (PlayerMap) existingValue;

            if (PlayerMap != null)
            {
                JToken Token     = JToken.Load(reader);

                JArray Regions   = (JArray) Token["MapRegions"];
                JArray Sectors   = (JArray) Token["Sectors"];

                if (Regions != null)
                {
                    List<Data> Datas = CSV.Tables.Get(Gamefile.Region).Datas;

                    for (int i = 0; i < Datas.Count && i < Regions.Count; i++)
                    {
                        RegionData RegionData = (RegionData) Datas[i];
                        JArray Nodes          = (JArray) Regions[i]["Nodes"];

                        if (Nodes != null)
                        {
                            if (Nodes.Count != 0)
                            {
                                int NodeCount = RegionData.GetNodeCount();

                                if (Nodes.Count == NodeCount)
                                {
                                    PlayerMap.MapRegions[i].CreateFromData(RegionData);

                                    for (int j = 0; j < NodeCount; j++)
                                    {
                                        using (var JsonReader = Nodes[i].CreateReader())
                                        {
                                            JsonSerializer.CreateDefault().Populate(JsonReader, PlayerMap.MapRegions[i].Nodes[j]);
                                        }
                                    }
                                }
                                else
                                {
                                    Logging.Error(this.GetType(), "ReadJson() - Regions " + i + " is corrupted! global id : " + RegionData.GlobalID + "  Nodes : " + Nodes.Count + "  Required Nodes : " + NodeCount);
                                }
                            }
                        }
                    }

                    Token["MapRegions"].Parent.Remove();
                }

                if (Sectors != null)
                {
                    List<Data> Datas = CSV.Tables.Get(Gamefile.Sector).Datas;

                    for (int i = 0; i < Sectors.Count && i < Datas.Count; i++)
                    {
                        using (var JsonReader = Sectors[i].CreateReader())
                        {
                            JsonSerializer.CreateDefault().Populate(JsonReader, PlayerMap.Sectors[i]);
                        }
                    }

                    Token["Sectors"].Parent.Remove();
                }

                using (var JsonReader = Token.CreateReader())
                {
                    JsonSerializer.CreateDefault().Populate(JsonReader, PlayerMap);
                }
            }

            return PlayerMap;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}