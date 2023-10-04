using Invasion.Engine;
using Invasion.Engine.Interfaces;

namespace Invasion.Models;

/// <summary>
/// Represents border
/// </summary>
public class Border : GameObject
{
    /// <summary>
    /// Border constructor
    /// </summary>
    /// <param name="components">Components</param>
    /// <param name="layer">Layer</param>
    public Border(List<IComponent> components, Layer layer = Layer.Border) : base(components, layer)
    {
        
    }
}