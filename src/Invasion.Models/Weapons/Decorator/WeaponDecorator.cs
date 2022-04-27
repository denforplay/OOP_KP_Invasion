using Invasion.Engine;
using Invasion.Engine.Interfaces;
using Invasion.Models.Interfaces;
using System.Numerics;

namespace Invasion.Models.Weapons.Decorator;

public class WeaponBaseDecorator : WeaponBase
{
    public void SetWeapon(WeaponBase weaponBase)
    {
        DecoratedWeaponBase = weaponBase;
    }

    public override float ReloadTime { get => DecoratedWeaponBase.ReloadTime; }
    protected WeaponBase DecoratedWeaponBase;
    public WeaponBase Weapon => DecoratedWeaponBase;

    public override GameObject Parent => Weapon.Parent;

    public override int Damage => Weapon.Damage;
    public override float Speed => Weapon.Speed;

    public override void GiveDamage(IHealthable healthable)
    {
        healthable.TakeDamage(Damage);
    }

    public override void Attack(Vector2 direction)
    {
        DecoratedWeaponBase.Attack(direction);
    }

    public override void Update()
    {
        DecoratedWeaponBase.Update();
    }

    public WeaponBaseDecorator(WeaponBase weaponBase, List<IComponent> components, Layer layer = Layer.Weapon) : base(components, layer)
    {
        DecoratedWeaponBase = weaponBase;
    }
}