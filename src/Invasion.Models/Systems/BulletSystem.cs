using Invasion.Core;
using Invasion.Models.System.Base;
using Invasion.Models.Weapons.Firearms.Bullets;

namespace Invasion.Models.Systems;

public class BulletSystem : SystemBase<BulletBase>
{
    public void Work(BulletBase bullet)
    {
        Entity<BulletBase> entity = new Entity<BulletBase>(bullet);
        Work(entity);
    }
    
    public override void Update(float deltaTime)
    {
    }
}