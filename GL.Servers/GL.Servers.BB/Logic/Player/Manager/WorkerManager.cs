namespace GL.Servers.BB.Logic.Manager
{
    using System.Collections.Generic;
    using GL.Servers.BB.Core;
    using GL.Servers.BB.Logic.GameObject;

    internal class WorkerManager
    {
        internal int TotalWorkers = 1;
        internal List<GameObject> GameObjects;

        internal int FreeWorkers
        {
            get
            {
                return this.TotalWorkers - this.GameObjects.Count;
            }
        }

        public WorkerManager()
        {
            this.GameObjects = new List<GameObject>(5);
        }

        internal void AllocateWorker(GameObject GameObject)
        {
            if (this.GameObjects.Contains(GameObject))
            {
                Logging.Error(this.GetType(), "AllocateWorker() called twice for same target!");
            }
        }

        internal void DeallocateWorker(GameObject GameObject)
        {
            this.GameObjects.Remove(GameObject);
        }
    }
}