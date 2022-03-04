using Invasion.Core;
using Invasion.Core.Interfaces;
using Invasion.Engine;
using Invasion.Engine.Components;
using SharpDX;

namespace Invasion.View
{
    public class GameObjectView : IView
    {
        private Transform _transform;
        private SpriteRenderer _sprite;
        private GameObject _gameObject;
        private float _scale;
        private float _height;

        public GameObjectView(GameObject gameObject, float scale, float height)
        {
            _scale = scale;
            _height = height;
            _gameObject = gameObject;
            _gameObject.TryTakeComponent(out _transform);
            _gameObject.TryTakeComponent(out _sprite);
        }

        public void Update()
        {
            if (_sprite is not null)
            {
                if (_transform is not null)
                {
                    _sprite.Transform.Position = _transform.Position;
                }

                _sprite.Draw(1, _scale, _height);
            }
        }
    }
}
