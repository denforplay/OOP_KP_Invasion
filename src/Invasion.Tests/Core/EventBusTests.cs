using Invasion.Core.EventBus;
using Invasion.Models.Events;
using Xunit;

namespace Invasion.Tests.Core
{
    public class EventBusTests
    {
        private bool _isEventCalled = false;

        [Fact]
        public void EventBusTest()
        {
            EventBus eventBus = new EventBus();
            eventBus.Subscribe<GameWinEvent>(Subscriber);
            eventBus.Invoke(new GameWinEvent(5));
            Assert.True(_isEventCalled);
            _isEventCalled = false;
            eventBus.Unsubscribe<GameWinEvent>(Subscriber);
            eventBus.Invoke(new GameWinEvent(5));
            SingletonEventBus.GetInstance.Unsubscribe<GameWinEvent>(Subscriber);
            Assert.False(_isEventCalled);
        }

        [Fact]
        public void SingletonEventBusTest()
        {
            SingletonEventBus.GetInstance.Subscribe<GameWinEvent>(Subscriber);
            SingletonEventBus.GetInstance.Invoke(new GameWinEvent(5));
            Assert.True(_isEventCalled);
            _isEventCalled = false;
            SingletonEventBus.GetInstance.Unsubscribe<GameWinEvent>(Subscriber);
            SingletonEventBus.GetInstance.Invoke(new GameWinEvent(5));
            SingletonEventBus.GetInstance.Unsubscribe<GameWinEvent>(Subscriber);
            Assert.False(_isEventCalled);
        }

        private void Subscriber(GameWinEvent gameWinEvent)
        {
            _isEventCalled = true;
        }
    }
}
