using Invasion.Engine.Interfaces;

namespace Invasion.Engine.Components.Interfaces
{
    public interface IRenderer : IComponent
    {
        void Draw();
        void SetTransform(Transform transform);
    }
}
