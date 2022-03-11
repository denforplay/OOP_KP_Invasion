using Invasion.Core.Interfaces;

namespace Invasion.Engine
{
    public class GameObject : ILiveObject
    {
        public Layer Layer { get; private set; }
        private List<IComponent> _components;
        public List<IComponent> Components => _components;
        public GameObject(List<IComponent> components = null , Layer layer = Layer.Default)
        {
            Layer = layer;
            if (components is not null)
                _components = components;
            else
                _components = new List<IComponent>();
        }

        public void AddComponent(IComponent component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component), "Added component is null");

            _components.Add(component);
        }

        public virtual void OnInvoke() { }
        public virtual void OnStart() { }
        public virtual void OnUpdate() { }
        public virtual void OnEnable() { }
        public virtual void OnDisable() { }

        public bool TryTakeComponent<T>(out T component) where T : IComponent
        {
            component = (T)_components.Find(component => component.GetType() == typeof(T) || component.GetType().BaseType == typeof(T));
            return component is not null;
        }
    }
}