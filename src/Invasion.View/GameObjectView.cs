using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Engine.Components.Interfaces;
using Invasion.Engine.Interfaces;

namespace Invasion.View
{
    public class GameObjectView : IView
    {
        private IRenderer _renderer;
        private Transform _transform;
        private GameObject _gameObject;

        public GameObjectView(GameObject gameObject)
        {
            _gameObject = gameObject;
            _gameObject.TryTakeComponent(out _transform);
            _gameObject.TryTakeComponent(out _renderer);
            _renderer?.SetTransform(_transform);
        }

        public void Update()
        {
            _renderer?.Draw();
        }
    }
}
