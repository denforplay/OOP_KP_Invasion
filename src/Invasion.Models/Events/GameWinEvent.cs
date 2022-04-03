using Invasion.Core.EventBus.Interfaces;

namespace Invasion.Models.Events
{
    public class GameWinEvent : IEvent
    {
        private int _score;
        public int Score => _score;

        public GameWinEvent(int score)
        {
            _score = score;
        }
    }
}
