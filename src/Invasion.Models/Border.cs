using Invasion.Core.Interfaces;
using Invasion.Engine;

namespace Invasion.Models;

public class Border : GameObject
{
    public Border(List<IComponent> components, Layer layer = Layer.Border) : base(components, layer)
    {
        
    }
}