using Invasion.Core;
using Invasion.Core.Interfaces;
using Invasion.Engine;
using Invasion.Models.Enemies;
using Invasion.Models.Systems;
using Invasion.Models.Weapons.Firearms.Bullets;
using SharpDX;

namespace Invasion.Models
{
    public class CollisionRecords
    {
        private BulletSystem _bulletSystem;
        private EnemySystem _enemySystem;

        public CollisionRecords(BulletSystem bulletSystem, EnemySystem enemySystem)
        {
            _bulletSystem = bulletSystem;
            _enemySystem = enemySystem;
        }
        
        public IEnumerable<IRecord> StartCollideValues()
        {
            yield return IfCollided<BulletBase, EnemyBase>((bullet, enemy) =>
            {
                if (bullet.Parent is Player)
                {
                    _bulletSystem.StopWork(bullet);
                }
            });
            
            yield return IfCollided<BulletBase, Player>((bullet, player) =>
            {
                if (bullet.Parent is EnemyBase)
                {
                    _bulletSystem.StopWork(bullet);
                }
            });
        }

        private IRecord IfCollided<T1, T2>(Action<T1, T2> action)
        {
            return new Record<T1, T2>(action);
        }
    }
}