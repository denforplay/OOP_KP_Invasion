using Invasion.Engine;
using Invasion.Engine.Interfaces;
using Invasion.Models.Configurations;

namespace Invasion.Models.Weapons.Melee;

public class Knife : MeleeBase
{
    public Knife(GameObject parent, WeaponConfiguration configuration, List<IComponent> components, Layer layer = Layer.Weapon) : base(parent, configuration, components, layer)
    {
    }
}