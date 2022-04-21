using Invasion.Engine.Interfaces;

namespace Invasion.Engine.Components.Interfaces
{
    /// <summary>
    /// Describes renderer functionality
    /// </summary>
    public interface IRenderer : IComponent
    {
        /// <summary>
        /// Render method
        /// </summary>
        void Draw();

        /// <summary>
        /// Render transform information
        /// </summary>
        /// <param name="transform">Transform information</param>
        void SetTransform(Transform transform);

        void Dispose();
    }
}
