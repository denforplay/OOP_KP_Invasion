using Invasion.Core.Abstracts;

namespace Invasion.Core
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
