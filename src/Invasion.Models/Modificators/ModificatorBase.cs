using Invasion.Engine;
using Invasion.Engine.Interfaces;
using Invasion.Models.Configurations;

namespace Invasion.Models.Modificators;

/// <summary>
/// Class provides base modificator functionality
/// </summary>
public abstract class ModificatorBase : GameObject, IAppliable
{
    protected ModificatorConfiguration _configuration;

    /// <summary>
    /// Modificator base constructor
    /// </summary>
    /// <param name="components">List of components</param>
    /// <param name="configuration">Modificator configuration</param>
    /// <param name="layer">Modificator layer</param>
    public ModificatorBase(List<IComponent> components, ModificatorConfiguration configuration, Layer layer = Layer.Modificator) : base(components, layer)
    {
        _configuration = configuration;
    }

    public bool IsApplied { get; set; }
    public abstract void Apply(GameObject gameObject);
}