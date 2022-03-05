﻿using Invasion.Core.Interfaces;
using SharpDX;

namespace Invasion.Engine.Components
{
    public class Transform : IComponent
    {
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public Vector3 Scale { get; set; }
    }
}