namespace Invasion.Models.System.Base
{
    /// <summary>
    /// Represents base system to work with entity
    /// </summary>
    /// <typeparam name="T">Type of objects to work with</typeparam>
    public abstract class SystemBase<T>
    {
        private List<Entity<T>> _entities = new List<Entity<T>>();

        /// <summary>
        /// Entities
        /// </summary>
        public IEnumerable<Entity<T>> Entities => _entities;

        /// <summary>
        /// Delegate called when system start to work new entity
        /// </summary>
        public Action<Entity<T>> OnStart;

        /// <summary>
        /// Delegate called when system stop to work some entity
        /// </summary>
        public Action<Entity<T>> OnEnd;

        /// <summary>
        /// Called to update system
        /// </summary>
        /// <param name="deltaTime">Delta time</param>
        public abstract void Update(float deltaTime);

        protected void Work(Entity<T> entity)
        {
            _entities.Add(entity);
            OnStart?.Invoke(entity);
        }

        protected void Stop(Entity<T> entity)
        {
            _entities.Remove(entity);
            OnEnd?.Invoke(entity);
        }
        
        /// <summary>
        /// Stop to work entity
        /// </summary>
        /// <param name="entityModel">Entity model</param>
        public void StopWork(T entityModel)
        {
            var entity = _entities.Find(x => x.GetEntity.Equals(entityModel));
            if (entity is not null)
                Stop(entity);
        }

        /// <summary>
        /// Stop all entities
        /// </summary>
        public void StopAll()
        {
            var entitiesCopy = new List<Entity<T>>(_entities);
            entitiesCopy.ForEach(Stop);
        }
    }
}
