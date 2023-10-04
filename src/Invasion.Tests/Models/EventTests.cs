using Invasion.Models.Events;
using Xunit;

namespace Invasion.Tests.Models
{
    public class EventTests
    {
        [Fact]
        public void GameWinEventTest()
        {
            GameWinEvent gameWinEvent = new GameWinEvent(5);
            Assert.NotNull(gameWinEvent);
            Assert.Equal(5, gameWinEvent.Score);
        }
    }
}
