using Invasion.Engine;
using Invasion.Engine.Interfaces;
using Invasion.Models.Configurations;
using Invasion.Models.Factories;
using Invasion.Models.Systems;
using Invasion.Models.Weapons.Firearms.Base;
using Invasion.Models.Weapons.Firearms.Bullets;

namespace Invasion.Models.Weapons.Firearms;

/// <summary>
/// Represents pistol
/// </summary>
public class Pistol : FirearmBase
{
    /// <summary>
    /// Pistol constructor
    /// </summary>
    /// <param name="bulletFactory">Bullet factory</param>
    /// <param name="bulletSystem">Bullet system</param>
    /// <param name="configuration">Weapon configuration</param>
    /// <param name="components">Components</param>
    /// <param name="parent">Parent</param>
    public Pistol(IModelFactory<BulletBase> bulletFactory, BulletSystem bulletSystem, WeaponConfiguration configuration, List<IComponent> components, GameObject parent) : base(bulletFactory, bulletSystem, configuration, components, parent)
    {
    }
}