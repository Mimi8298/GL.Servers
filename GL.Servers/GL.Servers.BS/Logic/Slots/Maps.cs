namespace GL.Servers.BS.Logic.Slots
{
    using System.Collections.Generic;

    using GL.Servers.BS.Core;
    using GL.Servers.BS.Logic.Slots.Items;

    using GL.Servers.Extensions.List;

    using Newtonsoft.Json;

    internal class Maps
    {
        [JsonProperty("objects")] private Objects Objects;
        /*[JsonProperty("entries")]*/ internal readonly List<Map> Entries;

        /// <summary>
        /// Initializes a new instance of the <see cref="Maps"/> class.
        /// </summary>
        internal Maps()
        {
            this.Entries = new List<Map>(4);
            this.Generate();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Maps"/> class.
        /// </summary>
        /// <param name="Objects">The objects.</param>
        internal Maps(Objects Objects) : this()
        {
            this.Objects = Objects;
        }

        /// <summary>
        /// Adds the specified map.
        /// </summary>
        /// <param name="Map">The map.</param>
        internal void Add(Map Map)
        {
            if (Map != null)
            {
                if (!this.Entries.Contains(Map))
                {
                    if (this.Entries.Count < 4)
                    {
                        this.Entries.Add(Map);
                    }
                    else
                    {
                        Logging.Error(this.GetType(), "Failed to add a map, the limit of 4 maps has been reached.");
                    }
                }
                else
                {
                    Logging.Error(this.GetType(), "Failed to add a map, map was already in the list.");
                }
            }
            else
            {
                Logging.Error(this.GetType(), "Failed to add a map, the map was null.");
            }
        }

        /// <summary>
        /// Removes the specified map.
        /// </summary>
        /// <param name="Map">The map.</param>
        internal void Remove(Map Map)
        {
            if (Map != null)
            {
                if (this.Entries.Contains(Map))
                {
                    if (!this.Entries.Remove(Map))
                    {
                        Logging.Error(this.GetType(), "Failed to remove a map, Remove(Map) returned false.");
                    }
                }
                else
                {
                    Logging.Error(this.GetType(), "Failed to remove a map, map is not in the list.");
                }
            }
            else
            {
                Logging.Error(this.GetType(), "Failed to remove a map, the map was null.");
            }
        }

        /// <summary>
        /// Generates this instance.
        /// </summary>
        internal void Generate()
        {
            this.Entries.Clear();

            for (int i = 0; i < this.Entries.Capacity; i++)
            {
                this.Add(new Map(this));
            }
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        internal void Update()
        {
            foreach (Map Map in this.Entries)
            {
                Map.Update();
            }
        }

        internal byte[] ToBytes
        {
            get
            {
                List<byte> Packet = new List<byte>();
                
                Packet.AddVInt(this.Entries.Count);

                foreach (Map Map in this.Entries)
                {
                    Packet.AddRange(Map.ToBytes);
                }
                
                return Packet.ToArray();
            }
        }
    }
}