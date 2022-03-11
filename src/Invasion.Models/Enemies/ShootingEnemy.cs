using Invasion.Core.Interfaces;
using Invasion.Engine;

namespace Invasion.Models.Enemies;

public class ShootingEnemy : EnemyBase
{
    public ShootingEnemy(List<IComponent> components, Layer layer = Layer.Default) : base(components, layer)
    {
    }
}