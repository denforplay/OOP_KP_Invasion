using Invasion.Models.Systems;
using Invasion.View.Factories.BulletFactories;
using Invasion.View.Factories.EnemyFactories;
using Invastion.CompositeRoot.Base;

namespace Invastion.CompositeRoot.Implementations;

public class EnemyCompositeRoot : ICompositeRoot
{
    private EnemySystem _enemySystem;
    private BulletSystem _bulletSystem;
    private EnemyFactory _enemyFactory;
    private DefaultBulletFactory _bulletFactory;
    public void Compose()
    {
    }
}