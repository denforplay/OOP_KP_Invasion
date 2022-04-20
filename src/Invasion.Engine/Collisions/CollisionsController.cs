using Invasion.Engine.Interfaces;

namespace Invasion.Models.Collisions
{
    /// <summary>
    /// Represents collision controller
    /// </summary>
    public class CollisionController
    {
        private Collisions _collisions = new Collisions();
        private readonly Func<IEnumerable<IRecord>> _startCollideRecordsProvider;

        /// <summary>
        /// Collision controller constructor
        /// </summary>
        /// <param name="startCollideRecordsProvider">Start collider records</param>
        public CollisionController(Func<IEnumerable<IRecord>> startCollideRecordsProvider)
        {
            _startCollideRecordsProvider = startCollideRecordsProvider;
        }

        /// <summary>
        /// Update collisions
        /// </summary>
        public void Update()
        {
            foreach (var pair in _collisions.CollisionPairs)
                TryCollide(pair);

            _collisions = new Collisions();
        }

        /// <summary>
        /// Try collide collision
        /// </summary>
        /// <param name="pair">Collisions pair</param>
        public void TryCollide((object, object) pair)
        {
            IEnumerable<IRecord> records = _startCollideRecordsProvider?.Invoke().Where(record => record.IsTarget(pair));

            foreach (var record in records)
                ((dynamic)record).Do((dynamic)pair.Item1, (dynamic)pair.Item2);
        }
    }
}
