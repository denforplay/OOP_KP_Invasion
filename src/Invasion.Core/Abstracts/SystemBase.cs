namespace Invasion.Core.Abstracts
{
    public abstract class SystemBase<T>
    {
        private List<Entity<T>> _entities = new List<Entity<T>>();

        public IEnumerable<Entity<T>> Entities => _entities;

        public Action<Entity<T>> OnStart;
        public Action<Entity<T>> OnEnd;

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
        
        public void StopWork(T entityModel)
        {
            var entity = _entities.Find(x => x.GetEntity.Equals(entityModel));
            if (entity is not null)
                Stop(entity);
        }

        public void StopAll()
        {
            var entitiesCopy = new List<Entity<T>>(_entities);
            entitiesCopy.ForEach(Stop);
        }
    }
}
