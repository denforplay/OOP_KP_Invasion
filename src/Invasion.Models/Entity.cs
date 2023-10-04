namespace Invasion.Models
{
    /// <summary>
    /// Represents entity
    /// </summary>
    /// <typeparam name="T">Type of entity model</typeparam>
    public sealed class Entity<T>
    {
        private readonly T _entity;

        /// <summary>
        /// Entity model
        /// </summary>
        public T GetEntity => _entity;

        /// <summary>
        /// Entity constructor
        /// </summary>
        /// <param name="entity">Entity model</param>
        public Entity(T entity)
        {
            _entity = entity;
        }
    }
}
