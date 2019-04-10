namespace GL.Servers.SL.Logic.Attack.Slots.Items
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    internal class Battle
    {
        [JsonProperty("enemies")]  internal List<Enemy> Enemies;
        [JsonProperty("obstacle")] internal List<Obstacle> Obstacles;
    }
}