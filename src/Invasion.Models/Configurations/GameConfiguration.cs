namespace Invasion.Models.Configurations
{
    /// <summary>
    /// Game configuration
    /// </summary>
    public class GameConfiguration
    {
        private int _neededScore;

        /// <summary>
        /// Needed score to win the game
        /// </summary>
        public int NeededScore => _neededScore;

        /// <summary>
        /// Game configuration constructor
        /// </summary>
        /// <param name="neededScore">Needed score to win the game</param>
        public GameConfiguration(int neededScore)
        {
            _neededScore = neededScore;
        }
    }
}
