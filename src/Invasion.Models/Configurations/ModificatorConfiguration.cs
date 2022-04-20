namespace Invasion.Models.Configurations
{
    /// <summary>
    /// Modificator configuration
    /// </summary>
    public class ModificatorConfiguration
    {
        private int _duration;

        /// <summary>
        /// Modificator applying duration
        /// </summary>
        public int Duration => _duration;

        /// <summary>
        /// Modificator configuration
        /// </summary>
        /// <param name="duration">Modificator duration</param>
        public ModificatorConfiguration(int duration)
        {
            _duration = duration;
        }
    }
}
