using Invasion.Engine.Interfaces;
using System.Numerics;

namespace Invasion.Engine.Components
{
    /// <summary>
    /// Provides object transform data
    /// </summary>
    public class Transform : IComponent
    {
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public Vector3 Scale { get; set; }
    }
}