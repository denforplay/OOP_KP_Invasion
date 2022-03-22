using Invasion.Core;
using Invasion.Core.Abstracts;
using Invasion.Models.Modificators;

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