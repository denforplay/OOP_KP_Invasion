
using Invasion.Core.EventBus.Interfaces;

namespace Invasion.Core.EventBus
{
    public class EventAction<T> : IEventAction<T> where T : IEvent
    {
        private Action<T> _action = delegate { };

        public void AddSubscriber(Action<T> subscriber)
        {
            _action += subscriber;
        }

        public void Invoke(T invokator)
        {
            _action.Invoke(invokator);
        }

        public bool HasSubscriber(Action<T> subscriber)
        {
            return _action.GetInvocationList().Contains(subscriber);
        }

        public void RemoveSubscriber(Action<T> subscriber)
        {
            _action -= subscriber;
        }
    }
}
