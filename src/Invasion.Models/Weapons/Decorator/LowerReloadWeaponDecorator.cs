using Invasion.Engine;
using Invasion.Engine.Interfaces;

namespace Invasion.Models.Weapons.Decorator
{
    public class LowerReloadWeaponDecorator : WeaponBaseDecorator
    {
        public override float ReloadTime => DecoratedWeaponBase.ReloadTime * 1.5f;

        public LowerReloadWeaponDecorator(WeaponBase weaponBase, List<IComponent> components, Layer layer = Layer.Weapon) : base(weaponBase, components, layer)
        {
        }
    }
}
