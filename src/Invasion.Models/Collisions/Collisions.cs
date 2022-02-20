namespace Invasion.Models.Collisions
{
    public class Collisions
    {
        private readonly List<(object, object)> _collisionPairs = new List<(object, object)>();

        public IEnumerable<(object, object)> CollisionPairs => _collisionPairs;

        public void TryBind(object a, object b)
        {
            foreach (var (left, right) in _collisionPairs)
            {
                if ((left == a && right == b) || left == b && right == a)
                    return;
            }

            _collisionPairs.Add((a, b));
        }
    }
}
