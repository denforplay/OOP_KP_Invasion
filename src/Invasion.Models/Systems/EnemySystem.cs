using Invasion.Models.Enemies;
using Invasion.Models.System.Base;

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