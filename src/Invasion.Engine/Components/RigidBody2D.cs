using Invasion.Core.Interfaces;
using SharpDX;

namespace Invasion.Engine.Components
{
    public class RigidBody2D : IComponent
    {
        public Vector2 Speed { get; private set; }

        public void SetSpeed(Vector2 speed)
        {
            Speed = speed;
        }
    }
}
