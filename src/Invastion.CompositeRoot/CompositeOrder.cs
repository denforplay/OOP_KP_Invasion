using Invastion.CompositeRoot.Base;

namespace Invastion.CompositeRoot
{
    public class CompositeOrder
    {
        private List<ICompositeRoot> _compositeRoots;

        public CompositeOrder(List<ICompositeRoot> compositeRoots)
        {
            _compositeRoots = compositeRoots;
        }

        public void Start()
        {
            foreach (var root in _compositeRoots)
            {
                root.Compose();
            }
        }
    }
}
