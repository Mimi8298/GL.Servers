namespace GL.Servers.CR.Logic
{
    using System.Collections.Generic;
    using GL.Servers.CR.Logic.Spells;
    using GL.Servers.DataStream;

    internal class Reward
    {
        internal List<Spell> Spells1;
        internal List<Spell> Spells2;

        /// <summary>
        /// Initializes a new instance of the <see cref="Reward"/> class.
        /// </summary>
        public Reward()
        {
            this.Spells1 = new List<Spell>();
            this.Spells2 = new List<Spell>();
        }

        /// <summary>
        /// Decodes this instance.
        /// </summary>
        internal void Decode(ByteStream Packet)
        {
            int Count = Packet.ReadVInt();

            if (Count > -1)
            {
                this.Spells1 = new List<Spell>(Count);

                for (int i = 0; i < Count; i++)
                {
                    Spell Spell = new Spell(null);
                    Spell.Decode(Packet);
                    this.Spells1.Add(Spell);
                }
            }

            Count = Packet.ReadVInt();

            if (Count > -1)
            {
                this.Spells2 = new List<Spell>(Count);

                for (int i = 0; i < Count; i++)
                {
                    Spell Spell = new Spell(null);
                    Spell.Decode(Packet);
                    this.Spells2.Add(Spell);
                }
            }

            Packet.ReadVInt();
            Packet.ReadVInt();
        }

        /// <summary>
        /// Encodes this instance.
        /// </summary>
        internal void Encode(ChecksumEncoder Packet)
        {
            if (this.Spells1 != null)
            {
                Packet.AddVInt(this.Spells1.Count);

                this.Spells1.ForEach(Spell =>
                {
                    Spell.Encode(Packet);
                });
            }
            else 
                Packet.AddVInt(-1);

            if (this.Spells2 != null)
            {
                Packet.AddVInt(this.Spells2.Count);

                this.Spells2.ForEach(Spell =>
                {
                    Spell.Encode(Packet);
                });
            }
            else
                Packet.AddVInt(-1);

            Packet.AddVInt(0);
            Packet.AddVInt(0);
        }
    }
}