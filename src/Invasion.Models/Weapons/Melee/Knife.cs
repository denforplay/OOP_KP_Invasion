using Invasion.Engine;
using Invasion.Engine.Interfaces;

namespace Invasion.Models.Weapons.Melee;

public class Knife : MeleeBase
{
    public Knife(GameObject parent, List<IComponent> components = null) : base(parent, components)
    {
    }
}