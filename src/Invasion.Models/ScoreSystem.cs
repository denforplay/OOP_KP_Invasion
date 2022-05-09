namespace Invasion.Models
{
    /// <summary>
    /// Represents score system
    /// </summary>
    public class ScoreSystem
    {
        /// <summary>
        /// Event called when score changed
        /// </summary>
        public event Action<int> OnScoreChanged;
        
        private int _currentScore;
        
        /// <summary>
        /// Current score system
        /// </summary>
        public int CurrentScore => _currentScore;

        /// <summary>
        /// Score system default constructor
        /// </summary>
        public ScoreSystem()
        {
            Restart();
        }

        /// <summary>
        /// Restart score system
        /// </summary>
        public void Restart()
        {
            _currentScore = 0;
            OnScoreChanged?.Invoke(_currentScore);
        }

        /// <summary>
        /// Add scores to score system
        /// </summary>
        /// <param name="score">Count of scores</param>
        public void AddScores(int score)
        {
            _currentScore += score;
            OnScoreChanged?.Invoke(_currentScore);
        }
    }
}