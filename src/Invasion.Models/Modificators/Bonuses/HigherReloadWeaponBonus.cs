using Invasion.Engine;
using Invasion.Engine.Interfaces;
using Invasion.Models.Configurations;
using Invasion.Models.Weapons.Decorator;

namespace Invasion.Models.Modificators.Bonuses;

/// <summary>
/// Represents higher reload weapon bonus
/// </summary>
public class HigherReloadWeaponBonus : BonusBase
{
    /// <summary>
    /// Higher reload weapon bonus constructor
    /// </summary>
    /// <param name="components">Components</param>
    /// <param name="configuration">Configuration</param>
    /// <param name="layer">Layer</param>
    public HigherReloadWeaponBonus(List<IComponent> components, ModificatorConfiguration configuration, Layer layer = Layer.Modificator) : base(components, configuration, layer)
    {
    }

    protected async override void Apply(WeaponBaseDecorator weaponBase)
    {
        var previousWeapon = weaponBase.Weapon;
        weaponBase.SetWeapon(new FasterWeaponBaseDecorator(previousWeapon, previousWeapon.Configuration, previousWeapon.Components));
        await Task.Delay(_configuration.Duration);
        weaponBase.SetWeapon(previousWeapon);
    }
}