using Invasion.Core.Interfaces;
using Invasion.Engine.Components;
using Invasion.Engine.Components.Colliders;
using SharpDX;

namespace Invasion.Engine
{
    public class Scene
    {
        private List<GameObject> _gameObjects;
        private List<IController> _controllers;
        private List<IView> _views;

        private List<ColliderBase> _colliders;

        public Scene()
        {
            _controllers = new List<IController>();
            _views = new List<IView>();
            _colliders = new List<ColliderBase>();
            _gameObjects = new List<GameObject>();
        }
        
        public Scene(List<GameObject> gameObjects, List<IController> controllers, List<IView> views)
        {
            _colliders = new List<ColliderBase>();
            _gameObjects = gameObjects;
            _controllers = controllers;
            _views = views;
            Initialize();
        }

        public void Add(GameObject gameObject, IView view, IController controller)
        {
            AddGameObject(gameObject);
            AddGameObjectView(view);
            AddController(controller);
        }

        public void AddController(IController controller)
        {
            if (controller is null)
            {
                throw new ArgumentNullException(nameof(controller));
            }
            
            _controllers.Add(controller);
        }
        
        public void AddGameObject(GameObject gameObject)
        {
            _gameObjects.Add(gameObject);
            if (gameObject.TryTakeComponent(out ColliderBase collider))
            {
                _colliders.Add(collider);
            }
        }

        public void AddGameObjectView(IView gameObjectView)
        {
            _views.Add(gameObjectView);
        }

        public void Initialize()
        {
            _gameObjects.ForEach(g =>
            {
                if (g.TryTakeComponent(out ColliderBase collider))
                {
                    _colliders.Add(collider);
                }
            });
        }

        public void Update()
        {
            _controllers.ForEach(c => c.Update());
            _views.ForEach(c => c.Update());
        }

        public void FixedUpdate()
        {
            _gameObjects.ForEach(g =>
            {
                if (g.TryTakeComponent(out Transform transform) && g.TryTakeComponent(out RigidBody2D rigidBody))
                {
                    transform.Position = new Vector3(transform.Position.X + rigidBody.Speed.X,
                        transform.Position.Y + rigidBody.Speed.Y, transform.Position.Z);
                }
            });

            _colliders.ForEach(c =>
            {
                _colliders.ForEach(nc =>
                {
                    if (c != nc)
                    {
                        c.TryCollide(nc);
                    }
                });
            });
        }
    }
}
