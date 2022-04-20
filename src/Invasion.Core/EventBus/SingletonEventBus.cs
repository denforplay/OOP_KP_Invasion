using Invasion.Core.EventBus.Interfaces;
using Invasion.Core.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invasion.Core.EventBus
{
    /// <summary>
    /// Represent singleton event bus
    /// </summary>
    public class SingletonEventBus : Singleton<SingletonEventBus>
    {
        private IEventBus _eventBus = new EventBus();

        public void Subscribe<T>(Action<T> subscriber) where T : IEvent
        {
            GetInstance._eventBus.Subscribe<T>(subscriber);
        }

        public void Unsubscribe<T>(Action<T> subscriber) where T : IEvent
        {
            GetInstance._eventBus.Unsubscribe<T>(subscriber);
        }

        public void Invoke<T>(T invokator) where T : IEvent
        {
            GetInstance._eventBus.Invoke(invokator);
        }
    }
}
