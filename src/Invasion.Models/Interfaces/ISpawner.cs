namespace Invasion.Models.Interfaces
{
    /// <summary>
    /// Interface provides base spawner functionality
    /// </summary>
    public interface ISpawner
    {
        /// <summary>
        /// Method that spawn spawner entities
        /// </summary>
        void Spawn();

        /// <summary>
        /// Method that starts spawn
        /// </summary>
        void StartSpawn();

        /// <summary>
        /// Method that stops spawn
        /// </summary>
        void StopSpawn();
    }
}
