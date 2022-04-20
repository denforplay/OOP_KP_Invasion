namespace Invasion.Core.EventBus.Interfaces
{
    /// <summary>
    /// Event action
    /// </summary>
    /// <typeparam name="T">Input action type</typeparam>
    public interface IEventAction<T> where T : IEvent
    {
        /// <summary>
        /// Add event subscriber
        /// </summary>
        /// <param name="subscriber">Subscriber</param>
        void AddSubscriber(Action<T> subscriber);

        /// <summary>
        /// Remove subscriber
        /// </summary>
        /// <param name="subscriber">Subscriber</param>
        void RemoveSubscriber(Action<T> subscriber);

        /// <summary>
        /// Check if event has subscriber
        /// </summary>
        /// <param name="subscriber">Subscriber</param>
        /// <returns>True if event has subscriber, other returns false</returns>
        bool HasSubscriber(Action<T> subscriber);

        /// <summary>
        /// Invoke event
        /// </summary>
        /// <param name="invokator">Invokated object</param>
        void Invoke(T invokator);
    }
}
