using Invasion.Engine;
using Invasion.Engine.Interfaces;
using Invasion.Models.Configurations;
using Invasion.Models.Factories;
using Invasion.Models.Systems;
using Invasion.Models.Weapons.Firearms.Base;
using Invasion.Models.Weapons.Firearms.Bullets;

namespace Invasion.Models.Weapons.Firearms;

public class Pistol : FirearmBase
{
    public Pistol(IModelFactory<BulletBase> bulletFactory, BulletSystem bulletSystem, WeaponConfiguration configuration, List<IComponent> components, GameObject parent) : base(bulletFactory, bulletSystem, configuration, components, parent)
    {
    }
}