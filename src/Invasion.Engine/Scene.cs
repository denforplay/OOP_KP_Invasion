using Invasion.Engine.Components;
using Invasion.Engine.Components.Colliders;
using Invasion.Engine.Interfaces;
using System.Numerics;

namespace Invasion.Engine
{
    /// <summary>
    /// Represents scene
    /// </summary>
    public class Scene : IScene
    {
        private List<GameObject> _gameObjects;
        private List<IController> _controllers;
        private List<IView> _views;

        private List<ColliderBase> _colliders;

        /// <summary>
        /// Scene default constructor
        /// </summary>
        public Scene()
        {
            _controllers = new List<IController>();
            _views = new List<IView>();
            _colliders = new List<ColliderBase>();
            _gameObjects = new List<GameObject>();
        }
        
        /// <summary>
        /// Scene constructor
        /// </summary>
        /// <param name="gameObjects">Start scene gameobjects</param>
        /// <param name="controllers">Start scene controllers</param>
        /// <param name="views">Start scene views</param>
        public Scene(List<GameObject> gameObjects, List<IController> controllers, List<IView> views)
        {
            _colliders = new List<ColliderBase>();
            _gameObjects = gameObjects;
            _controllers = controllers;
            _views = views;
            Initialize();
        }

        /// <summary>
        /// Add on scene new gameobject with view and controller
        /// </summary>
        /// <param name="gameObject">GameObject</param>
        /// <param name="view">View</param>
        /// <param name="controller">Controller</param>
        public void Add(GameObject gameObject, IView view, IController controller)
        {
            AddGameObject(gameObject);
            AddView(view);
            AddController(controller);
        }

        /// <summary>
        /// Add controller on scene
        /// </summary>
        /// <param name="controller">Controller</param>
        public void AddController(IController controller)
        {
            _controllers.Add(controller);
        }
        
        /// <summary>
        /// Removes controller from scene
        /// </summary>
        /// <param name="controller">Controller to remove</param>
        public void RemoveController(IController controller)
        {
            _controllers.Remove(controller);
        }
        
        /// <summary>
        /// Add gameobject on scene
        /// </summary>
        /// <param name="gameObject">Gameobject to add</param>
        public void AddGameObject(GameObject gameObject)
        {
            _gameObjects.Add(gameObject);
            if (gameObject.TryTakeComponent(out ColliderBase collider))
            {
                _colliders.Add(collider);
            }
        }

        /// <summary>
        /// Remove gameobject from scene
        /// </summary>
        /// <param name="gameObject">Gameobject to remove</param>
        public void RemoveGameObject(GameObject gameObject)
        {
            _gameObjects.Remove(gameObject);
            if (gameObject.TryTakeComponent(out ColliderBase collider))
            {
                _colliders.Remove(collider);
            }
        }

        /// <summary>
        /// Add view on scene
        /// </summary>
        /// <param name="view">View to add</param>
        public void AddView(IView view)
        {
            _views.Add(view);
        }

        /// <summary>
        /// Remove view from scene
        /// </summary>
        /// <param name="view">View</param>
        public void RemoveView(IView view)
        {
            _views.Remove(view);
        }
        
        /// <summary>
        /// Initialize scene
        /// </summary>
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

        /// <summary>
        /// Update scene
        /// </summary>
        public void Update()
        {
            _controllers.ForEach(c => c.Update());
            _views.ForEach(c => c.Update());
        }

        /// <summary>
        /// Update scene physics
        /// </summary>
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
            var colliders = new List<ColliderBase>(_colliders);
            colliders.ForEach(c =>
            {
                colliders.ForEach(nc =>
                {
                    if (c != nc)
                    {
                        c.TryCollide(nc);
                    }
                });
            });
        }

        public void Dispose()
        {
            _colliders.Clear();
            _gameObjects.Clear();
            _controllers.Clear();
            _views.ForEach(x => x.Dispose());
            _views.Clear();
        }
    }
}
