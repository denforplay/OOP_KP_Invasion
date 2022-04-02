using System;

namespace Invasion.Models
{
    public class ScoreSystem
    {
        public event Action<int> OnScoreChanged;
        
        private int _currentScore;
        
        public int CurrentScore => _currentScore;

        public ScoreSystem()
        {
            Restart();
        }

        public void Restart()
        {
            _currentScore = 0;
            OnScoreChanged?.Invoke(_currentScore);
        }

        public void AddScores(int score)
        {
            _currentScore += score;
            OnScoreChanged?.Invoke(_currentScore);
        }
    }
}