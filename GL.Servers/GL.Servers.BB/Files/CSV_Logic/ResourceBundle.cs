namespace GL.Servers.BB.Files.CSV_Logic
{
    using System.Collections.Generic;

    using GL.Servers.BB.Files.Enums;
    using GL.Servers.BB.Logic.Items;

    using Newtonsoft.Json;

    internal class ResourceBundle
    {
        [JsonProperty] internal List<Item> Resources;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceBundle"/> class.
        /// </summary>
        internal ResourceBundle()
        {
            this.Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceBundle"/> class.
        /// </summary>
        /// <param name="Resource">The resource cost string.</param>
        internal ResourceBundle(string Resource) : this()
        {
            this.SetResources(Resource);
        }

        /// <summary>
        /// Copies all item::count to this items.
        /// </summary>
        /// <param name="Bundle"></param>
        internal void CopyFrom(ResourceBundle Bundle)
        {
            for (int i = 0; i < 5; i++)
            {
                this.Resources[i].Count = Bundle.Resources[i].Count;
            }
        }

        /// <summary>
        /// Returns if diamonds count is superior than 0.
        /// </summary>
        /// <returns></returns>
        internal bool IsPremiumCurrency()
        {
            return this.Resources[0].Count > 0;
        }

        /// <summary>
        /// Initializes the members.
        /// </summary>
        internal void Init()
        {
            this.Resources = new List<Item>(5);

            for (int i = 0; i < 5; i++)
            {
                this.Resources.Add(new Item(CSV.Tables.Get(Gamefile.Resource).GetDataWithInstanceID(i), -1));
            }
        }

        internal void Multiply(int Multiplier)
        {
            for (int i = 1; i < 5; i++)
            {
                
            }
        }

        internal void Removes(int Index, int Count)
        {
            this.Resources[Index].Count -= Count;
        }

        /// <summary>
        /// Sets the amount of the specified resource.
        /// </summary>
        /// <param name="Index"></param>
        /// <param name="Count"></param>
        internal void SetAmount(int Index, int Count)
        {
            this.Resources[Index].Count = Count;
        }
        
        /// <summary>
        /// Sets the resource count with the resource string.
        /// </summary>
        /// <param name="Resource">The resource cost string.</param>
        internal void SetResources(string Resource)
        {
            string[] Cost = Resource.Split(',');

            if (Cost.Length > 0)
            {
                int i = 0;
                int j = 0;

                if (Cost.Length == 3)
                    i = 2;
                else if (Cost.Length == 4 || Cost.Length <= 1)
                    i = 1;

                do
                {
                    this.Resources[i++].Count = int.Parse(Cost[j++]);
                } while (i != Cost.Length);
            }
        }
    }
}
