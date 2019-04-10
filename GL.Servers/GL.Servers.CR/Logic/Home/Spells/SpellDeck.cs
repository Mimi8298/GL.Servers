namespace GL.Servers.CR.Logic.Spells
{
    using System.Collections.Generic;
    using GL.Servers.CR.Core;
    using GL.Servers.CR.Extensions.Helper;
    using GL.Servers.CR.Files.Logic;
    using GL.Servers.DataStream;

    using Newtonsoft.Json.Linq;

    internal class SpellDeck : List<Spell>
    {
        /// <summary>
        /// Gets if this instance is empty
        /// </summary>
        internal bool Empty
        {
            get
            {
                for (int i = 0; i < 8; i++)
                {
                    if (this[i] == null)
                        return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Gets if this instance is full
        /// </summary>
        internal bool Full
        {
            get
            {
                for (int i = 0; i < 8; i++)
                {
                    if (this[i] == null)
                        return false;
                }

                return true;
            }
        }

        /// <summary>
        /// Gets the spell count in collection.
        /// </summary>
        internal int SpellCount
        {
            get
            {
                int Count = 0;

                for (int i = 0; i < 8; i++)
                {
                    if (this[i] != null)
                        ++Count;
                }

                return Count;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpellDeck"/> class.
        /// </summary>
        public SpellDeck()
        {
            for (int i = 0; i < 8; i++)
            {
                this.Add(null);
            }
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        internal SpellDeck Clone()
        {
            SpellDeck SpellDeck = new SpellDeck();

            for (int i = 0; i < 8; i++)
            {
                if (this[i] != null)
                {
                    SpellDeck[i] = this[i].Clone();
                }
            }

            return SpellDeck;
        }

        /// <summary>
        /// Returns if can be insert the specified spell.
        /// </summary>
        internal bool CanBeInserted(int Index, Spell Spell)
        {
            if (Index < 8)
            {
                Spell Existing = this[Index];

                if (Existing != null)
                {
                    if (Existing.Data.Equals(Spell.Data) && !Existing.Equals(Spell))
                    {
                        return true;
                    }
                }

                for (int i = 0; i < 8; i++)
                {
                    Existing = this[i];

                    if (Existing != null)
                    {
                        if (Existing.Equals(Spell))
                        {
                            return false;
                        }

                        if (Existing.Data.Equals(Spell.Data))
                        {
                            return false;
                        }
                    }
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns if this instance contains the specified spell data.
        /// </summary>
        internal bool ContainsSpellData(SpellData SpellData)
        {
            return this.GetSpellByData(SpellData) != null;
        }

        /// <summary>
        /// Returns if this instance contains the specified spell data.
        /// </summary>
        internal void MoveSpellFromCollection(int DeckIndex, int CollectionIndex, SpellCollection Collection)
        {
            Spell SpellCollection = Collection[DeckIndex];

            if (this.CanBeInserted(DeckIndex, SpellCollection))
            {
                if (this[DeckIndex] != null)
                {
                    this[DeckIndex] = Collection.SwapSpell(this[DeckIndex], CollectionIndex);
                }
                else
                    this.PutSpellInEmptySlot(DeckIndex, SpellCollection);
            }
            else 
                Logging.Error(this.GetType(), "CanBeInserted returns false, should check it before trying to move.");
        }

        /// <summary>
        /// Gets the spell id in collection by data.
        /// </summary>
        internal int GetSpellIdxByData(SpellData Data)
        {
            return this.FindIndex(S => S.Data == Data);
        }

        /// <summary>
        /// Gets a spell by data.
        /// </summary>
        internal Spell GetSpellByData(SpellData Data)
        {
            return this.Find(S => S.Data == Data);
        }

        /// <summary>
        /// Puts the spell in empty slot.
        /// </summary>
        internal void PutSpellInEmptySlot(int Index, Spell Spell)
        {
            if (Index >= 8)
            {
                Logging.Error(this.GetType(), "PutSpell() - Index is out of bounds " + (Index + 1) + "/" + 8 + ".");
                return;
            }

            if (this[Index] != null)
            {
                Logging.Error(this.GetType(), "PutSpell() - Trying to overwrite a spell at " + Index + ".");
                return;
            }

            this[Index] = Spell;
        }

        /// <summary>
        /// Sets the spell at specified index.
        /// </summary>
        internal void SetSpell(int Index, Spell Spell)
        {
            this[Index] = Spell;
        }

        /// <summary>
        /// Swaps two spells.
        /// </summary>
        internal void SwapSpells(int SpellId1, int SpellId2)
        {
            Spell SwapTemp = this[SpellId1];
            this[SpellId1] = this[SpellId2];
            this[SpellId2] = SwapTemp;
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal void Decode(ByteStream Reader)
        {
            for (int i = 0; i < 8; i++)
            {
                if (Reader.ReadBoolean())
                {
                    this[i] = new Spell(null);
                }
            }

            for (int i = 0; i < 8; i++)
            {
                if (this[i] != null)
                {
                    this[i].Decode(Reader);
                }
            }
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal void Encode(ByteStream Packet)
        {
            for (int i = 0; i < 8; i++)
            {
                Packet.AddBoolean(this[i] != null);
            }
            
            for (int i = 0; i < 8; i++)
            {
                if (this[i] != null)
                {
                    this[i].Encode(Packet);
                }
            }
        }

        /// <summary>
        /// Loads this instance from json.
        /// </summary>
        internal void Load(JToken Token)
        {
            if (JsonHelper.GetJsonArray(Token, "decks", out JArray Array))
            {
                for (int i = 0; i < Array.Count; i++)
                {
                    this[i] = new Spell(null);
                    this[i].Load(Array[i]);
                }
            }
        }

        /// <summary>
        /// Saves this instance to json.
        /// </summary>
        internal void Save(JObject Json)
        {
            JArray Array = new JArray();

            for (int i = 0; i < 8; i++)
            {
                if (this[i] != null)
                {
                    Array.Add(this[i].Save());
                }
            }

            Json.Add("decks", Array);
        }
    }
}