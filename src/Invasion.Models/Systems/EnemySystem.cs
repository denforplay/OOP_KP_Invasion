using Invasion.Models.Enemies;
using Invasion.Models.System.Base;

namespace Invasion.Models.Systems;

/// <summary>
/// Class represents enemy system
/// </summary>
public class EnemySystem : SystemBase<EnemyBase>
{
    /// <summary>
    /// Method to start work new entity based on enemy model
    /// </summary>
    /// <param name="enemy"></param>
    public void Work(EnemyBase enemy)
    {
        Entity<EnemyBase> enemyEntity = new Entity<EnemyBase>(enemy);
        Work(enemyEntity);
    }
    
    public override void Update(float deltaTime)
    {
    }
}