using Invasion.Engine.Interfaces;
using System.Numerics;

namespace Invasion.Engine.Components
{
    /// <summary>
    /// Provides object physics
    /// </summary>
    public class RigidBody2D : IComponent
    {
        /// <summary>
        /// Speed
        /// </summary>
        public Vector2 Speed { get; set; }
    }
}
