using Invasion.Core.Interfaces;
using Invasion.Engine;

namespace Invasion.Models.Weapons.Decorator;

public class FasterWeaponBaseDecorator : WeaponBaseDecorator
{
    public override float ReloadTime => DecoratedWeaponBase.ReloadTime/100f;

    public FasterWeaponBaseDecorator(WeaponBase weaponBase, List<IComponent> components, Layer layer = Layer.Weapon) : base(weaponBase, components, layer)
    {
    }
}