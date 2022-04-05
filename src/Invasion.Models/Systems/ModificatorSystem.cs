using Invasion.Models.Modificators;
using Invasion.Models.System.Base;

namespace Invasion.Models.Systems;

public class ModificatorSystem : SystemBase<ModificatorBase>
{
    public void Work(ModificatorBase modificator)
    {
        Entity<ModificatorBase> entity = new Entity<ModificatorBase>(modificator);
        Work(entity);
    }
    
    public override void Update(float deltaTime)
    {
    }
}