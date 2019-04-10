namespace GL.Servers.CoC.Logic.Attack.Manager
{
    using GL.Servers.CoC.Packets;
    using System.Collections.Generic;

    /*
    internal class LiveReplayCommandManager
    {
        internal BattleManager BattleManager;

        internal List<Command> Commands;
        internal List<Command> BufferedCommands;
        
        public LiveReplayCommandManager()
        {
            this.Commands         = new List<Command>(256);
            this.BufferedCommands = new List<Command>(8192);
        }

        public LiveReplayCommandManager(BattleManager BattleManager) : this()
        {
            this.BattleManager = BattleManager;
        }

        internal void StoreCommands(List<Command> Commands)
        {
            this.BufferedCommands.AddRange(Commands);
        }

        internal void Tick()
        {
            this.Commands.AddRange(this.BufferedCommands);
            this.BufferedCommands.Clear();
        }
    }
    */
}