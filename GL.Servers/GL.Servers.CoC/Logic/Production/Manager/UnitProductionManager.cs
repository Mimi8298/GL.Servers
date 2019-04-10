namespace GL.Servers.CoC.Logic.Production.Manager
{
    using System.Collections.Generic;
    
    using GL.Servers.CoC.Extensions.Helper;
    using GL.Servers.CoC.Files.CSV_Logic.Logic;
    using GL.Servers.CoC.Logic.Production.Items;

    using Newtonsoft.Json.Linq;

    internal class UnitProductionManager
    {
        internal Level Level;
        internal Timer Timer;
        internal List<ProductionItem> Productions;
        
        /// <summary>
        /// Gets a value indicating the capacity of the production ingoing.
        /// </summary>
        internal int InProductionCapacity
        {
            get
            {
                int Space = 0;

                for (int i = 0; i < this.Productions.Count; i++)
                {
                    Space += ((CharacterData) this.Productions[i].Data).HousingSpace * this.Productions[i].Count;
                }

                return Space;
            }
        }

        /// <summary>
        /// Gets a value indicating whether a training is ongoing.
        /// </summary>
        internal bool ProduceUnit
        {
            get
            {
                return this.Productions.Exists(T => !T.Terminate);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitProductionManager"/> class.
        /// </summary>
        public UnitProductionManager(Level Level)
        {
            this.Level       = Level;
            this.Timer       = new Timer();
            this.Productions = new List<ProductionItem>(50);
        }

        /// <summary>
        /// Adds unit to production.
        /// </summary>
        internal void AddUnit(CharacterData Data, int Count)
        {
            if (this.CanProduce(Data, Count))
            {
                if (this.Timer.Started)
                {
                    /*
                    ProductionItem First = this.Productions.Find(T => !T.Terminate);

                    if (First.Data == Data)
                    {
                        First.Count += Count;
                        return;
                    }
                    */

                    ProductionItem Last = this.Productions.FindLast(T => T.Data == Data && !T.Terminate);

                    if (Last != null)
                    {
                        Last.Count += Count;
                        return;
                    }
                }
                else
                {
                    this.Timer.StartTimer(this.Level.Time, this.GetTrainingTime(Data));
                }
                
                this.Productions.Add(new ProductionItem(Data, Count));
            }
        }

        /// <summary>
        /// Gets a value indicating whether the character can be produced.
        /// </summary>
        internal bool CanProduce(CharacterData Character, int Count)
        {
            if (!Character.DisableProduction)
            {
                if ((Character.UnitOfType == 1 ? this.Level.ComponentManager.MaxBarrackLevel : this.Level.ComponentManager.MaxDarkBarrackLevel) >= Character.BarrackLevel - 1)
                {
                    if (this.Level.ComponentManager.TotalMaxHousing >= this.InProductionCapacity + Character.HousingSpace * Count)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Creates a fast forward of time.
        /// </summary>
        internal void FastForwardTime(int Seconds)
        {
            this.Timer.FastForward(Seconds);
        }

        /// <summary>
        /// Gets the training time of character.
        /// </summary>
        internal int GetTrainingTime(CharacterData Character)
        {
            List<Building> Barracks = (Character.UnitOfType == 1 ? this.Level.ComponentManager.Barracks : this.Level.ComponentManager.DarkBarracks).FindAll(Barrack => Barrack.GetUpgradeLevel() >= Character.BarrackLevel - 1);

            if (Barracks.Count != 0)
            {
                return Character.TrainingTime / Barracks.Count;
            }

            return Character.TrainingTime;
        }
        
        /// <summary>
        /// Loads this instance from json.
        /// </summary>
        internal void Load(JToken Json)
        {
            if (Json != null)
            {
                JArray Slots = (JArray) Json["slots"];

                if (Slots != null)
                {
                    foreach (JToken Token in Slots)
                    {
                        ProductionItem Item = new ProductionItem();
                        Item.Load(Token);
                        this.Productions.Add(Item);
                    }

                    if (this.Productions.Count > 0)
                    {
                        if (JsonHelper.GetJsonNumber(Json, "t", out int Time))
                        {
                            this.Timer.StartTimer(this.Level.Time, Time);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Saves this instance to json.
        /// </summary>
        internal JObject Save()
        {
            JObject Json = new JObject();

            if (this.Timer.Started)
            {
                Json.Add("t", this.Timer.GetRemainingSeconds(this.Level.Time));
            }

            JArray Slots = new JArray();

            foreach (ProductionItem Production in this.Productions)
            {
                Slots.Add(Production.Save());
            }

            Json.Add("slots", Slots);

            return Json;
        }

        /// <summary>
        /// Ticks this instance.
        /// </summary>
        internal void Tick()
        {
            if (this.Productions.Count > 0)
            {
                int AvailableStorage          = this.Level.ComponentManager.TotalMaxHousing - this.Level.Player.Units.GetUnitsTotalCapacity();
                bool CanAddProductionInPlayer = true;

                for (int i = 0; i < this.Productions.Count; i++)
                {
                    ProductionItem Production = this.Productions[i];
                    CharacterData Character   = (CharacterData) Production.Data;

                    if (Production.Terminate)
                    {
                        if (i == 0)
                        {
                            while (Production.Count > 0)
                            {
                                if (AvailableStorage >= Character.HousingSpace)
                                {
                                    this.Level.Player.Units.Add(Character, 1);

                                    AvailableStorage += Character.HousingSpace;

                                    Production.Count--;
                                }

                                break;
                            }

                            if (Production.Count <= 0)
                            {
                                this.Productions.RemoveAt(i--);
                            }
                        }
                    }
                    else
                    {
                        while (Production.Count > 0)
                        {
                            if (this.Timer.GetRemainingSeconds(this.Level.Time) <= 0)
                            {
                                if (AvailableStorage >= Character.HousingSpace && CanAddProductionInPlayer)
                                {
                                    this.Level.Player.Units.Add(Character, 1);

                                    AvailableStorage += Character.HousingSpace;
                                }
                                else
                                {
                                    if (i > 0)
                                    {
                                        ProductionItem Previous = this.Productions[i - 1];

                                        if (Previous.Terminate)
                                        {
                                            if (Previous.Data == Production.Data)
                                            {
                                                Previous.Count++;
                                            }
                                            else
                                            {
                                                this.Productions.Insert(i++, new ProductionItem(Production.Data, 1, true));
                                            }
                                        }
                                        else
                                        {
                                            this.Productions.Insert(i++, new ProductionItem(Production.Data, 1, true));
                                        }
                                    }
                                    else
                                    {
                                        this.Productions.Insert(i++, new ProductionItem(Production.Data, 1, true));
                                    }

                                    CanAddProductionInPlayer = false;
                                }

                                Production.Count--;

                                this.Timer.IncreaseTimer(this.GetTrainingTime(Character));
                            }
                            else
                                break;
                        }

                        if (Production.Count <= 0)
                        {
                            this.Productions.RemoveAt(i--);
                        }
                    }

                    if (this.Timer.Started && !this.ProduceUnit)
                    {
                        this.Timer.StopTimer();
                    }
                }
            }
        }
    }
}