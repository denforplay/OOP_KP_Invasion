using Invasion.Engine;
using Invasion.Models.Enemies;

namespace Invasion.View.Factories.EnemyFactories.Concretes;

public class ShootingEnemyFactory : IEnemyFactory
{
    public EnemyBase CreateEnemy()
    {
        var enemy = new ShootingEnemy(null, Layer.Enemy);
        return enemy;
    }
}