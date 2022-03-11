using Invasion.Core;
using Invasion.Core.Abstracts;
using Invasion.Models.Enemies;

namespace Invasion.Models.Systems;

public class EnemySystem : SystemBase<EnemyBase>
{
    public void Work(EnemyBase enemy)
    {
        Entity<EnemyBase> enemyEntity = new Entity<EnemyBase>(enemy);
        Work(enemyEntity);
    }
    
    public override void Update(float deltaTime)
    {
    }
}