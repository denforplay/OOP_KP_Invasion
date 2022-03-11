using Invasion.Core.Interfaces;
using Invasion.Engine;

namespace Invasion.Models.Weapons.Firearms.Bullets;

public class DefaultBullet : BulletBase
{
    public DefaultBullet(List<IComponent> components = null, Layer layer = Layer.Bullet) : base(components, layer)
    {
    }
}