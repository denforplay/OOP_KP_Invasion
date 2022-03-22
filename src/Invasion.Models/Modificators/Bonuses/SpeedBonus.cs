using Invasion.Core.Interfaces;
using Invasion.Engine;
using Invasion.Models.Weapons;
using Invasion.Models.Weapons.Decorator;

namespace Invasion.Models.Modificators.Bonuses;

public class SpeedBonus : BonusBase
{
    public SpeedBonus(List<IComponent> components, Layer layer = Layer.Modificator) : base(components, layer)
    {
    }

    protected async override void Apply(IWeapon weapon)
    {
        var previousWeapon = weapon;
        weapon = new FasterWeaponDecorator(weapon);
        await Task.Delay(3000);
        weapon = previousWeapon;
    }
}