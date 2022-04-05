
using Invasion.Engine.Interfaces;

namespace Invasion.Models.Collisions
{
    public class CollisionController
    {
        private Collisions _collisions = new Collisions();
        private readonly Func<IEnumerable<IRecord>> _startCollideRecordsProvider;

        public CollisionController(Func<IEnumerable<IRecord>> startCollideRecordsProvider)
        {
            _startCollideRecordsProvider = startCollideRecordsProvider;
        }

        public void Update()
        {
            foreach (var pair in _collisions.CollisionPairs)
                TryCollide(pair);

            _collisions = new Collisions();
        }

        public void TryCollide((object, object) pair)
        {
            IEnumerable<IRecord> records = _startCollideRecordsProvider?.Invoke().Where(record => record.IsTarget(pair));

            foreach (var record in records)
                ((dynamic)record).Do((dynamic)pair.Item1, (dynamic)pair.Item2);
        }
    }
}
