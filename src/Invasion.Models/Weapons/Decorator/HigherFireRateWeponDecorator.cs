using Invasion.Engine;
using Invasion.Engine.Interfaces;
using Invasion.Models.Configurations;

namespace Invasion.Models.Weapons.Decorator
{
    public class HigherFireRateWeponDecorator : WeaponBaseDecorator
    {
        public HigherFireRateWeponDecorator(WeaponBase other, WeaponConfiguration configuration, List<IComponent> components, Layer layer = Layer.Weapon) : base(other, configuration, components, layer)
        {
        }

        public override float Speed => DecoratedWeaponBase.Speed * 2f;


    }
}
