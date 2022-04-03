namespace Invasion.Models.Configurations
{
    public class GameConfiguration
    {
        private int _neededScore;
        public int NeededScore => _neededScore;
        public GameConfiguration(int neededScore)
        {
            _neededScore = neededScore;
        }
    }
}
