using Invasion.Models.Collisions;
using Invastion.CompositeRoot.Base;

namespace Invastion.CompositeRoot.Implementations
{
    public class CollisionsCompositeRoot : ICompositeRoot
    {
        private CollisionController _controller;
        private CollisionRecords _records;
        public CollisionController Controller => _controller;

        public CollisionsCompositeRoot()
        {
            Compose();
        }

        public void Compose()
        {
            _records = new CollisionRecords();
            _controller = new CollisionController(_records.StartCollideValues);
        }

        public void Update()
        {
            _controller.Update();
        }
    }
}
