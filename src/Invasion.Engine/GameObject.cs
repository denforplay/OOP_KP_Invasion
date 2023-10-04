using Invasion.Engine.Interfaces;

namespace Invasion.Engine
{
    /// <summary>
    /// Represents gameobject
    /// </summary>
    public class GameObject : ILiveObject
    {
        /// <summary>
        /// Event called on gameobject destroyed
        /// </summary>
        public event Action OnDestroyed;

        /// <summary>
        /// Gameobject layer
        /// </summary>
        public Layer Layer { get; private set; }

        private List<IComponent> _components;

        /// <summary>
        /// Gameobject components
        /// </summary>
        public List<IComponent> Components => _components;
        
        /// <summary>
        /// GameObject constructor
        /// </summary>
        /// <param name="components">Components</param>
        /// <param name="layer">Layer</param>
        public GameObject(List<IComponent> components, Layer layer = Layer.Default)
        {
            Layer = layer;
            if (components is not null)
                _components = components;
            else
                _components = new List<IComponent>();
        }

        /// <summary>
        /// Add component to gameobject
        /// </summary>
        /// <param name="component"></param>
        /// <exception cref="ArgumentNullException">Throws if component is null</exception>
        public void AddComponent(IComponent component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component), "Added component is null");

            _components.Add(component);
        }

        public virtual void OnInvoke() { }
        public virtual void OnStart() { }
        public virtual void OnUpdate() { }

        /// <summary>
        /// Calls to destroy object
        /// </summary>
        public virtual void OnDestroy()
        {
            OnDestroyed?.Invoke();
        }

        /// <summary>
        /// Method to take component
        /// </summary>
        /// <typeparam name="T">Component type</typeparam>
        /// <param name="component">Output component</param>
        /// <returns>True if component was taken, other returns false</returns>
        public bool TryTakeComponent<T>(out T component) where T : IComponent
        {
            component = (T)_components.Find(c => c is T);
            return component is not null;
        }
    }
}