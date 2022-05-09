using Invasion.Engine;
using Invasion.Engine.Interfaces;
using Invasion.Models.Configurations;
using Invasion.Models.Weapons.Decorator;

namespace Invasion.Models.Modificators.Bonuses
{
    /// <summary>
    /// Class represents higher fire rate bonus
    /// </summary>
    public class HigherFIreRateWeaponBonus : BonusBase
    {
        /// <summary>
        /// Higher fire rate weapon bonus constructor
        /// </summary>
        /// <param name="components">Components</param>
        /// <param name="configuration">Configuration</param>
        /// <param name="layer">Layer</param>
        public HigherFIreRateWeaponBonus(List<IComponent> components, ModificatorConfiguration configuration, Layer layer = Layer.Modificator) : base(components, configuration, layer)
        {
        }

        protected async override void Apply(WeaponBaseDecorator weaponBase)
        {
            var previousWeapon = weaponBase.Weapon;
            weaponBase.SetWeapon(new HigherFireRateWeponDecorator(previousWeapon, previousWeapon.Configuration, previousWeapon.Components));
            await Task.Delay(_configuration.Duration);
            weaponBase.SetWeapon(previousWeapon);
        }
    }
}
