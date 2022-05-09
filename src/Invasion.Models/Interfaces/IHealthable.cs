namespace Invasion.Models.Interfaces
{
    /// <summary>
    /// Provides health functionality
    /// </summary>
    public interface IHealthable
    {
        /// <summary>
        /// Event called when health changed
        /// </summary>
        event Action<int> OnHealthChanged;

        /// <summary>
        /// Max health
        /// </summary>
        int MaxHealthPoint { get; set; }

        /// <summary>
        /// Current health
        /// </summary>
        int CurrentHealthPoints { get; set; }

        /// <summary>
        /// Take damage
        /// </summary>
        /// <param name="damage">Damage to take</param>
        void TakeDamage(int damage);

        /// <summary>
        /// Set current health
        /// </summary>
        /// <param name="health">Value of health to set</param>
        void SetHealth(int health);
    }
}
