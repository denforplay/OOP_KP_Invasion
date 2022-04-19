using Invasion.Models;
using Invasion.Models.Collisions;
using Invasion.Models.Systems;
using Invastion.CompositeRoot.Base;

namespace Invastion.CompositeRoot.Implementations
{
    /// <summary>
    /// Composite root for collisions
    /// </summary>
    public class CollisionsCompositeRoot : ICompositeRoot
    {
        private CollisionController _controller;
        private CollisionRecords _records;
        public CollisionController Controller => _controller;
        private BulletSystem _bulletSystem;
        private EnemySystem _enemySystem;
        private ModificatorSystem _modificatorSystem;

        /// <summary>
        /// Collisions composite root controller
        /// </summary>
        /// <param name="bulletSystem">Bullet system</param>
        /// <param name="enemySystem">Enemy system</param>
        /// <param name="modificatorSystem">Modificator system</param>
        public CollisionsCompositeRoot(BulletSystem bulletSystem, EnemySystem enemySystem, ModificatorSystem modificatorSystem)
        {
            _modificatorSystem = modificatorSystem;
            _enemySystem = enemySystem;
            _bulletSystem = bulletSystem;
            Compose();
        }
        
        public void Compose()
        {
            _records = new CollisionRecords(_bulletSystem, _enemySystem, _modificatorSystem);
            _controller = new CollisionController(_records.StartCollideValues);
        }

        /// <summary>
        /// Update collisions
        /// </summary>
        public void Update()
        {
            _controller.Update();
        }

        public void Dispose()
        {
        }
    }
}
