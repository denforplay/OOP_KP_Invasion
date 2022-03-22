using Invasion.Core.Interfaces;
using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Models.Collisions;
using Invasion.Models.Factories;
using Invasion.Models.Systems;
using Invasion.Models.Weapons.Firearms.Base;
using Invasion.Models.Weapons.Firearms.Bullets;

namespace Invasion.Models.Weapons.Firearms;

public class Pistol : FirearmBase
{
    public Pistol(IModelFactory<BulletBase> bulletFactory, BulletSystem bulletSystem, List<IComponent> components, GameObject parent) : base(bulletFactory, bulletSystem, components, parent)
    {
    }
}