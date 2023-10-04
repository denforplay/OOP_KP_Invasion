using Invasion.Core;
using Invasion.Engine.Interfaces;
using Invasion.Models.Enemies;
using Invasion.Models.Modificators.Bonuses;
using Invasion.Models.Modificators.Traps;
using Invasion.Models.Systems;
using Invasion.Models.Weapons;
using Invasion.Models.Weapons.Decorator;
using Invasion.Models.Weapons.Firearms.Bullets;
using Invasion.Models.Weapons.Melee;

namespace Invasion.Models
{
    /// <summary>
    /// Represents collision records
    /// </summary>
    public class CollisionRecords
    {
        private BulletSystem _bulletSystem;
        private EnemySystem _enemySystem;
        private ModificatorSystem _modificatorSystem;

        /// <summary>
        /// Collision records constructor
        /// </summary>
        /// <param name="bulletSystem">Bullet system</param>
        /// <param name="enemySystem">Enemy system</param>
        /// <param name="modificatorSystem">Modificator system</param>
        public CollisionRecords(BulletSystem bulletSystem, EnemySystem enemySystem, ModificatorSystem modificatorSystem)
        {
            _modificatorSystem = modificatorSystem;
            _bulletSystem = bulletSystem;
            _enemySystem = enemySystem;
        }
        
        /// <summary>
        /// Returns collide values
        /// </summary>
        /// <returns>List of collision records</returns>
        public IEnumerable<IRecord> StartCollideValues()
        {
            yield return IfCollided<BulletBase, EnemyBase>((bullet, enemy) =>
            {
                if (bullet.Parent.Parent is Player && !bullet.IsUsed)
                {
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
            
            yield return IfCollided<WeaponBaseDecorator, EnemyBase>((weaponDecorator, enemy) =>
            {
                if (weaponDecorator.Parent is Player)
                {
                    var weapon = weaponDecorator.Weapon;

                    while (weapon is WeaponBaseDecorator otherDecorator)
                    {
                        weapon = otherDecorator.Weapon;
                    }

                    if (weapon is MeleeBase meleeWeapon)
                    {
                        if (meleeWeapon.IsAttack)
                        {
                            meleeWeapon.GiveDamage(enemy);
                        }
                        if (enemy.CurrentHealthPoints <= 0)
                            _enemySystem.StopWork(enemy);
                    }
                }
            });

            yield return IfCollided<WeaponBaseDecorator, Player>((weaponDecorator, player) =>
            {
                if (weaponDecorator.Parent is EnemyBase)
                {
                    var weapon = weaponDecorator.Weapon;

                    while (weapon is WeaponBaseDecorator otherDecorator)
                    {
                        weapon = otherDecorator.Weapon;
                    }

                    if (weapon is MeleeBase meleeWeapon)
                    {
                        if (meleeWeapon.IsAttack)
                        {
                            meleeWeapon.GiveDamage(player);
                        }

                    }
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

            yield return IfCollided<TrapBase, Player>((modificator, player) =>
            {
                modificator.Apply(player);
                _modificatorSystem.StopWork(modificator);
            });

            yield return IfCollided<EnemyBase, Player>((enemy, player) =>
            {
                player.OnDestroy();
            });
        }

        private IRecord IfCollided<T1, T2>(Action<T1, T2> action)
        {
            return new Record<T1, T2>(action);
        }
    }
}