using Invasion.Engine;
using Invasion.Engine.Interfaces;

namespace Invasion.Models.Weapons.Decorator;

public class FasterWeaponBaseDecorator : WeaponBaseDecorator
{
    public override float ReloadTime => DecoratedWeaponBase.ReloadTime/100f;

    public FasterWeaponBaseDecorator(WeaponBase weaponBase, List<IComponent> components, Layer layer = Layer.Weapon) : base(weaponBase, components, layer)
    {
    }
}