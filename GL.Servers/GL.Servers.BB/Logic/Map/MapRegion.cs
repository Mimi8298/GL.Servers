namespace GL.Servers.BB.Logic.Map
{
    using System.Linq;
    using System.Collections.Generic;

    using GL.Servers.BB.Core;
    using GL.Servers.BB.Files.CSV_Logic;

    using GL.Servers.Extensions.List;

    using Newtonsoft.Json;

    internal class MapRegion
    {
        internal PlayerMap Map;
        internal RegionData RegionData;
        
        internal int Checksum
        {
            get
            {
                return this.Nodes.Sum(T => T.Checksum);
            }
        }

        internal bool Explored
        {
            get
            {
                return this.Nodes.Count > 0;
            }
        }

        [JsonProperty] internal List<MapNode> Nodes;

        public MapRegion()
        {
            this.Nodes = new List<MapNode>(16);
        }

        public MapRegion(PlayerMap Map) : this()
        {
            this.Map = Map;
        }

        internal void CreateFromData(RegionData Data)
        {
            this.RegionData = Data;
            int NodeCount   = this.RegionData.GetNodeCount();

            for (int i = this.Nodes.Count; i < NodeCount; i++)
            {
                this.Nodes.Add(new MapNode(this, this.RegionData.GetNodeType(i)));
            }
        }

        internal void Encode(ByteWriter Packet)
        {
            if (this.Explored)
            {
                Packet.AddBoolean(true);

                for (int i = 0; i < this.Nodes.Count; i++)
                {
                    this.Nodes[i].Encode(Packet);
                }
            }
            else 
                Packet.AddBoolean(false);
        }

        internal MapNode GetNode(int node)
        {
            if (this.Nodes.Count > node)
            {
                return this.Nodes[node];
            }

            Logging.Error(this.GetType(), "GetNode() - Index is out of range. Node " + node + " not exist.");

            return null;
        }
    }
}