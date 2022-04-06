namespace Invasion.Models.Configurations
{
    public class ModificatorConfiguration
    {
        private int _duration;

        public int Duration => _duration;

        public ModificatorConfiguration(int duration)
        {
            _duration = duration;
        }
    }
}
