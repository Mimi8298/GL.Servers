namespace GL.Servers.CR.Logic
{
    using System;
    using System.Collections.Generic;

    using GL.Servers.CR.Core;
    using GL.Servers.CR.Extensions.Game;
    using GL.Servers.CR.Extensions.Helper;
    using GL.Servers.CR.Files;
    using GL.Servers.CR.Files.CSV_Helpers;
    using GL.Servers.CR.Files.Logic;
    using GL.Servers.CR.Logic.Chests;
    using GL.Servers.CR.Logic.Enums;
    using GL.Servers.CR.Logic.Mode;
    using GL.Servers.CR.Logic.Spells;

    using GL.Servers.DataStream;
    using GL.Servers.Extensions.List;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    [JsonConverter(typeof(HomeConverter))]
    internal class Home
    {
        internal Time Time;
        internal GameMode GameMode;

        internal int HighID;
        internal int LowID;

        internal Random Random;
        internal ShopCycle ShopCycle;
        internal SpellDeck SpellDeck;
        internal SpellCollection SpellCollection;

        internal Timer FreeChestTimer;
        internal Timer CrownChestTimer;
        internal Timer NextShopCycleTimer;

        internal int[][] DeckPresets;

        internal int PageOpened;
        internal int SelectedDeck;
        internal int TutorialStep;
        internal int ChestSlotCount;
        internal int CollectedFreeChest;
        internal int DonationCapacityLimit;
        internal int LastShownLevelUp = 1;

        internal Data LastShownArena;

        internal DateTime LastTick;

        /// <summary>
        /// Gets the checksum of this instance.
        /// </summary>
        internal int Checksum
        {
            get
            {
                return this.Random.Seed + (this.SpellCollection.Count << 16) + 1;
            }
        }

        /// <summary>
        /// Gets total seconds since last save.
        /// </summary>
        internal int SecondsSinceLastSave
        {
            get
            {
                return Math.Max((int) DateTime.UtcNow.Subtract(this.LastTick).TotalSeconds, 0);
            }
        }

        /// <summary>
        /// Gets the number of spells.
        /// </summary>
        internal int SpellCount
        {
            get
            {
                return this.SpellDeck.Count + this.SpellCollection.Count;
            }
        }

        internal bool HasAllCardsFull
        {
            get
            {
                for (int i = this.SpellCount - 1; i >= 0; i--)
                {
                    // TODO Implement ClientHome::hasAllCardsFull().
                }

                return true;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Home"/> class.
        /// </summary>
        internal Home()
        {
            // Game

            this.Random          = new Random(Resources.Random.Next());
            this.ShopCycle       = new ShopCycle();
            this.SpellDeck       = new SpellDeck();
            this.SpellCollection = new SpellCollection();

            this.DeckPresets     = new int[5][];
            this.DeckPresets[0]  = new int[8];
            this.DeckPresets[1]  = new int[8];
            this.DeckPresets[2]  = new int[8];
            this.DeckPresets[3]  = new int[8];
            this.DeckPresets[4]  = new int[8];
            
            // Game Timers.

            this.FreeChestTimer = new Timer();
            this.NextShopCycleTimer = new Timer();
            this.NextShopCycleTimer.StartTimer(new Time(), 86400);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Home"/> class.
        /// </summary>
        internal Home(int HighID, int LowID) : this()
        {
            this.HighID = HighID;
            this.LowID = LowID;
        }

        /// <summary>
        /// Adds the specified spell.
        /// </summary>
        internal void AddSpell(Spell Spell)
        {
            if (!this.HasSpell(Spell.Data))
            {
                if (this.SpellDeck.SpellCount != 8)
                {
                    this.SpellDeck.Add(Spell);
                }
                else 
                    this.SpellCollection.AddSpell(Spell);

                return;
            }

            Logging.Error(this.GetType(), "Trying to add spell that already exists in collection, data:" + Spell.Data.GlobalID);
        }

        /// <summary>
        /// Gets the spell at specified index.
        /// </summary>
        internal Spell GetSpellAt(int Index)
        {
            if (this.SpellDeck.Count > Index)
            {
                return this.SpellDeck[Index];
            }

            if (this.SpellCollection.Count > Index - this.SpellDeck.Count)
            {
                return this.SpellCollection[Index];
            }

            Logging.Error(this.GetType(), "GetSpellAt() - Index out of bounds.");

            return null;
        }

        /// <summary>
        /// Gets spell by data.
        /// </summary>
        internal Spell GetSpellByData(SpellData Data)
        {
            Spell Spell = this.SpellDeck.GetSpellByData(Data);

            if (Spell == null)
            {
                Spell = this.SpellCollection.GetSpellByData(Data);
            }

            return Spell;
        }

        /// <summary>
        /// Gets if the player have the specified spell.
        /// </summary>
        internal bool HasSpell(SpellData Data)
        {
            return this.GetSpellByData(Data) != null;
        }

        /// <summary>
        /// Called after encode for refresh this instance.
        /// </summary>
        internal void LoadingFinished()
        {
            this.SaveCurrentDeckTo(this.SelectedDeck);
            this.UpdateChestCountToPlayer();
        }

        /// <summary>
        /// Sets the last shown level up.
        /// </summary>
        internal void SetLastShownLevelUp(int ExpLevel)
        {
            this.LastShownLevelUp = ExpLevel;
        }

        /// <summary>
        /// Sets the opened page.
        /// </summary>
        internal void SetPageOpened(int Page)
        {
            this.PageOpened |= 1 << Page;
        }

        /// <summary>
        /// Sets the tutorial to finish step.
        /// </summary>
        internal void SetTutorialFinished(TutorialData Data)
        {
            // TODO Implement LogicClientHome::setTutorialFinished().
        }

        /// <summary>
        /// Updates the number of chest to player.
        /// </summary>
        internal void UpdateArenaFromPlayer()
        {
            Player Player = this.GameMode.Player;

            if (Player != null)
            {
                this.LastShownArena = Player.Arena;
            }
        }

        /// <summary>
        /// Updates the number of chest to player.
        /// </summary>
        internal void UpdateChestCountToPlayer()
        {
            Player Player = this.GameMode.Player;

            if (Player != null)
            {
                Player.SetChestCount(0);
            }
        }

        /// <summary>
        /// Saves the current deco to the specified preset.
        /// </summary>
        internal void SaveCurrentDeckTo(int Index)
        {
            if (Index >= 5)
            {
                Logging.Error(this.GetType(), "SaveCurrentDeckTo() - deckIdx out of range.");
                return;
            }

            for (int i = 0; i < 8; i++)
            {
                if (this.SpellDeck[i] != null)
                {
                    this.DeckPresets[Index][i] = this.SpellDeck[i].Data.GlobalID;
                }
                else
                    this.DeckPresets[Index][i] = 0;
            }
        }

        /// <summary>
        /// Sets the selected deck.
        /// </summary>
        internal void SetSelectedDeck(int Index)
        {
            if (Globals.MultipleDecks)
            {
                if (Index >= 5)
                {
                    Logging.Error(this.GetType(), "SetSelectedDeck() - deckIdx out of range.");
                    return;
                }

                if (this.SelectedDeck != Index)
                {
                    if (Array.TrueForAll(this.DeckPresets[Index], Id => Id == 0))
                    {
                        this.SaveCurrentDeckTo(Index);
                        return;
                    }

                    for (int i = 0; i < 8; i++)
                    {
                        SpellData Data = CSV.Tables.Spells.Find(T => T.GlobalID == this.DeckPresets[Index][i]);

                        if (Data != null)
                        {
                            int SpellIdx = this.SpellDeck.GetSpellIdxByData(Data);

                            if (SpellIdx == -1)
                            {
                                SpellIdx = this.SpellCollection.GetSpellIdxByData(Data);

                                if (SpellIdx == -1)
                                {
                                    if (this.SpellCollection.Count < 1)
                                    {
                                        Spell Spell = this.SpellDeck[i];

                                        if (Spell != null)
                                        {
                                            this.DeckPresets[Index][i] = Spell.Data.GlobalID;
                                        }
                                        else
                                            this.DeckPresets[Index][i] = -1;

                                        continue;
                                    }

                                    this.DeckPresets[Index][i] = this.SpellCollection[0].Data.GlobalID;
                                }

                                Spell InDeck = this.SpellDeck[i];
                                Spell InCollection = this.SpellCollection[SpellIdx];

                                if (InDeck != null)
                                {
                                    this.SpellCollection.SetSpell(SpellIdx, InDeck);
                                }
                                else 
                                    this.SpellCollection.RemoveSpell(SpellIdx);

                                this.SpellDeck.SetSpell(i, InCollection);
                            }
                            else if (i != SpellIdx)
                            {
                                this.SpellDeck.SwapSpells(SpellIdx, i);
                            }
                        }
                    }
                }

                this.SelectedDeck = Index;
            }
        }

        /// <summary>
        /// Creates a fast forward.
        /// </summary>
        internal void FastForward(int Seconds)
        {
            // FastForward.
        }

        /// <summary>
        /// Ticks this instance.
        /// </summary>
        internal void Tick()
        {
            this.Time = this.GameMode.Time;
            this.LastTick = DateTime.UtcNow;

            if (this.NextShopCycleTimer.IsFinished(this.Time))
            {
                // TODO Implement ReGen Shop
                this.NextShopCycleTimer.StartTimer(this.Time, 86400);
            }

            this.UpdateArenaFromPlayer();
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal void Encode(ByteStream Packet)
        {
            Packet.AddLong(this.HighID, this.LowID);

            Packet.AddVInt(this.Random.Seed);
            Packet.AddVInt(this.CollectedFreeChest);

            this.FreeChestTimer.Encode(Packet);

            Packet.AddVInt(this.DonationCapacityLimit);

            Packet.AddVInt(this.DeckPresets.Length);

            for (int i = 0; i < this.DeckPresets.Length; i++)
            {
                Packet.AddVInt(this.DeckPresets[i].Length);

                for (int j = 0; j < this.DeckPresets[i].Length; j++)
                {
                    Packet.AddVInt(this.DeckPresets[i][j]);
                }
            }

            this.SpellDeck.Encode(Packet);
            this.SpellCollection.Encode(Packet);

            Packet.AddVInt(this.SelectedDeck);

            new SpellDeck().Encode(Packet);

            Packet.AddRange("00-7F-7F-7F-98-A5-93-9E-0B-01-00-0B-83-09-00-00-00-19-44-65-6D-6F-20-41-63-63-6F-75-6E-74-20-32-76-32-20-46-72-69-65-6E-64-6C-79-05-A8-C0-D3-94-0B-A8-86-C7-A4-0B-A8-C0-D3-94-0B-00-00-00-00-00-00-00-00-00-00-00-19-44-65-6D-6F-20-41-63-63-6F-75-6E-74-20-32-76-32-20-46-72-69-65-6E-64-6C-79-00-00-00-4D-7B-22-54-61-72-67-65-74-5F-41-63-63-6F-75-6E-74-54-79-70-65-22-3A-22-44-65-6D-6F-41-63-63-6F-75-6E-74-22-2C-22-48-69-64-65-54-69-6D-65-72-22-3A-74-72-75-65-2C-22-47-61-6D-65-4D-6F-64-65-22-3A-22-54-65-61-6D-56-73-54-65-61-6D-22-7D-84-09-00-00-00-1F-44-65-6D-6F-20-41-63-63-6F-75-6E-74-20-32-76-32-20-44-72-61-66-74-20-46-72-69-65-6E-64-6C-79-05-A8-C0-D3-94-0B-A8-86-C7-A4-0B-A8-C0-D3-94-0B-00-00-00-00-00-00-00-00-00-00-00-1F-44-65-6D-6F-20-41-63-63-6F-75-6E-74-20-32-76-32-20-44-72-61-66-74-20-46-72-69-65-6E-64-6C-79-00-00-00-5B-7B-22-47-61-6D-65-4D-6F-64-65-22-3A-22-54-65-61-6D-56-73-54-65-61-6D-44-72-61-66-74-43-68-61-6C-6C-65-6E-67-65-22-2C-22-48-69-64-65-54-69-6D-65-72-22-3A-74-72-75-65-2C-22-54-61-72-67-65-74-5F-41-63-63-6F-75-6E-74-54-79-70-65-22-3A-22-44-65-6D-6F-41-63-63-6F-75-6E-74-22-7D-85-09-00-00-00-1F-44-65-6D-6F-20-41-63-63-6F-75-6E-74-20-31-76-31-20-44-72-61-66-74-20-46-72-69-65-6E-64-6C-79-05-A8-C0-D3-94-0B-A8-86-C7-A4-0B-A8-C0-D3-94-0B-00-00-00-00-00-00-00-00-00-00-00-1F-44-65-6D-6F-20-41-63-63-6F-75-6E-74-20-31-76-31-20-44-72-61-66-74-20-46-72-69-65-6E-64-6C-79-00-00-00-4C-7B-22-48-69-64-65-54-69-6D-65-72-22-3A-74-72-75-65-2C-22-54-61-72-67-65-74-5F-41-63-63-6F-75-6E-74-54-79-70-65-22-3A-22-44-65-6D-6F-41-63-63-6F-75-6E-74-22-2C-22-47-61-6D-65-4D-6F-64-65-22-3A-22-44-72-61-66-74-4D-6F-64-65-22-7D-95-11-00-00-00-0A-32-76-32-20-42-75-74-74-6F-6E-08-B0-93-D4-99-0B-B0-E1-DD-B7-0B-B0-A9-8A-99-0B-00-00-00-00-00-00-00-00-00-00-00-0A-32-76-32-20-42-75-74-74-6F-6E-00-00-00-75-7B-22-48-69-64-65-54-69-6D-65-72-22-3A-74-72-75-65-2C-22-48-69-64-65-50-6F-70-75-70-54-69-6D-65-72-22-3A-74-72-75-65-2C-22-45-78-74-72-61-47-61-6D-65-4D-6F-64-65-43-68-61-6E-63-65-22-3A-30-2C-22-47-61-6D-65-4D-6F-64-65-22-3A-22-54-65-61-6D-56-73-54-65-61-6D-4C-61-64-64-65-72-22-2C-22-45-78-74-72-61-47-61-6D-65-4D-6F-64-65-22-3A-22-4E-6F-6E-65-22-7D-AF-12-00-00-00-2A-32-76-32-20-44-6F-75-62-6C-65-20-45-6C-69-78-69-72-20-44-72-61-66-74-20-43-68-61-6C-6C-65-6E-67-65-20-46-72-69-65-6E-64-6C-79-05-B0-E1-AE-9D-0B-B0-F9-D8-9D-0B-B0-E1-AE-9D-0B-00-00-00-00-00-00-00-00-00-00-00-2A-32-76-32-20-44-6F-75-62-6C-65-20-45-6C-69-78-69-72-20-44-72-61-66-74-20-43-68-61-6C-6C-65-6E-67-65-20-46-72-69-65-6E-64-6C-79-00-00-00-49-7B-22-47-61-6D-65-4D-6F-64-65-22-3A-22-54-65-61-6D-56-73-54-65-61-6D-5F-44-72-61-66-74-4D-6F-64-65-49-6E-73-61-6E-65-5F-46-72-69-65-6E-64-6C-79-22-2C-22-44-72-61-66-74-44-65-63-6B-22-3A-22-44-72-61-66-74-5F-76-31-22-7D-B0-12-00-00-00-17-45-76-65-6E-74-20-50-61-67-65-20-48-65-61-64-65-72-20-49-6D-61-67-65-0D-B0-F9-D8-9D-0B-B0-E3-A2-9E-0B-B0-F9-D8-9D-0B-00-00-00-00-00-00-00-00-00-00-00-17-45-76-65-6E-74-20-50-61-67-65-20-48-65-61-64-65-72-20-49-6D-61-67-65-00-00-00-ED-7B-22-54-69-74-6C-65-22-3A-22-53-65-6D-61-69-6E-65-20-74-6F-75-63-68-64-6F-77-6E-22-2C-22-49-63-6F-6E-22-3A-22-69-63-6F-6E-5F-74-6F-75-72-6E-61-6D-65-6E-74-5F-74-6F-75-63-68-64-6F-77-6E-22-2C-22-54-69-74-6C-65-47-6C-6F-77-22-3A-74-72-75-65-2C-22-49-6D-61-67-65-22-3A-7B-22-50-61-74-68-22-3A-22-2F-30-36-32-32-65-32-61-61-62-38-65-31-34-30-37-33-61-61-61-38-64-36-31-30-64-64-61-62-34-37-37-32-5F-74-6F-75-63-68-64-6F-77-6E-5F-68-65-61-64-65-72-5F-30-31-2E-70-6E-67-22-2C-22-43-68-65-63-6B-73-75-6D-22-3A-22-30-36-32-32-65-32-61-61-62-38-65-31-34-30-37-33-61-61-61-38-64-36-31-30-64-64-61-62-34-37-37-32-22-2C-22-46-69-6C-65-22-3A-22-74-6F-75-63-68-64-6F-77-6E-5F-68-65-61-64-65-72-5F-30-31-2E-70-6E-67-22-7D-7D-B1-12-00-00-00-17-32-76-32-20-54-6F-75-63-68-20-44-6F-77-6E-20-50-72-61-63-74-69-63-65-02-B0-F9-D8-9D-0B-B0-E3-A2-9E-0B-B0-F9-D8-9D-0B-00-00-00-00-00-00-00-00-00-00-00-17-32-76-32-20-54-6F-75-63-68-20-44-6F-77-6E-20-50-72-61-63-74-69-63-65-00-00-04-D9-7B-22-43-61-73-75-61-6C-22-3A-74-72-75-65-2C-22-47-61-6D-65-4D-6F-64-65-22-3A-22-54-65-61-6D-56-73-54-65-61-6D-5F-54-6F-75-63-68-64-6F-77-6E-5F-44-72-61-66-74-22-2C-22-54-69-74-6C-65-22-3A-22-54-6F-75-63-68-64-6F-77-6E-20-71-75-6F-74-69-64-69-65-6E-20-32-63-32-22-2C-22-46-72-65-65-50-61-73-73-22-3A-31-2C-22-49-73-43-68-61-69-6E-65-64-45-76-65-6E-74-22-3A-66-61-6C-73-65-2C-22-49-73-44-61-69-6C-79-52-65-66-72-65-73-68-22-3A-74-72-75-65-2C-22-43-61-73-75-61-6C-5F-43-72-6F-77-6E-73-49-6E-73-74-65-61-64-4F-66-57-69-6E-73-22-3A-74-72-75-65-2C-22-52-65-77-61-72-64-73-22-3A-5B-7B-7D-2C-7B-7D-2C-7B-7D-2C-7B-22-4D-69-6C-65-73-74-6F-6E-65-22-3A-7B-22-54-79-70-65-22-3A-22-47-6F-6C-64-22-2C-22-41-6D-6F-75-6E-74-22-3A-33-30-30-7D-7D-2C-7B-7D-2C-7B-7D-2C-7B-22-4D-69-6C-65-73-74-6F-6E-65-22-3A-7B-22-43-68-65-73-74-22-3A-22-47-6F-6C-64-5F-3C-63-75-72-72-65-6E-74-5F-61-72-65-6E-61-3E-22-2C-22-54-79-70-65-22-3A-22-43-68-65-73-74-22-7D-7D-2C-7B-7D-2C-7B-7D-2C-7B-7D-2C-7B-22-4D-69-6C-65-73-74-6F-6E-65-22-3A-7B-22-49-73-48-69-67-68-6C-69-67-68-74-65-64-22-3A-74-72-75-65-2C-22-54-79-70-65-22-3A-22-47-65-6D-73-22-2C-22-41-6D-6F-75-6E-74-22-3A-35-7D-7D-5D-2C-22-49-63-6F-6E-45-78-70-6F-72-74-4E-61-6D-65-22-3A-22-69-63-6F-6E-5F-74-6F-75-72-6E-61-6D-65-6E-74-5F-74-6F-75-63-68-64-6F-77-6E-22-2C-22-57-69-6E-49-63-6F-6E-45-78-70-6F-72-74-4E-61-6D-65-22-3A-22-74-6F-75-72-6E-61-6D-65-6E-74-5F-6F-70-65-6E-5F-77-69-6E-73-5F-62-61-64-67-65-5F-62-72-6F-6E-7A-65-22-2C-22-41-72-65-6E-61-22-3A-22-41-72-65-6E-61-5F-54-6F-75-63-68-64-6F-77-6E-54-65-73-74-22-2C-22-53-75-62-74-69-74-6C-65-22-3A-22-45-6E-74-72-61-C3-AE-6E-65-6D-65-6E-74-20-61-76-65-63-20-72-C3-A9-63-6F-6D-70-65-6E-73-65-73-22-2C-22-44-65-73-63-72-69-70-74-69-6F-6E-22-3A-22-47-61-67-6E-65-7A-20-75-6E-65-20-63-6F-75-72-6F-6E-6E-65-20-65-6E-20-61-6D-65-6E-61-6E-74-20-75-6E-65-20-75-6E-69-74-C3-A9-20-C3-A0-20-6C-27-65-6E-2D-62-75-74-20-61-64-76-65-72-73-65-2E-20-52-65-6D-70-6F-72-74-65-7A-20-6C-65-20-63-6F-6D-62-61-74-20-61-76-65-63-20-33-20-63-6F-75-72-6F-6E-6E-65-73-2E-20-52-C3-A9-63-6F-6D-70-65-6E-73-65-73-20-71-75-6F-74-69-64-69-65-6E-6E-65-73-C2-A0-21-22-2C-22-42-61-63-6B-67-72-6F-75-6E-64-22-3A-7B-22-50-61-74-68-22-3A-22-2F-33-33-39-62-64-66-33-37-32-34-30-32-39-38-38-64-38-61-38-36-34-62-65-32-37-63-32-30-30-38-61-36-5F-74-6F-75-63-68-64-6F-77-6E-5F-63-68-61-6C-6C-65-6E-67-65-5F-30-31-5F-36-36-36-36-5F-61-6C-70-68-61-5F-70-72-65-6D-75-6C-2E-70-6E-67-22-2C-22-43-68-65-63-6B-73-75-6D-22-3A-22-33-33-39-62-64-66-33-37-32-34-30-32-39-38-38-64-38-61-38-36-34-62-65-32-37-63-32-30-30-38-61-36-22-2C-22-46-69-6C-65-22-3A-22-74-6F-75-63-68-64-6F-77-6E-5F-63-68-61-6C-6C-65-6E-67-65-5F-30-31-5F-36-36-36-36-5F-61-6C-70-68-61-5F-70-72-65-6D-75-6C-2E-70-6E-67-22-7D-2C-22-42-61-63-6B-67-72-6F-75-6E-64-5F-43-6F-6D-70-6C-65-74-65-22-3A-7B-22-50-61-74-68-22-3A-22-2F-33-33-39-62-64-66-33-37-32-34-30-32-39-38-38-64-38-61-38-36-34-62-65-32-37-63-32-30-30-38-61-36-5F-74-6F-75-63-68-64-6F-77-6E-5F-63-68-61-6C-6C-65-6E-67-65-5F-30-31-5F-36-36-36-36-5F-61-6C-70-68-61-5F-70-72-65-6D-75-6C-2E-70-6E-67-22-2C-22-43-68-65-63-6B-73-75-6D-22-3A-22-33-33-39-62-64-66-33-37-32-34-30-32-39-38-38-64-38-61-38-36-34-62-65-32-37-63-32-30-30-38-61-36-22-2C-22-46-69-6C-65-22-3A-22-74-6F-75-63-68-64-6F-77-6E-5F-63-68-61-6C-6C-65-6E-67-65-5F-30-31-5F-36-36-36-36-5F-61-6C-70-68-61-5F-70-72-65-6D-75-6C-2E-70-6E-67-22-7D-2C-22-44-72-61-66-74-44-65-63-6B-22-3A-22-44-72-61-66-74-5F-54-6F-75-63-68-64-6F-77-6E-5F-76-31-22-2C-22-55-6E-6C-6F-63-6B-65-64-46-6F-72-58-50-22-3A-22-45-76-65-72-79-6F-6E-65-22-2C-22-45-6E-64-4E-6F-74-69-66-69-63-61-74-69-6F-6E-22-3A-22-46-69-6E-20-64-75-20-64-C3-A9-66-69-20-74-6F-75-63-68-64-6F-77-6E-20-32-63-32-20-64-61-6E-73-20-32-C2-A0-68-C2-A0-21-22-7D-B2-12-00-00-00-16-46-72-69-65-6E-64-6C-79-20-32-76-32-20-54-6F-75-63-68-64-6F-77-6E-05-B0-F9-D8-9D-0B-B0-C3-CB-9F-0B-B0-F9-D8-9D-0B-00-00-00-00-00-00-00-00-00-00-00-16-46-72-69-65-6E-64-6C-79-20-32-76-32-20-54-6F-75-63-68-64-6F-77-6E-00-00-01-86-7B-22-47-61-6D-65-4D-6F-64-65-22-3A-22-54-65-61-6D-56-73-54-65-61-6D-5F-54-6F-75-63-68-64-6F-77-6E-5F-44-72-61-66-74-22-2C-22-46-69-78-65-64-41-72-65-6E-61-22-3A-22-41-72-65-6E-61-5F-54-6F-75-63-68-64-6F-77-6E-54-65-73-74-22-2C-22-54-69-74-6C-65-22-3A-22-54-6F-75-63-68-64-6F-77-6E-20-61-6D-69-63-61-6C-20-32-63-32-22-2C-22-53-75-62-74-69-74-6C-65-22-3A-22-4A-6F-75-65-7A-20-61-76-65-63-20-6C-65-73-20-6D-65-6D-62-72-65-73-20-64-65-20-76-6F-74-72-65-20-63-6C-61-6E-C2-A0-21-22-2C-22-44-72-61-66-74-44-65-63-6B-22-3A-22-44-72-61-66-74-5F-54-6F-75-63-68-64-6F-77-6E-5F-76-31-22-2C-22-42-61-63-6B-67-72-6F-75-6E-64-22-3A-7B-22-50-61-74-68-22-3A-22-2F-37-38-63-64-66-62-32-31-32-32-33-37-61-63-65-61-66-33-34-63-39-62-33-65-66-61-37-39-66-33-66-36-5F-66-72-69-65-6E-64-5F-74-6F-75-63-68-64-6F-77-6E-5F-30-31-5F-36-36-36-36-5F-61-6C-70-68-61-5F-70-72-65-6D-75-6C-2E-70-6E-67-22-2C-22-43-68-65-63-6B-73-75-6D-22-3A-22-37-38-63-64-66-62-32-31-32-32-33-37-61-63-65-61-66-33-34-63-39-62-33-65-66-61-37-39-66-33-66-36-22-2C-22-46-69-6C-65-22-3A-22-66-72-69-65-6E-64-5F-74-6F-75-63-68-64-6F-77-6E-5F-30-31-5F-36-36-36-36-5F-61-6C-70-68-61-5F-70-72-65-6D-75-6C-2E-70-6E-67-22-7D-7D-B3-12-00-00-00-18-32-76-32-20-54-6F-75-63-68-20-44-6F-77-6E-20-43-68-61-6C-6C-65-6E-67-65-02-B0-BF-E3-9D-0B-B0-91-83-9E-0B-B0-F9-D8-9D-0B-00-00-00-00-00-00-00-00-00-00-00-18-32-76-32-20-54-6F-75-63-68-20-44-6F-77-6E-20-43-68-61-6C-6C-65-6E-67-65-00-00-05-B0-7B-22-47-61-6D-65-4D-6F-64-65-22-3A-22-54-65-61-6D-56-73-54-65-61-6D-5F-54-6F-75-63-68-64-6F-77-6E-5F-44-72-61-66-74-22-2C-22-46-72-65-65-50-61-73-73-22-3A-30-2C-22-4A-6F-69-6E-43-6F-73-74-22-3A-31-30-2C-22-4A-6F-69-6E-43-6F-73-74-52-65-73-6F-75-72-63-65-22-3A-22-47-65-6D-73-22-2C-22-4D-61-78-4C-6F-73-73-65-73-22-3A-33-2C-22-52-65-77-61-72-64-73-22-3A-5B-7B-22-47-6F-6C-64-22-3A-31-33-30-2C-22-43-61-72-64-73-22-3A-32-7D-2C-7B-22-47-6F-6C-64-22-3A-31-38-30-2C-22-43-61-72-64-73-22-3A-33-7D-2C-7B-22-47-6F-6C-64-22-3A-32-34-30-2C-22-4D-69-6C-65-73-74-6F-6E-65-22-3A-7B-22-43-68-65-73-74-22-3A-22-47-6F-6C-64-5F-3C-63-75-72-72-65-6E-74-5F-61-72-65-6E-61-3E-22-2C-22-54-79-70-65-22-3A-22-43-68-65-73-74-22-7D-2C-22-43-61-72-64-73-22-3A-35-7D-2C-7B-22-47-6F-6C-64-22-3A-33-31-30-2C-22-43-61-72-64-73-22-3A-38-7D-2C-7B-22-47-6F-6C-64-22-3A-33-39-30-2C-22-4D-69-6C-65-73-74-6F-6E-65-22-3A-7B-22-54-79-70-65-22-3A-22-47-6F-6C-64-22-2C-22-41-6D-6F-75-6E-74-22-3A-31-30-30-30-7D-2C-22-43-61-72-64-73-22-3A-31-32-7D-2C-7B-22-47-6F-6C-64-22-3A-34-38-30-2C-22-43-61-72-64-73-22-3A-31-37-7D-2C-7B-22-47-6F-6C-64-22-3A-36-30-30-2C-22-4D-69-6C-65-73-74-6F-6E-65-22-3A-7B-22-49-73-48-69-67-68-6C-69-67-68-74-65-64-22-3A-74-72-75-65-2C-22-43-68-65-73-74-22-3A-22-47-69-61-6E-74-5F-3C-63-75-72-72-65-6E-74-5F-61-72-65-6E-61-3E-22-2C-22-54-79-70-65-22-3A-22-43-68-65-73-74-22-7D-2C-22-43-61-72-64-73-22-3A-32-35-7D-5D-2C-22-41-72-65-6E-61-22-3A-22-41-72-65-6E-61-5F-54-6F-75-63-68-64-6F-77-6E-54-65-73-74-22-2C-22-57-69-6E-49-63-6F-6E-45-78-70-6F-72-74-4E-61-6D-65-22-3A-22-74-6F-75-72-6E-61-6D-65-6E-74-5F-6F-70-65-6E-5F-77-69-6E-73-5F-62-61-64-67-65-5F-62-72-6F-6E-7A-65-22-2C-22-49-63-6F-6E-45-78-70-6F-72-74-4E-61-6D-65-22-3A-22-69-63-6F-6E-5F-74-6F-75-72-6E-61-6D-65-6E-74-5F-74-6F-75-63-68-64-6F-77-6E-22-2C-22-53-75-62-74-69-74-6C-65-22-3A-22-4F-62-74-65-6E-65-7A-20-74-6F-75-74-65-73-20-6C-65-73-20-72-C3-A9-63-6F-6D-70-65-6E-73-65-73-20-61-76-65-63-20-36-C2-A0-76-69-63-74-6F-69-72-65-73-C2-A0-21-22-2C-22-44-65-73-63-72-69-70-74-69-6F-6E-22-3A-22-47-61-67-6E-65-7A-20-75-6E-65-20-63-6F-75-72-6F-6E-6E-65-20-65-6E-20-61-6D-65-6E-61-6E-74-20-75-6E-65-20-75-6E-69-74-C3-A9-20-C3-A0-20-6C-27-65-6E-2D-62-75-74-20-61-64-76-65-72-73-65-2E-20-52-65-6D-70-6F-72-74-65-7A-20-6C-65-20-63-6F-6D-62-61-74-20-61-76-65-63-20-33-C2-A0-63-6F-75-72-6F-6E-6E-65-73-2E-20-4F-62-74-65-6E-65-7A-20-74-6F-75-74-65-73-20-6C-65-73-20-72-C3-A9-63-6F-6D-70-65-6E-73-65-73-20-61-75-20-62-6F-75-74-20-64-65-20-36-C2-A0-76-69-63-74-6F-69-72-65-73-2E-22-2C-22-42-61-63-6B-67-72-6F-75-6E-64-22-3A-7B-22-50-61-74-68-22-3A-22-2F-39-35-61-62-66-65-62-37-32-66-35-34-31-37-36-61-35-35-64-34-31-31-62-32-30-32-31-35-38-34-33-66-5F-74-6F-75-63-68-64-6F-77-6E-5F-63-68-61-6C-6C-65-6E-67-65-5F-30-32-5F-36-36-36-36-5F-61-6C-70-68-61-5F-70-72-65-6D-75-6C-2E-70-6E-67-22-2C-22-43-68-65-63-6B-73-75-6D-22-3A-22-39-35-61-62-66-65-62-37-32-66-35-34-31-37-36-61-35-35-64-34-31-31-62-32-30-32-31-35-38-34-33-66-22-2C-22-46-69-6C-65-22-3A-22-74-6F-75-63-68-64-6F-77-6E-5F-63-68-61-6C-6C-65-6E-67-65-5F-30-32-5F-36-36-36-36-5F-61-6C-70-68-61-5F-70-72-65-6D-75-6C-2E-70-6E-67-22-7D-2C-22-42-61-63-6B-67-72-6F-75-6E-64-5F-43-6F-6D-70-6C-65-74-65-22-3A-7B-22-50-61-74-68-22-3A-22-2F-39-35-61-62-66-65-62-37-32-66-35-34-31-37-36-61-35-35-64-34-31-31-62-32-30-32-31-35-38-34-33-66-5F-74-6F-75-63-68-64-6F-77-6E-5F-63-68-61-6C-6C-65-6E-67-65-5F-30-32-5F-36-36-36-36-5F-61-6C-70-68-61-5F-70-72-65-6D-75-6C-2E-70-6E-67-22-2C-22-43-68-65-63-6B-73-75-6D-22-3A-22-39-35-61-62-66-65-62-37-32-66-35-34-31-37-36-61-35-35-64-34-31-31-62-32-30-32-31-35-38-34-33-66-22-2C-22-46-69-6C-65-22-3A-22-74-6F-75-63-68-64-6F-77-6E-5F-63-68-61-6C-6C-65-6E-67-65-5F-30-32-5F-36-36-36-36-5F-61-6C-70-68-61-5F-70-72-65-6D-75-6C-2E-70-6E-67-22-7D-2C-22-55-6E-6C-6F-63-6B-65-64-46-6F-72-58-50-22-3A-22-45-78-70-65-72-69-65-6E-63-65-64-22-2C-22-44-72-61-66-74-44-65-63-6B-22-3A-22-44-72-61-66-74-5F-54-6F-75-63-68-64-6F-77-6E-5F-76-31-22-2C-22-54-69-74-6C-65-22-3A-22-44-C3-A9-66-69-20-74-6F-75-63-68-64-6F-77-6E-20-32-63-32-22-2C-22-53-74-61-72-74-4E-6F-74-69-66-69-63-61-74-69-6F-6E-22-3A-22-4C-65-20-64-C3-A9-66-69-20-74-6F-75-63-68-64-6F-77-6E-20-32-63-32-20-61-20-63-6F-6D-6D-65-6E-63-C3-A9-C2-A0-21-20-4A-6F-75-65-7A-20-61-76-65-63-20-75-6E-20-61-6D-69-20-6F-75-20-75-6E-20-C3-A9-71-75-69-70-69-65-72-20-61-6C-C3-A9-61-74-6F-69-72-65-C2-A0-21-22-7D-B4-12-00-00-00-13-32-76-32-20-54-6F-75-63-68-64-6F-77-6E-20-51-75-65-73-74-07-B0-F9-D8-9D-0B-B0-E3-A2-9E-0B-B0-F9-D8-9D-0B-00-00-00-00-00-00-00-00-00-00-00-13-32-76-32-20-54-6F-75-63-68-64-6F-77-6E-20-51-75-65-73-74-00-00-00-FF-7B-22-51-75-65-73-74-54-79-70-65-22-3A-22-57-69-6E-22-2C-22-57-69-6E-22-3A-7B-22-54-79-70-65-22-3A-22-43-72-6F-77-6E-73-22-2C-22-45-76-65-6E-74-73-22-3A-5B-31-32-30-31-2C-31-32-30-33-2C-31-32-30-35-5D-7D-2C-22-54-69-74-6C-65-22-3A-22-51-75-C3-AA-74-65-20-74-6F-75-63-68-64-6F-77-6E-20-32-63-32-22-2C-22-49-6E-66-6F-22-3A-22-4F-62-74-65-6E-65-7A-20-34-30-C2-A0-63-6F-75-72-6F-6E-6E-65-73-20-6C-6F-72-73-20-64-65-20-6C-E2-80-99-C3-A9-76-C3-A8-6E-65-6D-65-6E-74-20-74-6F-75-63-68-64-6F-77-6E-20-32-63-32-22-2C-22-43-6F-75-6E-74-22-3A-34-30-2C-22-4D-69-6E-4C-65-76-65-6C-22-3A-35-2C-22-50-6F-69-6E-74-73-22-3A-32-30-2C-22-43-68-72-6F-6E-6F-73-51-75-65-73-74-52-65-77-61-72-64-73-22-3A-5B-7B-22-54-79-70-65-22-3A-22-45-70-69-63-22-2C-22-43-6F-75-6E-74-22-3A-34-7D-5D-7D-B5-12-00-00-00-1D-32-76-32-20-54-6F-75-63-68-64-6F-77-6E-20-45-6C-69-74-65-20-43-68-61-6C-6C-65-6E-67-65-02-B0-91-83-9E-0B-B0-E3-A2-9E-0B-B0-91-83-9E-0B-00-00-00-00-00-00-00-00-00-00-00-1D-32-76-32-20-54-6F-75-63-68-64-6F-77-6E-20-45-6C-69-74-65-20-43-68-61-6C-6C-65-6E-67-65-00-00-06-2E-7B-22-47-61-6D-65-4D-6F-64-65-22-3A-22-54-65-61-6D-56-73-54-65-61-6D-5F-54-6F-75-63-68-64-6F-77-6E-5F-44-72-61-66-74-22-2C-22-46-72-65-65-50-61-73-73-22-3A-30-2C-22-4A-6F-69-6E-43-6F-73-74-22-3A-31-30-30-2C-22-4A-6F-69-6E-43-6F-73-74-52-65-73-6F-75-72-63-65-22-3A-22-47-65-6D-73-22-2C-22-4D-61-78-4C-6F-73-73-65-73-22-3A-33-2C-22-52-65-77-61-72-64-73-22-3A-5B-7B-22-47-6F-6C-64-22-3A-37-30-30-2C-22-43-61-72-64-73-22-3A-31-30-7D-2C-7B-22-47-6F-6C-64-22-3A-39-35-30-2C-22-43-61-72-64-73-22-3A-31-35-7D-2C-7B-22-47-6F-6C-64-22-3A-31-32-35-30-2C-22-43-61-72-64-73-22-3A-32-35-7D-2C-7B-22-47-6F-6C-64-22-3A-31-36-30-30-2C-22-4D-69-6C-65-73-74-6F-6E-65-22-3A-7B-22-54-79-70-65-22-3A-22-47-6F-6C-64-22-2C-22-41-6D-6F-75-6E-74-22-3A-33-30-30-30-7D-2C-22-43-61-72-64-73-22-3A-34-33-7D-2C-7B-22-47-6F-6C-64-22-3A-32-30-30-30-2C-22-43-61-72-64-73-22-3A-36-35-7D-2C-7B-22-47-6F-6C-64-22-3A-32-35-30-30-2C-22-4D-69-6C-65-73-74-6F-6E-65-22-3A-7B-22-54-79-70-65-22-3A-22-52-61-6E-64-6F-6D-53-70-65-6C-6C-22-2C-22-41-6D-6F-75-6E-74-22-3A-33-2C-22-52-61-6E-64-6F-6D-53-70-65-6C-6C-22-3A-22-45-70-69-63-22-7D-2C-22-43-61-72-64-73-22-3A-39-33-7D-2C-7B-22-47-6F-6C-64-22-3A-33-31-30-30-2C-22-43-61-72-64-73-22-3A-31-32-35-7D-2C-7B-22-47-6F-6C-64-22-3A-33-38-30-30-2C-22-4D-69-6C-65-73-74-6F-6E-65-22-3A-7B-22-54-79-70-65-22-3A-22-47-6F-6C-64-22-2C-22-41-6D-6F-75-6E-74-22-3A-33-30-30-30-30-7D-2C-22-43-61-72-64-73-22-3A-31-36-35-7D-2C-7B-22-47-6F-6C-64-22-3A-34-36-35-30-2C-22-43-61-72-64-73-22-3A-32-31-30-7D-2C-7B-22-47-6F-6C-64-22-3A-36-30-30-30-2C-22-4D-69-6C-65-73-74-6F-6E-65-22-3A-7B-22-49-73-48-69-67-68-6C-69-67-68-74-65-64-22-3A-74-72-75-65-2C-22-43-68-65-73-74-22-3A-22-4C-65-67-65-6E-64-61-72-79-22-2C-22-54-79-70-65-22-3A-22-43-68-65-73-74-22-7D-2C-22-43-61-72-64-73-22-3A-33-30-30-7D-5D-2C-22-49-63-6F-6E-45-78-70-6F-72-74-4E-61-6D-65-22-3A-22-69-63-6F-6E-5F-74-6F-75-72-6E-61-6D-65-6E-74-5F-74-6F-75-63-68-64-6F-77-6E-5F-67-72-61-6E-64-22-2C-22-57-69-6E-49-63-6F-6E-45-78-70-6F-72-74-4E-61-6D-65-22-3A-22-74-6F-75-72-6E-61-6D-65-6E-74-5F-6F-70-65-6E-5F-77-69-6E-73-5F-62-61-64-67-65-5F-67-6F-6C-64-22-2C-22-41-72-65-6E-61-22-3A-22-41-72-65-6E-61-5F-54-6F-75-63-68-64-6F-77-6E-54-65-73-74-22-2C-22-53-75-62-74-69-74-6C-65-22-3A-22-4F-62-74-65-6E-65-7A-20-74-6F-75-74-65-73-20-6C-65-73-20-72-C3-A9-63-6F-6D-70-65-6E-73-65-73-20-61-76-65-63-20-39-C2-A0-76-69-63-74-6F-69-72-65-73-C2-A0-21-22-2C-22-55-6E-6C-6F-63-6B-65-64-46-6F-72-58-50-22-3A-22-45-78-70-65-72-69-65-6E-63-65-64-22-2C-22-42-61-63-6B-67-72-6F-75-6E-64-22-3A-7B-22-50-61-74-68-22-3A-22-2F-64-66-65-36-64-62-32-31-63-63-65-61-38-63-66-35-36-39-32-38-62-34-32-36-62-37-30-35-61-33-36-36-5F-74-6F-75-63-68-64-6F-77-6E-5F-63-68-61-6C-6C-65-6E-67-65-5F-30-33-5F-36-36-36-36-5F-61-6C-70-68-61-5F-70-72-65-6D-75-6C-2E-70-6E-67-22-2C-22-43-68-65-63-6B-73-75-6D-22-3A-22-64-66-65-36-64-62-32-31-63-63-65-61-38-63-66-35-36-39-32-38-62-34-32-36-62-37-30-35-61-33-36-36-22-2C-22-46-69-6C-65-22-3A-22-74-6F-75-63-68-64-6F-77-6E-5F-63-68-61-6C-6C-65-6E-67-65-5F-30-33-5F-36-36-36-36-5F-61-6C-70-68-61-5F-70-72-65-6D-75-6C-2E-70-6E-67-22-7D-2C-22-42-61-63-6B-67-72-6F-75-6E-64-5F-43-6F-6D-70-6C-65-74-65-22-3A-7B-22-50-61-74-68-22-3A-22-2F-64-66-65-36-64-62-32-31-63-63-65-61-38-63-66-35-36-39-32-38-62-34-32-36-62-37-30-35-61-33-36-36-5F-74-6F-75-63-68-64-6F-77-6E-5F-63-68-61-6C-6C-65-6E-67-65-5F-30-33-5F-36-36-36-36-5F-61-6C-70-68-61-5F-70-72-65-6D-75-6C-2E-70-6E-67-22-2C-22-43-68-65-63-6B-73-75-6D-22-3A-22-64-66-65-36-64-62-32-31-63-63-65-61-38-63-66-35-36-39-32-38-62-34-32-36-62-37-30-35-61-33-36-36-22-2C-22-46-69-6C-65-22-3A-22-74-6F-75-63-68-64-6F-77-6E-5F-63-68-61-6C-6C-65-6E-67-65-5F-30-33-5F-36-36-36-36-5F-61-6C-70-68-61-5F-70-72-65-6D-75-6C-2E-70-6E-67-22-7D-2C-22-44-72-61-66-74-44-65-63-6B-22-3A-22-44-72-61-66-74-5F-54-6F-75-63-68-64-6F-77-6E-5F-76-31-22-2C-22-54-69-74-6C-65-22-3A-22-44-C3-A9-66-69-20-74-6F-75-63-68-64-6F-77-6E-20-32-63-32-20-64-E2-80-99-C3-A9-6C-69-74-65-22-2C-22-44-65-73-63-72-69-70-74-69-6F-6E-22-3A-22-47-61-67-6E-65-7A-20-75-6E-65-20-63-6F-75-72-6F-6E-6E-65-20-65-6E-20-61-6D-65-6E-61-6E-74-20-75-6E-65-20-75-6E-69-74-C3-A9-20-C3-A0-20-6C-27-65-6E-2D-62-75-74-20-61-64-76-65-72-73-65-2E-20-52-65-6D-70-6F-72-74-65-7A-20-6C-65-20-63-6F-6D-62-61-74-20-61-76-65-63-20-33-C2-A0-63-6F-75-72-6F-6E-6E-65-73-2E-20-4F-62-74-65-6E-65-7A-20-74-6F-75-74-65-73-20-6C-65-73-20-72-C3-A9-63-6F-6D-70-65-6E-73-65-73-20-61-75-20-62-6F-75-74-20-64-65-20-39-C2-A0-76-69-63-74-6F-69-72-65-73-2E-22-2C-22-53-74-61-72-74-4E-6F-74-69-66-69-63-61-74-69-6F-6E-22-3A-22-4C-65-20-64-C3-A9-66-69-20-74-6F-75-63-68-64-6F-77-6E-20-32-63-32-20-64-E2-80-99-C3-A9-6C-69-74-65-20-61-20-63-6F-6D-6D-65-6E-63-C3-A9-C2-A0-21-20-4A-6F-75-65-7A-20-61-76-65-63-20-75-6E-20-61-6D-69-C2-A0-21-22-7D-00-00-00-00-00-00-7F-7F-00-00-00-0B-83-09-00-84-09-00-85-09-00-95-11-01-AF-12-01-B0-12-01-B1-12-01-B2-12-01-B3-12-01-B4-12-01-B5-12-01-08-95-11-02-00-AF-12-03-00-B0-12-02-00-B1-12-02-00-B2-12-02-00-B3-12-03-00-B4-12-02-00-B5-12-02-00-10-00-00-00-92-7B-22-49-44-22-3A-22-43-4C-41-4E-5F-43-48-45-53-54-22-2C-22-50-61-72-61-6D-73-22-3A-7B-22-53-74-61-72-74-54-69-6D-65-22-3A-22-32-30-31-37-30-33-31-37-54-30-37-30-30-30-30-2E-30-30-30-5A-22-2C-22-41-63-74-69-76-65-44-75-72-61-74-69-6F-6E-22-3A-22-50-33-64-54-30-68-22-2C-22-49-6E-61-63-74-69-76-65-44-75-72-61-74-69-6F-6E-22-3A-22-50-34-64-54-30-68-22-2C-22-43-68-65-73-74-54-79-70-65-22-3A-5B-22-43-6C-61-6E-43-72-6F-77-6E-73-22-5D-7D-7D-00".HexaToBytes());

            Packet.AddVInt(this.ChestSlotCount);

            for (int i = 0; i < this.ChestSlotCount; i++)
            {
                Packet.AddBoolean(false);
            }

            new Timer().Encode(Packet);
            new Timer().Encode(Packet);

            if (false)
            {
                Packet.AddBoolean(true);
                new Chest().Encode(Packet);
            }
            else
                Packet.AddBoolean(false);

            if (false)
            {
                Packet.AddBoolean(true);
                new Chest().Encode(Packet);
            }
            else
                Packet.AddBoolean(false);

            Packet.EncodeData(null); // 66xxxxxx
            Packet.AddBoolean(false);
            Packet.AddVInt(0);
            Packet.AddVInt(0);
            Packet.AddVInt(0);
            Packet.AddVInt(0);
            Packet.AddVInt(0);
            Packet.AddVInt(0);
            Packet.AddVInt(0); // v3 + 636

            if ( /* v3 + 636 > 0 */ false)
            {
                Packet.AddVInt(0);
                Packet.AddVInt(0);
            }

            Packet.AddVInt(0);

            Packet.AddRange("90-C1-C0-01-84-F7-D2-01-92-F9-9C-9E-0B".HexaToBytes()); // Timer
            Packet.AddRange("90-C1-C0-01-84-F7-D2-01-92-F9-9C-9E-0B".HexaToBytes()); // Timer

            if (false)
            {
                Packet.AddBoolean(true);
                // Chest Encode.
            }
            else
                Packet.AddBoolean(false);

            new Timer().Encode(Packet);

            Packet.AddVInt(2817); // TutorialStep

            for (int j = 0; j < 7; j++)
            {
                Packet.AddVInt(0);
            }

            Packet.AddVInt(this.PageOpened);
            Packet.AddVInt(this.LastShownLevelUp);
            Packet.EncodeData(this.LastShownArena);
            Packet.AddVInt(this.LastTick.DayOfYear + 1);
            Packet.AddVInt(471);
            Packet.AddVInt(DateTime.UtcNow.DayOfYear + 1);

            this.NextShopCycleTimer.Encode(Packet);

            if (false)
            {
                Packet.AddBoolean(true);
                // ?
            }
            else
                Packet.AddBoolean(false);

            new Timer().Encode(Packet);
            new Timer().Encode(Packet);
            new Timer().Encode(Packet);

            Packet.AddVInt(0);
            Packet.AddVInt(0);
            Packet.AddVInt(0);
            Packet.AddVInt(0);
            Packet.AddVInt(0);
            Packet.AddVInt(0);
            Packet.AddVInt(0);
            Packet.AddVInt(0);

            Packet.AddVInt(0); // Array Count

            for (int i = 0; i < 0; i++)
            {
                Packet.AddVInt(0);
            }

            Packet.EncodeSpellList(new List<SpellData>(0));

            Packet.AddBoolean(false);
            Packet.AddVInt(10);
            Packet.AddVInt(0); // Array Count

            Packet.AddVInt(0);
            Packet.AddVInt(0);
            Packet.AddVInt(0);
            Packet.AddBoolean(false);
            Packet.AddBoolean(false);
            Packet.AddBoolean(false);

            new SpellDeck().Encode(Packet);

            if (false)
            {
                Packet.AddBoolean(true);

                // sub_2CB81A
                {
                    // ?
                }
            }
            else
                Packet.AddBoolean(false);

            if (false)
            {
                Packet.AddBoolean(true);

                // sub_2CBB8E
                {
                    // ?
                }
            }
            else
                Packet.AddBoolean(false);

            Packet.AddVInt(0); // Count

            for (int i = 0; i < 0; i++)
            {
                // ?
            }

            Packet.AddVInt(0); // Count

            for (int i = 0; i < 0; i++)
            {
                // ?
            }


            Packet.AddRange("00-00-00-00-00-00-01-00-00-00-01-01-8A-E6-BF-33-00-00-00-00-00-00-06-01-01-01-00-00-00-10-51-75-65-73-74-5F-46-72-65-65-43-68-65-73-74-73-00-00-00-14-54-49-44-5F-46-52-45-45-5F-43-48-45-53-54-5F-51-55-45-53-54-00-00-00-14-54-49-44-5F-46-52-45-45-5F-43-48-45-53-54-5F-51-55-45-53-54-00-00-00-08-73-63-2F-75-69-2E-73-63-00-00-00-15-71-75-65-73-74-5F-69-74-65-6D-5F-66-72-65-65-5F-63-68-65-73-74-05-03-00-01-00-00-00-00-00-00-03-13-07-01-13-07-01-13-07-01-03-01-00-00-03-00-04-04-01-9C-A7-12-80-94-23-A0-A1-94-9E-0B-00-01-02-00-00-00-00-00-00-00-14-51-75-C3-AA-74-65-20-74-6F-75-63-68-64-6F-77-6E-20-32-63-32-00-00-00-3B-4F-62-74-65-6E-65-7A-20-34-30-C2-A0-63-6F-75-72-6F-6E-6E-65-73-20-6C-6F-72-73-20-64-65-20-6C-E2-80-99-C3-A9-76-C3-A8-6E-65-6D-65-6E-74-20-74-6F-75-63-68-64-6F-77-6E-20-32-63-32-00-00-00-08-73-63-2F-75-69-2E-73-63-00-00-00-16-71-75-65-73-74-5F-69-74-65-6D-5F-73-70-65-63-69-61-6C-5F-70-76-70-14-28-B4-12-00-00-01-00-00-00-1C-69-63-6F-6E-5F-71-75-65-73-74-5F-74-79-70-65-5F-73-70-65-63-69-61-6C-65-76-65-6E-74-01-0E-02-04-02-00-00-00-03-B1-12-B3-12-B5-12-05-32-01-00-9F-04-00-01-01-00-03-06-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00-00".HexaToBytes());

            Packet.AddVInt(0);
            Packet.AddVInt(0);
            Packet.AddVInt(5);

            this.ShopCycle.Encode(Packet);
            
            // sub_2FDFF2
            {
                Packet.AddVInt(0); // Array
                Packet.AddBoolean(false);
            }

            Packet.AddVInt(0); // Array
            Packet.AddVInt(0); // Array

            Packet.AddVInt(0);
            Packet.AddVInt(0);
            Packet.AddVInt(0);
            Packet.AddVInt(0);
        }

        /// <summary>
        /// Loads this instance from json.
        /// </summary>
        internal void Load(JToken Token)
        {
            JsonHelper.GetJsonNumber(Token, "home_hi", out this.HighID);
            JsonHelper.GetJsonNumber(Token, "home_lo", out this.LowID);

            this.SpellDeck.Load(Token);
            this.SpellCollection.Load(Token);

            if(JsonHelper.GetJsonObject(Token, "next_shop_t", out JToken JObject))
            {
                this.NextShopCycleTimer.Load(JObject);
            }

            if (JsonHelper.GetJsonArray(Token, "deck_presets", out JArray DeckPresets))
            {
                for (int i = Math.Min(DeckPresets.Count, this.DeckPresets.Length) - 1; i >= 0; i--)
                {
                    JArray Deck = (JArray) DeckPresets[i];

                    for (int j = Math.Min(Deck.Count, this.DeckPresets.Length) - 1; j >= 0; j--)
                    {
                        this.DeckPresets[i][j] = (int) Deck[j];
                    }
                }
            }
            
            if (!JsonHelper.GetJsonDateTime(Token, "last_t", out this.LastTick))
            {
                this.LastTick = DateTime.UtcNow;
            }

            JsonHelper.GetJsonNumber(Token, "selected_deck", out this.SelectedDeck);
            JsonHelper.GetJsonNumber(Token, "page_opened", out this.PageOpened);
            JsonHelper.GetJsonNumber(Token, "collected_free_chest", out this.CollectedFreeChest);
            JsonHelper.GetJsonNumber(Token, "donation_capacity_limit", out this.DonationCapacityLimit);

            if (!JsonHelper.GetJsonNumber(Token, "last_shown_level_up", out this.LastShownLevelUp))
            {
                this.LastShownLevelUp = 1;
            }

            if (!JsonHelper.GetJsonData(Token, "last_shown_arena", out this.LastShownArena))
            {
                this.LastShownArena = CSV.Tables.Get(Gamefile.Arena).GetWithInstanceID<ArenaData>(0);
            }

            JsonHelper.GetJsonNumber(Token, "tutorial_step", out this.TutorialStep);
        }

        /// <summary>
        /// Saves this instance to json.
        /// </summary>
        /// <returns></returns>
        internal JObject Save()
        {
            JObject Json = new JObject();

            Json.Add("home_hi", this.HighID);
            Json.Add("home_lo", this.LowID);

            this.SpellDeck.Save(Json);
            this.SpellCollection.Save(Json);

            JArray DeckPresets = new JArray();

            for (int i = 0; i < this.DeckPresets.Length; i++)
            {
                JArray Deck = new JArray();

                for (int j = 0; j < this.DeckPresets[i].Length; j++)
                {
                    Deck.Add(this.DeckPresets[i][j]);
                }

                DeckPresets.Add(Deck);
            }

            Json.Add("deck_presets", DeckPresets);

            Json.Add("next_shop_t", this.NextShopCycleTimer.Save(this.Time));
            Json.Add("last_t", this.LastTick);

            Json.Add("selected_deck", this.SelectedDeck);
            Json.Add("page_opened", this.PageOpened);
            Json.Add("collected_free_chest", this.CollectedFreeChest);
            Json.Add("donation_capacity_limit", this.DonationCapacityLimit);
            Json.Add("last_shown_level_up", this.LastShownLevelUp);
            Json.Add("tutorial_step", this.TutorialStep);

            JsonHelper.SetLogicData(Json, "last_shown_arena", this.LastShownArena);

            return Json;
        }
    }

    internal class HomeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value != null)
            {
                ((Home) value).Save().WriteTo(writer);
            }
            else
                GL.Servers.Files.Home.JObject.WriteTo(writer);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject Json = JObject.Load(reader);

            if (!(existingValue is Home Home))
            {
                Home = new Home();
            }

            Home.Load(Json);

            return Home;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Home);
        }
    }
}