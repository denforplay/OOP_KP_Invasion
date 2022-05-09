using Invasion.Engine;
using Invasion.Engine.Interfaces;
using Invasion.Models.Configurations;

namespace Invasion.Models.Weapons.Melee;

/// <summary>
/// Represents knife 
/// </summary>
public class Knife : MeleeBase
{
    /// <summary>
    /// Knife constructor
    /// </summary>
    /// <param name="parent">Knife parent</param>
    /// <param name="configuration">Weapon configuration</param>
    /// <param name="components">Components</param>
    /// <param name="layer">Layer</param>
    public Knife(GameObject parent, WeaponConfiguration configuration, List<IComponent> components, Layer layer = Layer.Weapon) : base(parent, configuration, components, layer)
    {
    }
}