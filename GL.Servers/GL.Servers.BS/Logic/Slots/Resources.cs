namespace GL.Servers.BS.Logic.Slots
{
    using System.Collections.Generic;
    using System.Linq;

    using GL.Servers.BS.Files;
    using GL.Servers.BS.Files.CSV_Helpers;
    using GL.Servers.BS.Files.CSV_Logic;
    using GL.Servers.BS.Logic.Slots.Items;

    using GL.Servers.Extensions.List;

    using Newtonsoft.Json;

    internal class Resources : Dictionary<int, Resource>
    {
        [JsonProperty("player")] internal Player Player;

        /// <summary>
        /// Initializes a new instance of the <see cref="Resources"/> class.
        /// </summary>
        /// <param name="Player">The player.</param>
        internal Resources(Player Player, bool Initialize = true)
        {
            this.Player = Player;

            if (Initialize)
            {
                this.Initialize();
            }
        }

        /// <summary>
        /// Sets the specified resource value.
        /// </summary>
        /// <param name="GlobalID">The resource global id.</param>
        /// <param name="Value">The value.</param>
        internal void Set(int GlobalID, int Value)
        {
            if (this.ContainsKey(GlobalID))
            {
                this[GlobalID].Count = Value;
            }
            else
            {
                this.Add(GlobalID, new Resource(GlobalID, Value));
            }
        }

        /// <summary>
        /// Sets the specified resource value.
        /// </summary>
        /// <param name="Resource">The resource.</param>
        internal void Set(Resource Resource)
        {
            if (this.ContainsKey(Resource.GlobalID))
            {
                this[Resource.GlobalID].Count = Resource.Count;
            }
            else
            {
                this.Add(Resource.GlobalID, Resource);
            }
        }

        /// <summary>
        /// Sets the specified resource value.
        /// </summary>
        /// <param name="Resource">The resource.</param>
        /// <param name="Value">The value.</param>
        internal void Set(Enums.Resource Resource, int Value)
        {
            this.Set(GlobalID.Create(5, (int) Resource), Value);
        }

        /// <summary>
        /// Gets the specified resource value.
        /// </summary>
        /// <param name="Resource">The resource.</param>
        internal int Get(Enums.Resource Resource)
        {
            return this.Get(GlobalID.Create(5, (int) Resource));
        }

        /// <summary>
        /// Gets the specified resource value.
        /// </summary>
        /// <param name="GlobalID">The resource global id.</param>
        internal int Get(int GlobalID)
        {
            return this.ContainsKey(GlobalID) ? this[GlobalID].Count : 0;
        }

        /// <summary>
        /// Minuses the specified resource value.
        /// </summary>
        /// <param name="Resource">The resource.</param>
        /// <param name="Value">The value.</param>
        internal void Minus(Enums.Resource Resource, int Value)
        {
            int Global = GlobalID.Create(5, (int) Resource);

            if (this.ContainsKey(Global))
            {
                this[Global].Count -= Value;
            }
        }

        /// <summary>
        /// Minuses the specified resource value.
        /// </summary>
        /// <param name="GlobalID">The resource global id.</param>
        /// <param name="Value">The value.</param>
        internal void Minus(int GlobalID, int Value)
        {
            if (this.ContainsKey(GlobalID))
            {
                this[GlobalID].Count -= Value;
            }
        }

        /// <summary>
        /// Pluses the specified resource value.
        /// </summary>
        /// <param name="Resource">The resource.</param>
        /// <param name="Value">The value.</param>
        internal void Plus(Enums.Resource Resource, int Value)
        {
            int Global  = GlobalID.Create(5, (int) Resource);
            int Cap     = (CSV.Tables.Get(Enums.Gamefile.Resources).GetDataWithID(Global) as Files.CSV_Logic.Resources).Cap;

            if (this.ContainsKey(Global))
            {
                if (Cap > 0 && this[Global].Count + Value > 0)
                {
                    this[Global].Count = Cap;
                }
                else
                {
                    this[Global].Count += Value;
                }
            }
            else
            {
                this.Set(Resource, Value > Cap ? Cap : Value);
            }
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        internal void Initialize()
        {
            this.Set(Enums.Resource.Gems, (CSV.Tables.Get(Enums.Gamefile.Globals).GetData("STARTING_DIAMONDS") as Globals).NumberValue);
            this.Set(Enums.Resource.Gold, (CSV.Tables.Get(Enums.Gamefile.Globals).GetData("STARTING_GOLD") as Globals).NumberValue);
            this.Set(Enums.Resource.Elixir, 99999);
            this.Set(Enums.Resource.Upgradium, 99999);
        }

        internal byte[] ToBytes
        {
            get
            {
                List<byte> Packet = new List<byte>();

                Resource[] Resources = this.Values.ToArray();

                foreach (Resource Resource in Resources)
                {
                    Packet.AddVInt(Resource.Type);
                    Packet.AddVInt(Resource.Identifier);
                    Packet.AddVInt(Resource.Count);
                }

                return Packet.ToArray();
            }
        }
    }
}