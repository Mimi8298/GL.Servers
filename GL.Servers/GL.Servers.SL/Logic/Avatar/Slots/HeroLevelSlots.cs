namespace GL.Servers.SL.Logic.Avatar.Slots
{
    using GL.Servers.SL.Extensions.Game;
    using GL.Servers.SL.Logic.Avatar.Items;

    internal class HeroLevelSlots : DataSlots
    {
        internal HeroLevelSlots(int Capacity = 25) : base(Capacity)
        {
            // HeroSlots.
        }

        internal HeroLevelSlots(Player Player, int Capacity = 25) : base(Player, Capacity)
        {
            // HeroSlots.
        }

        internal void Initialize()
        {
            this.Set(new Item(GameSettings.StartingCharacter.GlobalID, 0));
        }
    }
}