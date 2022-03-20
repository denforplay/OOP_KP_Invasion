using Invasion.Core.Interfaces;
using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Models.Weapons.Firearms.Base;

namespace Invasion.Models.Weapons.Firearms;

public class Pistol : FirearmBase
{
    public Pistol(DX2D dx2D, List<IComponent> components, GameObject parent, Layer layer = Layer.Default) : base(dx2D, components, parent, layer)
    {
    }
}