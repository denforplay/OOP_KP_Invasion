using Invasion.Engine;
using Invasion.Engine.Interfaces;

namespace Invasion.Models.Weapons.Decorator
{
    public class HigherFireRateWeponDecorator : WeaponBaseDecorator
    {
        public override float Speed => DecoratedWeaponBase.Speed * 2f;

        public HigherFireRateWeponDecorator(WeaponBase weaponBase, List<IComponent> components, Layer layer = Layer.Weapon) : base(weaponBase, components, layer)
        {
        }
    }
}
