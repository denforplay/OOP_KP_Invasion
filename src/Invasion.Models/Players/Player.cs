using Invasion.Engine;
using Invasion.Engine.Interfaces;
using Invasion.Models.Configurations;
using Invasion.Models.Interfaces;

namespace Invasion.Models
{
    /// <summary>
    /// Class reprents player object
    /// </summary>
    public class Player: GameObject, IHealthable
    {
        public event Action<int>? OnHealthChanged;
        
        /// <summary>
        /// Player configuration
        /// </summary>
        public PlayerConfiguration Configuration { get; init; }
        public int MaxHealthPoint { get; set; }
        public virtual int CurrentHealthPoints { get; set; }

        /// <summary>
        /// Player speed
        /// </summary>
        public virtual float Speed { get; set; }

        /// <summary>
        /// Player can give damage or not
        /// </summary>
        public virtual bool CanDamage { get => true; }

        /// <summary>
        /// Player constructor
        /// </summary>
        /// <param name="components">Components</param>
        /// <param name="playerConfig">Player configuration</param>
        /// <param name="layer">Layer</param>
        public Player(List<IComponent> components, PlayerConfiguration playerConfig, Layer layer = Layer.Player) : base(components, layer)
        {
            Configuration = playerConfig;
            Speed = playerConfig.Speed;
            CurrentHealthPoints = playerConfig.MaxHealth;
            MaxHealthPoint = playerConfig.MaxHealth;
        }

        public void TakeDamage(int damage)
        {
            CurrentHealthPoints -= damage;
            OnHealthChanged?.Invoke(CurrentHealthPoints);
            if (CurrentHealthPoints <= 0)
            {
                OnDestroy();
            }
        }

        public void SetHealth(int health)
        {
            CurrentHealthPoints = health;
            OnHealthChanged?.Invoke(CurrentHealthPoints);
        }
    }
}
