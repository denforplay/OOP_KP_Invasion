using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Models.Enemies;
using Invasion.View.Factories.Base;

namespace Invasion.View.Factories.EnemyFactories;

public class EnemyFactory : GameObjectViewFactoryBase<EnemyBase>
{
    private string _enemySpriteName = "shootingEnemy.png";
    private IEnemyFactory _factory;
    
    public EnemyFactory(DX2D dx2D, IEnemyFactory factory) : base(dx2D)
    {
        _factory = factory;
    }
}