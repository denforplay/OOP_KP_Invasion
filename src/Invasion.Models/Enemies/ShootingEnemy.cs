using Invasion.Engine;
using Invasion.Engine.Interfaces;
using Invasion.Models.Configurations;

namespace Invasion.Models.Enemies;

public class ShootingEnemy : EnemyBase
{
    public ShootingEnemy(List<IComponent> components, EnemyConfiguration enemyConfiguration, Layer layer = Layer.Enemy) : base(components, enemyConfiguration, layer)
    {
    }
}