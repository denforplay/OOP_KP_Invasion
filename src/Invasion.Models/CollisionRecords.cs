using Invasion.Core;
using Invasion.Core.Interfaces;
using Invasion.Models.Enemies;
using Invasion.Models.Systems;
using Invasion.Models.Weapons.Firearms.Bullets;
using Invasion.Models.Weapons.Melee;

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
                if (bullet.Parent.Parent is Player)
                {
                    bullet.Parent.GiveDamage(enemy);
                    _bulletSystem.StopWork(bullet);
                    if (enemy.CurrentHealthPoints <= 0)
                        _enemySystem.StopWork(enemy);
                }
            });
            
            yield return IfCollided<BulletBase, Player>((bullet, player) =>
            {
                if (bullet.Parent.Parent is EnemyBase)
                {
                    _bulletSystem.StopWork(bullet);
                    bullet.Parent.GiveDamage(player);
                }
            });

            yield return IfCollided<BulletBase, Border>((bullet, border) =>
            {
                _bulletSystem.StopWork(bullet);
            });
            
            yield return IfCollided<MeleeBase, EnemyBase>((meleeWeapon, enemy) =>
            {
                if (meleeWeapon.IsAttack)
                {
                    meleeWeapon.GiveDamage(enemy);
                }
                if (enemy.CurrentHealthPoints <= 0)
                    _enemySystem.StopWork(enemy);
            });
        }

        private IRecord IfCollided<T1, T2>(Action<T1, T2> action)
        {
            return new Record<T1, T2>(action);
        }
    }
}