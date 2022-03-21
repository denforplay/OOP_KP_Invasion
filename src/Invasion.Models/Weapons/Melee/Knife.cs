﻿using Invasion.Core.Interfaces;
using Invasion.Engine;

namespace Invasion.Models.Weapons.Melee;

public class Knife : MeleeBase
{
    public Knife(GameObject parent, List<IComponent> components = null) : base(parent, components)
    {
    }
}