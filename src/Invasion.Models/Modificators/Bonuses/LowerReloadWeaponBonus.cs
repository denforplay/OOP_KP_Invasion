﻿
using Invasion.Engine;
using Invasion.Engine.Interfaces;
using Invasion.Models.Configurations;
using Invasion.Models.Weapons.Decorator;

namespace Invasion.Models.Modificators.Bonuses
{
    public class LowerReloadWeaponBonus : BonusBase
    {
        public LowerReloadWeaponBonus(List<IComponent> components, ModificatorConfiguration configuration, Layer layer = Layer.Modificator) : base(components, configuration, layer)
        {
        }

        protected async override void Apply(WeaponBaseDecorator weaponBase)
        {
            var previousWeapon = weaponBase.Weapon;
            weaponBase.SetWeapon(new LowerReloadWeaponDecorator(previousWeapon, previousWeapon.Components));
            await Task.Delay(_configuration.Duration);
            weaponBase.SetWeapon(previousWeapon);
        }
    }
}
