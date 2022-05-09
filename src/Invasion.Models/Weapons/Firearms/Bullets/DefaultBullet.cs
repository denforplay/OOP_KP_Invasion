using Invasion.Engine;
using Invasion.Engine.Interfaces;

namespace Invasion.Models.Weapons.Firearms.Bullets;

/// <summary>
/// Class represents base bullet
/// </summary>
public class DefaultBullet : BulletBase
{
    /// <summary>
    /// Default bullet constructor
    /// </summary>
    /// <param name="components">Components</param>
    /// <param name="layer">Layer</param>
    public DefaultBullet(List<IComponent> components = null, Layer layer = Layer.Bullet) : base(components, layer)
    {
    }
}