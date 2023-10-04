using Invasion.Models.Modificators;
using Invasion.Models.System.Base;

namespace Invasion.Models.Systems;

/// <summary>
/// Class represents system to work with modificators
/// </summary>
public class ModificatorSystem : SystemBase<ModificatorBase>
{
    /// <summary>
    /// Method that start to work new entity based on modificator model
    /// </summary>
    /// <param name="modificator"></param>
    public void Work(ModificatorBase modificator)
    {
        Entity<ModificatorBase> entity = new Entity<ModificatorBase>(modificator);
        Work(entity);
    }
    
    public override void Update(float deltaTime)
    {
    }
}