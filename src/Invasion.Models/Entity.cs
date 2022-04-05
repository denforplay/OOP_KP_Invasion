namespace Invasion.Models
{
    public sealed class Entity<T>
    {
        private readonly T _entity;
        public T GetEntity => _entity;
        public Entity(T entity)
        {
            _entity = entity;
        }
    }
}
