
using Invasion.Core.EventBus.Interfaces;

namespace Invasion.Core.EventBus
{
    /// <summary>
    /// Represents event action
    /// </summary>
    /// <typeparam name="T">Type of event</typeparam>
    public class EventAction<T> : IEventAction<T> where T : IEvent
    {
        private Action<T> _action = delegate { };

        /// <summary>
        /// Add subsriber on event
        /// </summary>
        /// <param name="subscriber">Subsribted delegate</param>
        public void AddSubscriber(Action<T> subscriber)
        {
            _action += subscriber;
        }

        /// <summary>
        /// Invoke event
        /// </summary>
        /// <param name="invokator">Event invoker</param>
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
