using Invasion.Engine;
using Invasion.Engine.Interfaces;
using Invasion.Models.Configurations;

namespace Invasion.Models.Weapons.Decorator
{
    public class LowerReloadWeaponDecorator : WeaponBaseDecorator
    {
        public LowerReloadWeaponDecorator(WeaponBase other, WeaponConfiguration configuration, List<IComponent> components, Layer layer = Layer.Weapon) : base(other, configuration, components, layer)
        {
        }

        public override float ReloadTime => DecoratedWeaponBase.ReloadTime * 1.5f;

    }
}
