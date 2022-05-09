using Invasion.Core;
using Invasion.Models.System.Base;
using Invasion.Models.Weapons.Firearms.Bullets;

namespace Invasion.Models.Systems;

/// <summary>
/// Class represents system who'll work with bullets
/// </summary>
public class BulletSystem : SystemBase<BulletBase>
{
    /// <summary>
    /// Start to work bullet
    /// </summary>
    /// <param name="bullet">Bullet model to work with</param>
    public void Work(BulletBase bullet)
    {
        Entity<BulletBase> entity = new Entity<BulletBase>(bullet);
        Work(entity);
    }
    
    public override void Update(float deltaTime)
    {
    }
}