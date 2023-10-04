
namespace Invasion.Core.EventBus.Interfaces
{
    /// <summary>
    /// Describes event bus functionality
    /// </summary>
    public interface IEventBus
    {
        /// <summary>
        /// Subscribe on event
        /// </summary>
        /// <typeparam name="T">Event type</typeparam>
        /// <param name="subscriber">Subriber delegate</param>
        void Subscribe<T>(Action<T> subscriber) where T : IEvent;

        /// <summary>
        /// Unsubscribe delegate from event
        /// </summary>
        /// <typeparam name="T">Event type</typeparam>
        /// <param name="subscriber"></param>
        void Unsubscribe<T>(Action<T> subscriber) where T : IEvent;

        /// <summary>
        /// Invokes event
        /// </summary>
        /// <typeparam name="T">Event type</typeparam>
        /// <param name="invocator">Invocator event</param>
        void Invoke<T>(T invocator) where T : IEvent;
    }
}
