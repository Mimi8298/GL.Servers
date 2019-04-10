namespace GL.Servers.SL.Logic.Avatar.Slots
{
    using GL.Servers.SL.Extensions.Game;
    using GL.Servers.SL.Logic.Avatar.Items;
    using GL.Servers.SL.Logic.Slots.Items;

    internal class ResourceSlots : DataSlots
    {
        internal ResourceSlots(int Capacity = 10) : base(Capacity)
        {
            // ResourceSlots.
        }

        internal ResourceSlots(Player Player, int Capacity = 10) : base(Player, Capacity)
        {
            // ResourceSlots.
        }

        internal void Initialize()
        {
            this.Set(new Item(2000001, GameSettings.StartingGold));
            this.Set(new Item(2000002, 15));
        }
    }
}