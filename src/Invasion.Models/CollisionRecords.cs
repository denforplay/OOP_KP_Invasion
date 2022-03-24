using Invasion.Core;
using Invasion.Core.Interfaces;
using Invasion.Engine;
using Invasion.Models.Enemies;
using Invasion.Models.Modificators;
using Invasion.Models.Modificators.Bonuses;
using Invasion.Models.Systems;
using Invasion.Models.Weapons;
using Invasion.Models.Weapons.Decorator;
using Invasion.Models.Weapons.Firearms.Bullets;
using Invasion.Models.Weapons.Melee;

namespace Invasion.Models
{
    public class CollisionRecords
    {
        private BulletSystem _bulletSystem;
        private EnemySystem _enemySystem;
        private ModificatorSystem _modificatorSystem;

        public CollisionRecords(BulletSystem bulletSystem, EnemySystem enemySystem, ModificatorSystem modificatorSystem)
        {
            _modificatorSystem = modificatorSystem;
            _bulletSystem = bulletSystem;
            _enemySystem = enemySystem;
        }
        
        public IEnumerable<IRecord> StartCollideValues()
        {
            yield return IfCollided<BulletBase, EnemyBase>((bullet, enemy) =>
            {
                if (bullet.Parent.Parent is Player && !bullet.IsUsed)
                {
                    Console.WriteLine("Damage" + enemy);
                    bullet.Parent.GiveDamage(enemy);
                    _bulletSystem.StopWork(bullet);
                    bullet.IsUsed = true;
                    if (enemy.CurrentHealthPoints <= 0)
                        _enemySystem.StopWork(enemy);
                }
            });
            
            yield return IfCollided<BulletBase, Player>((bullet, player) =>
            {
                if (bullet.Parent.Parent is EnemyBase && !bullet.IsUsed)
                {
                    bullet.IsUsed = true;
                    _bulletSystem.StopWork(bullet);
                    bullet.Parent.GiveDamage(player);
                }
            });

            yield return IfCollided<BulletBase, Border>((bullet, border) =>
            {
                _bulletSystem.StopWork(bullet);
            });
            
            //wtf do with that????
            yield return IfCollided<WeaponBaseDecorator, EnemyBase>((weaponDecorator, enemy) =>
            {
                if (weaponDecorator.Weapon is MeleeBase meleeWeapon)
                {
                    if (meleeWeapon.IsAttack)
                    {
                        meleeWeapon.GiveDamage(enemy);
                    }
                    if (enemy.CurrentHealthPoints <= 0)
                        _enemySystem.StopWork(enemy);
                }
                
            });
            
            yield return IfCollided<BonusBase, WeaponBase>((modificator, modificatedObject) =>
            {
                modificator.Apply(modificatedObject);
                if (modificator.IsApplied)
                {
                    _modificatorSystem.StopWork(modificator);
                }
            });
        }

        private IRecord IfCollided<T1, T2>(Action<T1, T2> action)
        {
            return new Record<T1, T2>(action);
        }
    }
}