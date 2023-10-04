using Invasion.Engine;
using Invasion.Engine.Interfaces;
using Invasion.Models.Configurations;

namespace Invasion.Models.Enemies;

/// <summary>
/// Represents shooting enemy who uses shooting weapon
/// </summary>
public class ShootingEnemy : EnemyBase
{
    public ShootingEnemy(List<IComponent> components, EnemyConfiguration enemyConfiguration, Layer layer = Layer.Enemy) : base(components, enemyConfiguration, layer)
    {
    }
}