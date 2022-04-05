using Invasion.Engine;
using Invasion.Engine.Interfaces;

namespace Invasion.Models;

public class Border : GameObject
{
    public Border(List<IComponent> components, Layer layer = Layer.Border) : base(components, layer)
    {
        
    }
}