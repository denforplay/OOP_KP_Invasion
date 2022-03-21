using Invasion.Core.Interfaces;
using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Models.Collisions;
using Invasion.Models.Systems;
using Invasion.Models.Weapons.Firearms.Base;

namespace Invasion.Models.Weapons.Firearms;

public class Pistol : FirearmBase
{
    public Pistol(CollisionController collisionController, DX2D dx2D, BulletSystem bulletSystem, List<IComponent> components, GameObject parent, Layer layer = Layer.Default) : base(collisionController, dx2D, bulletSystem, components, parent, layer)
    {
    }
}