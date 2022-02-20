using System;
using System.Numerics;

namespace Invasion.Core.Abstracts
{
    public abstract class Transformable2D
    {
        public Vector2 Position { get; set; }
        public Vector2 Rotation { get; set; }

        public Transformable2D(Vector2 position, Vector2 rotation)
        {
            Rotation = rotation;
            Position = position;
        }
    }
}
