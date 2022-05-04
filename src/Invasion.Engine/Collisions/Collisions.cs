namespace Invasion.Models.Collisions
{
    /// <summary>
    /// Represents list of collisions
    /// </summary>
    public class Collisions
    {
        private readonly List<(object, object)> _collisionPairs = new List<(object, object)>();

        /// <summary>
        /// Collision pairs
        /// </summary>
        public IEnumerable<(object, object)> CollisionPairs => _collisionPairs;

        /// <summary>
        /// Try bind to collision two objects
        /// </summary>
        /// <param name="a">First object</param>
        /// <param name="b">Second object</param>
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
