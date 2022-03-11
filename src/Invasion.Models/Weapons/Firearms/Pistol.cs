using Invasion.Core.Interfaces;
using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Models.Weapons.Firearms.Base;

namespace Invasion.Models.Weapons.Firearms;

public class Pistol : FirearmBase
{
    public Pistol(List<IComponent> components, Transform parent, Layer layer = Layer.Default) : base(components, parent, layer)
    {
    }
}