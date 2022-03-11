using Invasion.Models.Enemies;

namespace Invasion.View.Factories.EnemyFactories;

public interface IEnemyFactory
{
    public EnemyBase CreateEnemy();
}