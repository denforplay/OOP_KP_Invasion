using Invasion.Core.EventBus.Interfaces;

namespace Invasion.Models.Events
{
    /// <summary>
    /// Represents event called when game won
    /// </summary>
    public class GameWinEvent : IEvent
    {
        private int _score;

        /// <summary>
        /// Game score
        /// </summary>
        public int Score => _score;

        /// <summary>
        /// Game win event constructor
        /// </summary>
        /// <param name="score">Game score</param>
        public GameWinEvent(int score)
        {
            _score = score;
        }
    }
}
