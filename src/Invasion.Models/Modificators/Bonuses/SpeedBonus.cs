using Invasion.Engine;
using Invasion.Engine.Interfaces;
using Invasion.Models.Weapons;
using Invasion.Models.Weapons.Decorator;

namespace Invasion.Models.Modificators.Bonuses;

public class SpeedBonus : BonusBase
{
    public SpeedBonus(List<IComponent> components, Layer layer = Layer.Modificator) : base(components, layer)
    {
    }

    protected async override void Apply(WeaponBase weaponBase)
    {
        if (weaponBase is WeaponBaseDecorator decorator)
        {
            var previousWeapon = decorator.Weapon;
            decorator.SetWeapon(new FasterWeaponBaseDecorator(previousWeapon, previousWeapon.Components));
            await Task.Delay(3000);
            decorator.SetWeapon(previousWeapon);
        }
    }
}