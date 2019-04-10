namespace GL.Servers.CoC.Logic.Worker
{
    using System.Collections.Generic;

    using GL.Servers.CoC.Core;
    using GL.Servers.CoC.Logic;

    internal class WorkerManager
    {
        internal int WorkerCount;

        internal List<GameObject> GameObjects;

        /// <summary>
        /// Gets a value indicating the free workers count.
        /// </summary>
        internal int FreeWorkers
        {
            get
            {
                return this.WorkerCount - this.GameObjects.Count;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkerManager"/> class.
        /// </summary>
        public WorkerManager()
        {
            this.GameObjects = new List<GameObject>(5);
        }

        /// <summary>
        /// Allocates one worker to a task.
        /// </summary>
        internal void AllocateWorker(GameObject GameObject)
        {
            if (this.GameObjects.Contains(GameObject))
            {
                Logging.Error(this.GetType(), "AllocateWorker() called twice for same target!");
                return;
            }

            this.GameObjects.Add(GameObject);
        }

        /// <summary>
        /// Deallocates a worker to her task.
        /// </summary>
        internal void DeallocateWorker(GameObject GameObject)
        {
            if (!this.GameObjects.Remove(GameObject))
            {
                Logging.Error(this.GetType(), "DeallocateWorker() - GameObject is not in array!");
            }
        }

        /// <summary>
        /// Gets a value indicating the shortest task go.
        /// </summary>
        internal GameObject GetShortestTaskGO()
        {
            if (this.GameObjects.Count > 0)
            {
                Task Task = new Task();

                this.GameObjects.ForEach(Construction =>
                {
                    int RemainingTime = -1;

                    switch (Construction.Type)
                    {
                        case 0:
                        {
                            Building Building = (Building) Construction;

                            if (!Building.Constructing)
                            {
                                Logging.Error(this.GetType(), "GetShortestTaskGO() : Worker allocated to building with remaining construction time 0");
                                break;
                            }

                            RemainingTime = Building.RemainingConstructionTime;

                            break;
                        }

                        case 3:
                        {
                            Obstacle Obstacle = (Obstacle) Construction;

                            if (!Obstacle.ClearingOnGoing)
                            {
                                Logging.Error(this.GetType(), "GetShortestTaskGO() : Worker allocated to obstacle with remaining clearing time 0");
                                break;
                            }

                            RemainingTime = Obstacle.RemainingClearingTime;

                            break;
                        }
                    }

                    if (RemainingTime != -1)
                    {
                        if (Task.RemainingSeconds > RemainingTime)
                        {
                            Task.GameObject = Construction;
                            Task.RemainingSeconds = RemainingTime;
                        }
                    }
                });

                return Task.GameObject;
            }

            return null;
        }

        private struct Task
        {
            internal GameObject GameObject;
            internal int RemainingSeconds;
        }
    }
}