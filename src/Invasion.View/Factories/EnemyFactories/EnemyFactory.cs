using Invasion.Engine;
using Invasion.Models.Enemies;
using Invasion.View.Factories.Base;

namespace Invasion.View.Factories.EnemyFactories;

public class EnemyFactory : GameObjectViewFactoryBase<EnemyBase>
{
    private IEnemyFactory _factory;
    
    public EnemyFactory(DX2D dx2D, IEnemyFactory factory) : base(dx2D)
    {
        _factory = factory;
    }

    protected override GameObject GetEntity(EnemyBase entity)
    {
        return _factory.CreateEnemy();
    }
}